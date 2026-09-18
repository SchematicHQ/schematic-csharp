using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.Cache;
using System.Net.Http;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// What a check answers when nothing can evaluate it. A plain check falls back
/// to the REST flag check, and the caller's default is what that call is
/// supposed to return when it fails, not only when the client is offline.
///
/// <para>The same holds on the datastream branch, where the engine reports a
/// refusal to evaluate as the flag's registered default under one of two
/// reasons rather than by throwing. Telling that apart from a real verdict is
/// what decides whether the caller's default applies.</para>
/// </summary>
[TestFixture]
public class CheckDefaultValueTests
{
    // Nothing listens here, so the flag check fails rather than answering.
    private const string Unreachable = "http://127.0.0.1:1";

    [Test]
    public async Task An_Unreachable_Api_Answers_With_The_Callers_Default()
    {
        var schematic = Client();
        try
        {
            var allowed = await schematic.Check(
                "inference",
                new Dictionary<string, string> { ["id"] = "co_1" },
                options: new CheckOptions { DefaultValue = true }
            );

            Assert.That(allowed.Allowed, Is.True);
            Assert.That(allowed.Value, Is.True);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task Without_A_Default_An_Unreachable_Api_Denies()
    {
        var schematic = Client();
        try
        {
            var denied = await schematic.Check(
                "inference",
                new Dictionary<string, string> { ["id"] = "co_1" }
            );

            Assert.That(denied.Allowed, Is.False);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public void A_Verdict_The_Engine_Reached_Is_Not_A_Decline()
    {
        // The engine answering false must never be read as a decline, or every
        // denied check would come back as the caller's default.
        Assert.That(
            Schematic.EngineDeclined(
                new CheckFlagResult
                {
                    FlagKey = "inference",
                    Value = false,
                    Reason = "No rules matched; default value for flag",
                }
            ),
            Is.False
        );
    }

    [TestCase("RULES_ENGINE_UNAVAILABLE")]
    [TestCase("RULES_ENGINE_ERROR")]
    public void An_Engine_That_Could_Not_Evaluate_Is_A_Decline(string reason)
    {
        // Neither of these is a verdict. The engine reports them with the flag's
        // registered default in Value, which is why the caller's own default has
        // to be resolved instead of trusting what came back.
        Assert.That(
            Schematic.EngineDeclined(
                new CheckFlagResult
                {
                    FlagKey = "inference",
                    Value = false,
                    Reason = reason,
                }
            ),
            Is.True
        );
    }

    [Test]
    public void An_Engine_That_Reported_An_Error_Is_A_Decline()
    {
        Assert.That(
            Schematic.EngineDeclined(
                new CheckFlagResult
                {
                    FlagKey = "inference",
                    Value = false,
                    Reason = "boom",
                    Error = new Exception("boom"),
                }
            ),
            Is.True
        );
    }

    [Test]
    public async Task A_Verdict_The_Engine_Reached_Wins_Over_The_Callers_Default()
    {
        // The datastream branch answers most checks, so a default applied there
        // unconditionally would override every real verdict. This drives that
        // branch end to end with the flag and the company both cached, which is
        // what sends it straight to the engine.
        var cache = new FlagAndCompanyCache("inference", "comp_1");
        var schematic = new Schematic(
            "sch_test",
            new ClientOptions
            {
                BaseUrl = Unreachable,
                HttpClient = new HttpClient
                {
                    BaseAddress = new Uri(Unreachable),
                    Timeout = TimeSpan.FromSeconds(2),
                },
                MaxRetries = 0,
                CacheProvider = cache,
                UseDatastream = true,
                EventBuffer = new RecordingEventBuffer(),
            }
        );
        try
        {
            var allowed = await schematic.Check(
                "inference",
                new Dictionary<string, string> { ["id"] = "comp_1" },
                options: new CheckOptions { DefaultValue = true }
            );

            // The engine answered, so its verdict stands and the caller's
            // default is not consulted. The other half of the branch, where the
            // engine declines, cannot be driven from here: the WASM engine is
            // built inside the datastream client with no seam to disable it, so
            // the predicate that decides it is pinned directly below.
            Assert.That(allowed.Reason, Is.EqualTo("No rules matched; default value for flag"));
            Assert.That(allowed.Value, Is.False);
            Assert.That(allowed.Allowed, Is.False);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    /// <summary>
    /// A datastream cache holding one flag and one company, enough for the
    /// adapter to consider every resource present and go straight to the engine.
    /// </summary>
    private sealed class FlagAndCompanyCache : ICacheProvider
    {
        private readonly string _flagKey;
        private readonly string _companyId;

        public FlagAndCompanyCache(string flagKey, string companyId)
        {
            _flagKey = flagKey;
            _companyId = companyId;
        }

        public ValueTask<T?> Get<T>(string key, CancellationToken token = default)
            where T : notnull
        {
            // The cache key carries a schema version between the prefix and the
            // name, so match the ends rather than the whole string.
            if (
                typeof(T) == typeof(RulesengineFlag)
                && key.EndsWith(":" + _flagKey, StringComparison.Ordinal)
            )
            {
                return new ValueTask<T?>(
                    (T)
                        (object)
                            new RulesengineFlag
                            {
                                AccountId = "acct_1",
                                EnvironmentId = "env_1",
                                Id = "flag_1",
                                Key = _flagKey,
                                DefaultValue = false,
                            }
                );
            }
            if (
                typeof(T) == typeof(string)
                && key.EndsWith(":id:" + _companyId, StringComparison.Ordinal)
            )
            {
                return new ValueTask<T?>((T)(object)_companyId);
            }
            if (
                typeof(T) == typeof(RulesengineCompany)
                && key.EndsWith(":" + _companyId, StringComparison.Ordinal)
            )
            {
                return new ValueTask<T?>(
                    (T)
                        (object)
                            new RulesengineCompany
                            {
                                Id = _companyId,
                                AccountId = "acct_1",
                                EnvironmentId = "env_1",
                            }
                );
            }
            return new ValueTask<T?>(default(T));
        }

        public ValueTask Set<T>(
            string key,
            T val,
            TimeSpan? ttlOverride = null,
            CancellationToken token = default
        )
            where T : notnull => default;

        public async ValueTask<T> GetOrSet<T>(
            string key,
            Func<CancellationToken, Task<T>> factory,
            TimeSpan? ttlOverride = null,
            CancellationToken token = default
        )
            where T : notnull => await factory(token).ConfigureAwait(false);

        public ValueTask<bool> Delete(string key, CancellationToken token = default) =>
            new ValueTask<bool>(false);

        public ValueTask DeleteMissing(IEnumerable<string> keys, string? scanPattern = null) =>
            default;
    }

    private static Schematic Client() =>
        new(
            "sch_test",
            new ClientOptions
            {
                BaseUrl = Unreachable,
                HttpClient = new HttpClient
                {
                    BaseAddress = new Uri(Unreachable),
                    Timeout = TimeSpan.FromSeconds(2),
                },
                MaxRetries = 0,
                EventBuffer = new RecordingEventBuffer(),
            }
        );
}
