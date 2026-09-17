using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Pins each Lua script to its SHA1.
///
/// <para>The scripts are the one piece of this SDK that has to be byte-identical
/// across every language: a mixed fleet shares one lease hash per slot, and each
/// pod EVALs its own copy of the text. A script that quietly lost its expiry
/// guard or its lease pin would still serve every conformance vector, because
/// the vectors run against a fake Redis that interprets the scripts rather than
/// executing them. Hashing the text is what actually catches the drift.</para>
///
/// <para>These hashes are the SHA1 of the reference implementation's script
/// bodies. A failure here means either a real edit to a script, in which case
/// every SDK has to move together and these values change in lockstep, or an
/// accidental edit, in which case revert it.</para>
/// </summary>
[TestFixture]
public class RedisScriptFingerprintTests
{
    [Test]
    [TestCase("LeaseNowMs", "536f9f925aa2c2a5ac1351b839dd77447b2c602c")]
    [TestCase("ReplaceScript", "96e6830dd5664bee5700e30d64f04b1726b37c3f")]
    [TestCase("TryReserveScript", "53d744e6e38eca7fc26462fd7e8b5e70bbdfb642")]
    [TestCase("RefundScript", "43873b5f916a0ec012d9377af400e1ecc0b3ec0d")]
    [TestCase("ExtendScript", "842913369460707b70475b6853d996687878b7eb")]
    [TestCase("ClaimScript", "b89cdd478995f79d7fd6e5534d434ca08d214c6c")]
    public void Script_Text_Matches_The_Reference_Implementation(string name, string expected)
    {
        Assert.That(Sha1(Script(name)), Is.EqualTo(expected), $"{name} has drifted");
    }

    [Test]
    public void Every_Script_Reading_A_Balance_Guards_On_The_Server_Clock()
    {
        // A second line of defence in plain sight: the two scripts that serve a
        // balance read the Redis clock rather than the caller's, so pods with
        // skewed clocks cannot disagree about whether a lease is still live.
        foreach (var name in new[] { "ReplaceScript", "TryReserveScript" })
        {
            Assert.That(Script(name), Does.Contain("redis.call('TIME')"), name);
            Assert.That(Script(name), Does.Contain("expiry"), name);
        }
    }

    private static string Script(string name) =>
        name switch
        {
            "LeaseNowMs" => RedisLeaseStore.LeaseNowMs,
            "ReplaceScript" => RedisLeaseStore.ReplaceScript,
            "TryReserveScript" => RedisLeaseStore.TryReserveScript,
            "RefundScript" => RedisLeaseStore.RefundScript,
            "ExtendScript" => RedisLeaseStore.ExtendScript,
            "ClaimScript" => RedisReservationStore.ClaimScript,
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, "unknown script"),
        };

    private static string Sha1(string script) =>
        Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(script))).ToLowerInvariant();
}
