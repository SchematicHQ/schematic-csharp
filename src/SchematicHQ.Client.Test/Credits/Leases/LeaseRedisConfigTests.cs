using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Leases;
using StackExchange.Redis;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// How the lease backend reads a Redis configuration. It has to accept every
/// shape the cache accepts, or a caller gets a working cache and leases that
/// quietly fall back to per-process stores, which is the cross-pod gating they
/// configured Redis for in the first place.
/// </summary>
[TestFixture]
public class LeaseRedisConfigTests
{
    [Test]
    public void The_Individual_Fields_Are_Read_The_Same_Way_The_Cache_Reads_Them()
    {
#pragma warning disable CS0618 // the individual fields predate ConfigurationOptions
        var config = new RedisCacheConfig
        {
            Endpoints = new List<string> { "127.0.0.1:1" },
            Password = "hunter2",
            Ssl = true,
            ClientName = "leases",
        };
#pragma warning restore CS0618

        var options = StackExchangeLeaseRedis.BuildOptions(config);

        Assert.That(options.EndPoints, Has.Count.EqualTo(1));
        Assert.That(options.Password, Is.EqualTo("hunter2"));
        Assert.That(options.Ssl, Is.True);
        Assert.That(options.ClientName, Is.EqualTo("leases"));
    }

    [Test]
    public void A_Connection_String_Is_Parsed()
    {
        var options = StackExchangeLeaseRedis.BuildOptions(
            new RedisCacheConfig { Configuration = "127.0.0.1:1,name=leases" }
        );

        Assert.That(options.EndPoints, Has.Count.EqualTo(1));
        Assert.That(options.ClientName, Is.EqualTo("leases"));
    }

    [Test]
    public void The_Connection_Never_Aborts_Because_Redis_Is_Down_Right_Now()
    {
        var caller = new ConfigurationOptions { AbortOnConnectFail = true };
        caller.EndPoints.Add("127.0.0.1", 1);

        var options = StackExchangeLeaseRedis.BuildOptions(
            new RedisCacheConfig { ConfigurationOptions = caller }
        );

        // The lease backend is built once, at construction, so a throw there
        // leaves the client on per-process stores for its whole life.
        Assert.That(options.AbortOnConnectFail, Is.False);
        // And the caller's own object is left as they set it.
        Assert.That(caller.AbortOnConnectFail, Is.True);
    }

    [Test]
    public void A_Client_Configured_With_Only_Endpoints_Still_Gates_Across_Pods()
    {
        using var logs = new RecordingLoggerFactory();
#pragma warning disable CS0618
        var redisConfig = new RedisCacheConfig
        {
            Endpoints = new List<string> { "127.0.0.1:1" },
            ConnectTimeout = 50,
            ConnectRetry = 0,
        };
#pragma warning restore CS0618

        var schematic = new Schematic(
            "sch_test",
            new ClientOptions
            {
                EventBuffer = new RecordingEventBuffer(),
                LoggerFactory = logs,
                CacheConfiguration = new CacheConfiguration { RedisConfig = redisConfig },
                CreditLeases = new CreditLeaseConfig { Mode = CreditLeaseMode.Client },
            }
        );

        try
        {
            // Nothing answers on that endpoint, but the backend is still the
            // shared one: it reconnects when Redis comes back, where a fallback
            // to per-process stores would never recover.
            Assert.That(logs.Logged(LogLevel.Warning, "without a shared Redis backend"), Is.False);
        }
        finally
        {
            schematic.Shutdown().GetAwaiter().GetResult();
        }
    }
}
