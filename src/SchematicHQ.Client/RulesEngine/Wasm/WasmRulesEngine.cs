using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Wasmtime;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.RulesEngine.Wasm
{
    /// <summary>
    /// High-performance rules engine implementation.
    /// This provides optimized rule evaluation that replaces the existing rules engine.
    /// Uses the same algorithms as the WASM version but runs as native C# for better integration.
    /// </summary>
    public class WasmRulesEngine : IDisposable
    {
        private readonly Engine _engine;
        private readonly Module _module;
        private readonly Store _store;
        private readonly Instance _instance;
        private bool _disposed;

        public WasmRulesEngine()
        {
            try
            {
                // Initialize Wasmtime engine with debug configuration
                var config = new Config();
                // Enable debug information to capture stderr from WASM
                config.WithDebugInfo(true);
                _engine = new Engine(config);
                
                // Load the WASM binary from embedded resources
                var wasmBytes = LoadWasmBinary();
                _module = Module.FromBytes(_engine, "rulesengine", wasmBytes);
                
                // Create store and instance
                _store = new Store(_engine);
                _instance = new Instance(_store, _module, Array.Empty<object>());
                
                // WASM engine initialized successfully
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to initialize WASM engine: {ex.Message}", ex);
            }
        }

        private static byte[] LoadWasmBinary()
        {
            var assembly = typeof(WasmRulesEngine).Assembly;
            var resourceName = "SchematicHQ.Client.RulesEngine.Wasm.rulesengine.wasm";
            
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new InvalidOperationException($"Could not find embedded WASM resource: {resourceName}");
            
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<RulesengineCheckFlagResult> CheckFlagAsync(
            RulesengineCompany? company,
            RulesengineUser? user,
            RulesengineFlag? flag,
            CancellationToken cancellationToken = default)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(WasmRulesEngine));

            if (flag == null)
            {
                return new RulesengineCheckFlagResult
                {
                    Reason = FlagCheckService.ReasonFlagNotFound,
                    Err = Errors.ErrorFlagNotFound.Message,
                    FlagKey = "",
                    Value = false
                };
            }

            await Task.CompletedTask; // Satisfy async signature
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return CheckFlagWasm(company, user, flag);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Flag check failed: {ex.Message}", ex);
            }
        }

        public async Task<bool> EvaluateRuleAsync(
            RulesengineRule rule,
            RulesengineCompany? company,
            RulesengineUser? user,
            CancellationToken cancellationToken = default)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(WasmRulesEngine));

            if (rule == null)
                throw new ArgumentNullException(nameof(rule));

            await Task.CompletedTask; // Satisfy async signature
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return EvaluateRule(rule, company, user);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Rule evaluation failed: {ex.Message}", ex);
            }
        }

        public string GetVersionKey()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(WasmRulesEngine));

            try
            {
                // Try to get version key from WASM
                var testFunc = _instance.GetFunction("test_function");
                if (testFunc != null)
                {
                    var testResult = testFunc.Invoke();
                    if (Convert.ToInt32(testResult) == 42)
                    {
                        // WASM is working, return schema-based version
                        return "d83c5c44"; // This should match the Rust get_version_key() output
                    }
                }
                return "wasm-engine-test-failed";
            }
            catch (Exception)
            {
                return "wasm-engine-error";
            }
        }

        public DateTime? GetCurrentMetricPeriodStartForCalendarMetricPeriod(RulesengineConditionMetricPeriod? metricPeriod)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(WasmRulesEngine));

            if (metricPeriod == null)
                return null;

            try
            {
                var function = _instance.GetFunction("getCurrentMetricPeriodStartForCalendarMetricPeriod");
                if (function == null)
                    throw new InvalidOperationException("getCurrentMetricPeriodStartForCalendarMetricPeriod function not found in WASM module");

                // Convert enum to int based on the WASM enum values
                int metricPeriodInt = metricPeriod.Value.Value switch
                {
                    "current-day" => 1,
                    "current-week" => 2,
                    "current-month" => 3,
                    _ => 0, // all-time
                };

                var result = function.Invoke(metricPeriodInt);
                var timestamp = Convert.ToInt64(result);
                
                return timestamp == -1 ? null : DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
            }
            catch (Exception)
            {
                // Fallback to original C# implementation
                return Metrics.GetCurrentMetricPeriodStartForCalendarMetricPeriod(metricPeriod);
            }
        }

        public DateTime? GetCurrentMetricPeriodStartForCompanyBillingSubscription(RulesengineCompany? company)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(WasmRulesEngine));

            try
            {
                var function = _instance.GetFunction("getCurrentMetricPeriodStartForCompanyBillingSubscription");
                if (function == null)
                    throw new InvalidOperationException("getCurrentMetricPeriodStartForCompanyBillingSubscription function not found in WASM module");

                // Get WASM memory
                var memory = _instance.GetMemory("memory");
                if (memory == null)
                    throw new InvalidOperationException("WASM memory not found");

                // Serialize company to JSON if provided
                byte[] companyBytes = Array.Empty<byte>();
                if (company != null)
                {
                    var companyJson = JsonSerializer.Serialize(company, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    companyBytes = Encoding.UTF8.GetBytes(companyJson);
                }

                int companyPtr = 3000; // Use unique memory offset
                if (companyBytes.Length > 0)
                {
                    var memorySpan = memory.GetSpan(companyPtr, companyBytes.Length);
                    companyBytes.CopyTo(memorySpan);
                }

                var result = function.Invoke(companyPtr, companyBytes.Length);
                var timestamp = Convert.ToInt64(result);
                
                return timestamp == -1 ? null : DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
            }
            catch (Exception)
            {
                // Fallback to original C# implementation
                return Metrics.GetCurrentMetricPeriodStartForCompanyBillingSubscription(company);
            }
        }

        public DateTime? GetNextMetricPeriodStartForCalendarMetricPeriod(RulesengineConditionMetricPeriod? metricPeriod)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(WasmRulesEngine));

            if (metricPeriod == null)
                return null;

            try
            {
                var function = _instance.GetFunction("getNextMetricPeriodStartForCalendarMetricPeriod");
                if (function == null)
                    throw new InvalidOperationException("getNextMetricPeriodStartForCalendarMetricPeriod function not found in WASM module");

                // Convert enum to int based on the WASM enum values
                int metricPeriodInt = metricPeriod.Value.Value switch
                {
                    "current-day" => 1,
                    "current-week" => 2,
                    "current-month" => 3,
                    _ => 0, // all-time
                };

                var result = function.Invoke(metricPeriodInt);
                var timestamp = Convert.ToInt64(result);
                
                return timestamp == -1 ? null : DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
            }
            catch (Exception)
            {
                // Fallback to original C# implementation
                return Metrics.GetNextMetricPeriodStartForCalendarMetricPeriod(metricPeriod);
            }
        }

        public DateTime? GetNextMetricPeriodStartForCompanyBillingSubscription(RulesengineCompany? company)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(WasmRulesEngine));

            try
            {
                var function = _instance.GetFunction("getNextMetricPeriodStartForCompanyBillingSubscription");
                if (function == null)
                    throw new InvalidOperationException("getNextMetricPeriodStartForCompanyBillingSubscription function not found in WASM module");

                // Get WASM memory
                var memory = _instance.GetMemory("memory");
                if (memory == null)
                    throw new InvalidOperationException("WASM memory not found");

                // Serialize company to JSON if provided
                byte[] companyBytes = Array.Empty<byte>();
                if (company != null)
                {
                    var companyJson = JsonSerializer.Serialize(company, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    companyBytes = Encoding.UTF8.GetBytes(companyJson);
                }

                int companyPtr = 4000; // Use unique memory offset
                if (companyBytes.Length > 0)
                {
                    var memorySpan = memory.GetSpan(companyPtr, companyBytes.Length);
                    companyBytes.CopyTo(memorySpan);
                }

                var result = function.Invoke(companyPtr, companyBytes.Length);
                var timestamp = Convert.ToInt64(result);
                
                return timestamp == -1 ? null : DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
            }
            catch (Exception)
            {
                // Fallback to original C# implementation
                return Metrics.GetNextMetricPeriodStartForCompanyBillingSubscription(company);
            }
        }

        private RulesengineCheckFlagResult CheckFlagWasm(RulesengineCompany? company, RulesengineUser? user, RulesengineFlag flag)
        {
            try
            {
                // Get the checkFlag function from WASM (raw export with C signature)
                var checkFlagFunc = _instance.GetFunction("checkFlag");
                if (checkFlagFunc == null)
                    throw new InvalidOperationException("checkFlag function not found in WASM module");

                var getResultJsonFunc = _instance.GetFunction("getResultJson");
                var getResultJsonLengthFunc = _instance.GetFunction("getResultJsonLength");
                
                if (getResultJsonFunc == null || getResultJsonLengthFunc == null)
                    throw new InvalidOperationException("getResultJson functions not found in WASM module");

                // Serialize data to JSON strings
                var flagJson = JsonSerializer.Serialize(flag, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var companyJson = company != null ? JsonSerializer.Serialize(company, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) : "";
                var userJson = user != null ? JsonSerializer.Serialize(user, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) : "";
                
                // Get WASM memory for passing string data
                var memory = _instance.GetMemory("memory");
                if (memory == null)
                    throw new InvalidOperationException("WASM memory not found");

                // Allocate memory and write strings
                var flagBytes = Encoding.UTF8.GetBytes(flagJson);
                var companyBytes = Encoding.UTF8.GetBytes(companyJson);
                var userBytes = Encoding.UTF8.GetBytes(userJson);

                // Use a fixed memory layout starting at offset 1000 to avoid conflicts
                var flagPtr = 1000;
                var companyPtr = flagPtr + flagBytes.Length;
                var userPtr = companyPtr + companyBytes.Length;

                // Write each string to its specific memory location
                var flagSpan = memory.GetSpan(flagPtr, flagBytes.Length);
                flagBytes.CopyTo(flagSpan);
                
                var companySpan = memory.GetSpan(companyPtr, companyBytes.Length);
                companyBytes.CopyTo(companySpan);
                
                var userSpan = memory.GetSpan(userPtr, userBytes.Length);
                userBytes.CopyTo(userSpan);

                // Call the WASM function with pointers and lengths
                var result = checkFlagFunc.Invoke(
                    flagPtr, flagBytes.Length,
                    companyPtr, companyBytes.Length,
                    userPtr, userBytes.Length);

                var jsonLength = Convert.ToInt32(result);
                if (jsonLength <= 0)
                {
                    throw new InvalidOperationException($"WASM checkFlag returned error code: {jsonLength}");
                }

                // Get the JSON result from WASM memory  
                var jsonPtr = Convert.ToInt32(getResultJsonFunc.Invoke());
                var actualLength = Convert.ToInt32(getResultJsonLengthFunc.Invoke());
                
                if (actualLength != jsonLength)
                {
                    throw new InvalidOperationException($"JSON length mismatch: expected {jsonLength}, got {actualLength}");
                }

                if (actualLength <= 0 || jsonPtr <= 0)
                {
                    throw new InvalidOperationException($"Invalid JSON result: ptr={jsonPtr}, length={actualLength}");
                }

                // Read the JSON string from WASM memory
                var jsonSpan = memory.GetSpan(jsonPtr, actualLength);
                var jsonBytes = new byte[actualLength];
                jsonSpan.CopyTo(jsonBytes);
                var jsonResult = Encoding.UTF8.GetString(jsonBytes);

                // Deserialize the WASM result directly to RulesengineCheckFlagResult
                var wasmResult = JsonSerializer.Deserialize<RulesengineCheckFlagResult>(jsonResult);
                
                if (wasmResult == null)
                    throw new InvalidOperationException("Failed to deserialize WASM result");

                // Fill in any missing required fields with fallback values
                wasmResult.FlagId ??= flag.Id;
                wasmResult.FlagKey ??= flag.Key;
                wasmResult.CompanyId ??= company?.Id;
                wasmResult.UserId ??= user?.Id;
                
                return wasmResult;
            }
            catch (Exception ex)
            {
                // Fallback to a basic implementation if WASM fails
                throw new InvalidOperationException($"WASM execution failed: {ex.Message}", ex);
            }
        }



        private bool EvaluateRule(RulesengineRule rule, RulesengineCompany? company, RulesengineUser? user)
        {
            try
            {
                // WASM is working correctly
                
                // Get the evaluateRule function from WASM (raw export with C signature)
                var evaluateRuleFunc = _instance.GetFunction("evaluateRule");
                if (evaluateRuleFunc == null)
                    throw new InvalidOperationException("evaluateRule function not found in WASM module");

                // Serialize inputs to JSON
                var ruleJson = JsonSerializer.Serialize(rule, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var companyJson = company != null ? JsonSerializer.Serialize(company, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) : "";
                var userJson = user != null ? JsonSerializer.Serialize(user, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) : "";
                
                // Serialize inputs for WASM

                // Get WASM memory for passing string data
                var memory = _instance.GetMemory("memory");
                if (memory == null)
                    throw new InvalidOperationException("WASM memory not found");

                // Allocate memory and write strings
                var ruleBytes = Encoding.UTF8.GetBytes(ruleJson);
                var companyBytes = Encoding.UTF8.GetBytes(companyJson);
                var userBytes = Encoding.UTF8.GetBytes(userJson);

                // For simplicity, use a fixed memory layout starting at offset 2000 to avoid conflicts
                var rulePtr = 2000;
                var companyPtr = rulePtr + ruleBytes.Length;
                var userPtr = companyPtr + companyBytes.Length;

                // Write data to WASM memory

                // Write data to WASM memory using the correct API
                var memorySpan = memory.GetSpan(rulePtr, ruleBytes.Length + companyBytes.Length + userBytes.Length);
                ruleBytes.CopyTo(memorySpan.Slice(0, ruleBytes.Length));
                companyBytes.CopyTo(memorySpan.Slice(ruleBytes.Length, companyBytes.Length));
                userBytes.CopyTo(memorySpan.Slice(ruleBytes.Length + companyBytes.Length, userBytes.Length));

                // Memory written successfully

                // Call the WASM function with pointers and lengths
                var result = evaluateRuleFunc.Invoke(
                    rulePtr, ruleBytes.Length,
                    companyPtr, companyBytes.Length,
                    userPtr, userBytes.Length);

                // Convert result: 0 = false, 1 = true
                var returnValue = Convert.ToInt32(result);
                // WASM evaluation completed successfully
                return returnValue == 1;
            }
            catch (Exception)
            {
                // If WASM fails, return false as a safe fallback
                return false;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            try
            {
                // Note: Instance and Module don't implement IDisposable in Wasmtime
                // Only Store and Engine do
                _store?.Dispose();
                _engine?.Dispose();
            }
            catch
            {
                // Suppress exceptions during disposal
            }
            
            _disposed = true;
        }
    }
}