using System;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using SchematicHQ.Client.Core;
using Wasmtime;

#nullable enable

namespace SchematicHQ.Client.RulesEngine
{
    /// <summary>
    /// WASM-based rules engine for local flag evaluation.
    ///
    /// <para>Loads the shared Schematic rules engine WASM binary (compiled from Rust)
    /// and evaluates flags locally using cached flag definitions, company data, and
    /// user data. Hosts the module with the Wasmtime .NET runtime — mirroring the
    /// Python and Ruby SDKs (and the pure-Java Chicory host in schematic-java).</para>
    ///
    /// <para>Data flow: typed models are serialized to snake_case JSON (via each type's
    /// <c>JsonPropertyName</c> attributes) into an envelope <c>{flag, company, user}</c>;
    /// the WASM module returns camelCase JSON which is converted to snake_case and
    /// deserialized into <see cref="RulesengineCheckFlagResult"/>.</para>
    /// </summary>
    internal sealed class WasmRulesEngine : IDisposable
    {
        private const string WasmResourceName = "rulesengine.wasm";

        private readonly ILogger _logger;
        private readonly object _lock = new();

        private volatile bool _initialized;
        private string? _versionKey;

        // Wasmtime objects — kept alive for the lifetime of the engine.
        private Engine? _engine;
        private Module? _module;
        private Linker? _linker;
        private Store? _store;
        private Instance _instance;
        private Memory? _memory;

        // Exported functions (see the Rust rules engine C-ABI).
        private Func<int, int>? _alloc; // alloc(len) -> ptr
        private Action<int, int>? _dealloc; // dealloc(ptr, len)
        private Func<int, int, int>? _checkFlag; // checkFlagCombined(ptr, len) -> resultLen
        private Action<long>? _setTime; // setCurrentTimeMillis(nowMillis) — optional
        private Func<int>? _getResultJson; // getResultJson() -> ptr
        private Func<int>? _getResultJsonLength; // getResultJsonLength() -> len
        private Func<int>? _getVersionKey; // get_version_key_wasm() -> ptr (C string)

        public WasmRulesEngine(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsInitialized => _initialized;

        /// <summary>
        /// Loads and instantiates the WASM module. Idempotent and thread-safe.
        /// On failure the engine stays uninitialized so callers can degrade gracefully.
        /// </summary>
        public void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                using var wasmStream =
                    typeof(WasmRulesEngine).Assembly.GetManifestResourceStream(WasmResourceName)
                    ?? throw new InvalidOperationException(
                        $"Embedded WASM resource '{WasmResourceName}' not found. "
                            + "Ensure scripts/download-wasm.sh ran before the build.");

                _engine = new Engine();
                _module = Module.FromStream(_engine, WasmResourceName, wasmStream);

                _linker = new Linker(_engine);
                _linker.DefineWasi();

                _store = new Store(_engine);
                // Inherit stdout/stderr so Rust panics / eprintln! surface for debugging.
                _store.SetWasiConfiguration(
                    new WasiConfiguration()
                        .WithInheritedStandardOutput()
                        .WithInheritedStandardError()
                );

                _instance = _linker.Instantiate(_store, _module);

                _memory =
                    _instance.GetMemory("memory")
                    ?? throw new InvalidOperationException("WASM module does not export 'memory'");

                _alloc =
                    _instance.GetFunction<int, int>("alloc")
                    ?? throw new InvalidOperationException("WASM module does not export 'alloc'");
                _dealloc =
                    _instance.GetAction<int, int>("dealloc")
                    ?? throw new InvalidOperationException("WASM module does not export 'dealloc'");
                _checkFlag =
                    _instance.GetFunction<int, int, int>("checkFlagCombined")
                    ?? throw new InvalidOperationException(
                        "WASM module does not export 'checkFlagCombined'"
                    );
                _getResultJson =
                    _instance.GetFunction<int>("getResultJson")
                    ?? throw new InvalidOperationException(
                        "WASM module does not export 'getResultJson'"
                    );
                _getResultJsonLength =
                    _instance.GetFunction<int>("getResultJsonLength")
                    ?? throw new InvalidOperationException(
                        "WASM module does not export 'getResultJsonLength'"
                    );

                // Optional exports — resolved defensively so older wasm builds still load.
                // Without setCurrentTimeMillis the clockless wasmtime host cannot compute
                // metric-period reset timestamps (SCHY-471); with it, feature_usage_reset_at
                // is populated.
                _setTime = _instance.GetAction<long>("setCurrentTimeMillis");
                _getVersionKey = _instance.GetFunction<int>("get_version_key_wasm");

                _initialized = true;

                // Cache the version key immediately — the pointer is only stable before
                // any other WASM call mutates linear memory.
                _versionKey = ReadVersionKeyFromWasm();
                _logger.LogDebug("WASM rules engine initialized (version: {Version})", _versionKey);
            }
        }

