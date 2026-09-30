using System.Net.Http;
using System.Text.Json;
using NUnit.Framework;
using SchematicHQ.Client.Leases;
using WireMock.Server;
using WireMockRequest = WireMock.RequestBuilders.Request;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// A plain check carries the caller's preflight over REST as well as through a
/// local evaluation, so the verdict accounts for the usage about to be
/// recorded. The flag cache is keyed by flag, company and user alone, so a
/// preflighted verdict must never be served from it or written to it.
/// </summary>
[TestFixture]
public class CheckPreflightRestTests
{
    private const string CheckPath = "/flags/inference/check";

    private WireMockServer _server = null!;
    private Schematic _schematic = null!;

    [SetUp]
    public void SetUp()
    {
        _server = WireMockServer.Start();
        _schematic = new Schematic(
            "sch_test",
            new ClientOptions
            {
                BaseUrl = _server.Url,
                HttpClient = new HttpClient { BaseAddress = new Uri(_server.Url!) },
                MaxRetries = 0,
            }
        );
    }

    [TearDown]
    public async Task TearDown()
    {
        await _schematic.Shutdown();
        _server.Stop();
        _server.Dispose();
    }

    [Test]
    public async Task The_Rest_Body_Carries_The_Callers_Preflight()
    {
        StubCheck(value: true);

        await _schematic.Check(
            "inference",
            Company("co_1"),
            options: new CheckOptions { Usage = 2, EventSubtype = "tokens" }
        );

        var preflight = SentBodies().Single().GetProperty("preflight");
        var eventUsage = preflight.GetProperty("event_usage");
        Assert.That(eventUsage.GetProperty("event_subtype").GetString(), Is.EqualTo("tokens"));
        Assert.That(eventUsage.GetProperty("quantity").GetInt64(), Is.EqualTo(2));
    }

    [Test]
    public async Task A_Plain_Check_Sends_No_Preflight_And_Is_Answered_From_The_Cache()
    {
        StubCheck(value: true);

        var first = await _schematic.Check("inference", Company("co_2"));
        var second = await _schematic.Check("inference", Company("co_2"));

        Assert.That(first.Allowed, Is.True);
        Assert.That(second.Allowed, Is.True);

        var bodies = SentBodies();
        Assert.That(bodies, Has.Length.EqualTo(1));
        Assert.That(bodies[0].TryGetProperty("preflight", out _), Is.False);
    }

    [Test]
    public async Task A_Zero_Usage_Sends_No_Preflight_And_Stays_On_The_Cache()
    {
        StubCheck(value: true);

        var first = await _schematic.Check(
            "inference",
            Company("co_4"),
            options: new CheckOptions { Usage = 0 }
        );
        var second = await _schematic.Check(
            "inference",
            Company("co_4"),
            options: new CheckOptions { Usage = 0 }
        );

        Assert.That(first.Allowed, Is.True);
        Assert.That(second.Allowed, Is.True);

        // Zero usage is documented as having no effect, so sending it would
        // only cost the check its cache entry.
        var bodies = SentBodies();
        Assert.That(bodies, Has.Length.EqualTo(1));
        Assert.That(bodies[0].TryGetProperty("preflight", out _), Is.False);
    }

    [Test]
    public async Task A_Preflighted_Check_Neither_Reads_Nor_Writes_The_Cache()
    {
        StubCheck(value: true);

        // Warms the cache with the plain verdict.
        await _schematic.Check("inference", Company("co_3"));

        _server.Reset();
        StubCheck(value: false);

        var preflighted = await _schematic.Check(
            "inference",
            Company("co_3"),
            options: new CheckOptions { Usage = 5 }
        );

        // The cached true is not an answer to "would five more be allowed?", so
        // the check goes to the server and takes its no.
        Assert.That(preflighted.Allowed, Is.False);
        Assert.That(SentBodies(), Has.Length.EqualTo(1));

        var plain = await _schematic.Check("inference", Company("co_3"));

        // And the no it took is not an answer to the plain question either, so
        // the original cached verdict still stands and costs no second call.
        Assert.That(plain.Allowed, Is.True);
        Assert.That(SentBodies(), Has.Length.EqualTo(1));
    }

    private static Dictionary<string, string> Company(string id) =>
        new() { ["id"] = id };

    private void StubCheck(bool value)
    {
        _server
            .Given(WireMockRequest.Create().WithPath(CheckPath).UsingPost())
            .RespondWith(
                WireMockResponse
                    .Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(
                        JsonSerializer.Serialize(
                            new
                            {
                                data = new
                                {
                                    flag = "inference",
                                    reason = "stub",
                                    value,
                                },
                                @params = new { },
                            }
                        )
                    )
            );
    }

    private JsonElement[] SentBodies() =>
        _server
            .LogEntries.Where(entry => entry.RequestMessage.Path == CheckPath)
            .Select(entry => JsonDocument.Parse(entry.RequestMessage.Body!).RootElement)
            .ToArray();
}
