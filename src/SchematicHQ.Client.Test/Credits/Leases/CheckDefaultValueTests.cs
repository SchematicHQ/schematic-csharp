using System.Net.Http;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// What a check answers when it cannot reach the API. A plain check falls back
/// to the REST flag check, and the caller's default is what that call is
/// supposed to return when it fails, not only when the client is offline.
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