        /// <summary>
        /// Evaluates a flag using the WASM rules engine.
        /// </summary>
        public CheckFlagResult CheckFlag(
            RulesengineCompany? company,
            RulesengineUser? user,
            RulesengineFlag flag
        )
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("WASM rules engine not initialized");
            }

            var envelope = new JsonObject
            {
                ["flag"] = Sanitize(JsonUtils.SerializeToNode(flag), null),
            };
            if (company != null)
            {
                envelope["company"] = Sanitize(JsonUtils.SerializeToNode(company), "company");
            }
            if (user != null)
            {
                envelope["user"] = Sanitize(JsonUtils.SerializeToNode(user), "user");
            }

            var resultJson = CallWasm(envelope.ToJsonString());

            // WASM returns camelCase JSON; the generated type expects snake_case.
            var camelNode =
                JsonNode.Parse(resultJson)
                ?? throw new InvalidOperationException("WASM returned empty result JSON");
            var snakeJson = CamelToSnakeKeys(camelNode).ToJsonString();
            var result = JsonUtils.Deserialize<RulesengineCheckFlagResult>(snakeJson);

            return ToCheckFlagResult(result);
        }

        private static CheckFlagResult ToCheckFlagResult(RulesengineCheckFlagResult r) =>
            new()
            {
                CompanyId = r.CompanyId,
                Entitlement = r.Entitlement,
                Error = string.IsNullOrEmpty(r.Err) ? null : new Exception(r.Err),
                FeatureAllocation = r.FeatureAllocation,
                FeatureUsage = r.FeatureUsage,
                FeatureUsageEvent = r.FeatureUsageEvent,
                FeatureUsagePeriod = r.FeatureUsagePeriod,
                FeatureUsageResetAt = r.FeatureUsageResetAt,
                FlagId = r.FlagId,
                FlagKey = r.FlagKey,
                Reason = r.Reason,
                RuleId = r.RuleId,
                RuleType = r.RuleType,
                UserId = r.UserId,
                Value = r.Value,
            };

        /// <summary>
        /// Version key from the WASM engine, used for cache-key generation so caches
        /// invalidate when the engine changes. Computed once during initialization.
        /// </summary>
        public string GetVersionKey() => _versionKey ?? "1";

        public void Dispose()
        {
            _store?.Dispose();
            _linker?.Dispose();
            _module?.Dispose();
            _engine?.Dispose();
        }

        /// <summary>
        /// Writes <paramref name="inputJson"/> into WASM memory, invokes the engine,
        /// and returns the result JSON. Serialized under a lock because the WASM
        /// instance shares a single linear memory across calls.
        /// </summary>
        private string CallWasm(string inputJson)
        {
            var data = Encoding.UTF8.GetBytes(inputJson);
            var length = data.Length;

            lock (_lock)
            {
                var ptr = _alloc!(length);
                try
                {
                    data.CopyTo(_memory!.GetSpan(ptr, length));

                    // Supply the host's current time so the engine can compute
                    // metric-period reset timestamps (SCHY-471). No-op on older wasm.
                    _setTime?.Invoke(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

                    var resultLen = _checkFlag!(ptr, length);
                    if (resultLen < 0)
                    {
                        throw new InvalidOperationException(
                            $"WASM checkFlagCombined returned error code: {resultLen}"
                        );
                    }

                    var resultPtr = _getResultJson!();
                    var actualLen = _getResultJsonLength!();

                    // Result buffer is owned by the WASM module (thread-local); no free needed.
                    return _memory.ReadString(resultPtr, actualLen, Encoding.UTF8);
                }
                finally
                {
                    // Frees the input buffer only.
                    _dealloc!(ptr, length);
                }
            }
        }

        private string ReadVersionKeyFromWasm()
        {
            if (_getVersionKey == null)
            {
                return "1";
            }

            try
            {
                var ptr = _getVersionKey();
                return _memory!.ReadNullTerminatedString(ptr);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to read WASM version key");
                return "1";
            }
        }

        /// <summary>
        /// Recursively converts JSON object keys from camelCase to snake_case.
        /// </summary>
        private static JsonNode? CamelToSnakeKeys(JsonNode? node)
        {
            switch (node)
            {
                case JsonObject obj:
                    var result = new JsonObject();
                    foreach (var kvp in obj)
                    {
                        result[CamelToSnake(kvp.Key)] = CamelToSnakeKeys(kvp.Value?.DeepClone());
                    }
                    return result;
                case JsonArray arr:
                    var newArr = new JsonArray();
                    foreach (var element in arr)
                    {
                        newArr.Add(CamelToSnakeKeys(element?.DeepClone()));
                    }
                    return newArr;
                default:
                    return node?.DeepClone();
            }
        }

        private static string CamelToSnake(string name)
        {
            var sb = new StringBuilder(name.Length + 4);
            for (var i = 0; i < name.Length; i++)
            {
                var c = name[i];
                if (char.IsUpper(c))
                {
                    if (i > 0)
                    {
                        sb.Append('_');
                    }
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Recursively fixes any <c>trait_definition.entity_type</c> that is missing or
        /// empty. The WASM engine's Rust <c>EntityType</c> enum only accepts "user" or
        /// "company"; an empty string fails the entire parse with error code -1.
        ///
        /// <para>When <paramref name="defaultEntityType"/> is non-null (company/user
        /// subtree) we inject it; when null (flag subtree, where the entity is ambiguous)
        /// we drop the optional <c>trait_definition</c> so the rule still evaluates.
        /// Workaround for replicator data that writes <c>entity_type: ""</c>; a no-op
        /// once the replicator writes correct values.</para>
        /// </summary>
        private static JsonNode? Sanitize(JsonNode? node, string? defaultEntityType)
        {
            switch (node)
            {
                case JsonObject obj:
                    if (obj["trait_definition"] is JsonObject td)
                    {
                        var et = td["entity_type"];
                        var missing =
                            et == null
                            || et.GetValueKind() != System.Text.Json.JsonValueKind.String
                            || string.IsNullOrEmpty(et.GetValue<string>());
                        if (missing)
                        {
                            if (defaultEntityType != null)
                            {
                                td["entity_type"] = defaultEntityType;
                            }
                            else
                            {
                                obj.Remove("trait_definition");
                            }
                        }
                    }
                    foreach (var kvp in obj)
                    {
                        Sanitize(kvp.Value, defaultEntityType);
                    }
                    return node;
                case JsonArray arr:
                    foreach (var element in arr)
                    {
                        Sanitize(element, defaultEntityType);
                    }
                    return node;
                default:
                    return node;
            }
        }
    }
}
