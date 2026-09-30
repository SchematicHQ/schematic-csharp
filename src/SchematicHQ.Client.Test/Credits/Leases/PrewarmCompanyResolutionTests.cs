using System.Net.Http;
using System.Text.Json;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Leases;
using NUnit.Framework;
using WireMock.Server;
using WireMockRequest = WireMock.RequestBuilders.Request;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Which company a prewarm acquires a lease for. Keys are looked up first,
/// whatever they are named, because an account is free to define its own key
/// called "id"; only a lookup that finds nothing falls back to reading a value
/// as Schematic's own company id, by its prefix.
/// </summary>
[TestFixture]
public class PrewarmCompanyResolutionTests
{
    private const string AcquirePath = "/billing/credits/lease";

    private WireMockServer _server = null!;

    [SetUp]
    public void SetUp()
    {
        _server = WireMockServer.Start();
        _server
            .Given(WireMockRequest.Create().WithPath(AcquirePath).UsingPost())
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
                                    id = "lse_1",
                                    company_id = "comp_real",
                                    credit_id = "ct_1",
                                    granted_amount = 1000.0,
                                    tracked_amount = 0.0,
                                    expires_at = DateTime.UtcNow.AddMinutes(5),
                                },
                                @params = new { },
                            }
                        )
                    )
            );
    }

    [TearDown]
    public void TearDown()
    {
        _server.Stop();
        _server.Dispose();
    }

    [Test]
    public async Task An_Account_Defined_Id_Key_Resolves_Through_The_Cache()
    {
        // The account's own identifier happens to live under a key named "id".
        // It is an ordinary entity key, so the lookup decides.
        var schematic = Client(new FakeCompanyCache("id", "acme", "comp_real"));
        try
        {
            await schematic.Prewarm(
                new Dictionary<string, string> { ["id"] = "acme" },
                new[] { "ct_1" }
            );

            Assert.That(AcquiredCompanyIds(), Is.EqualTo(new[] { "comp_real" }));
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task A_Miss_Falls_Back_To_A_Prefixed_Value_Under_Any_Key()
    {
        var schematic = Client(new FakeCompanyCache());
        try
        {
            await schematic.Prewarm(
                new Dictionary<string, string> { ["account_id"] = "comp_1" },
                new[] { "ct_1" }
            );

            Assert.That(AcquiredCompanyIds(), Is.EqualTo(new[] { "comp_1" }));
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task A_Miss_With_No_Schematic_Id_Resolves_Nothing()
    {
        var schematic = Client(new FakeCompanyCache());
        try
        {
            await schematic.Prewarm(
                new Dictionary<string, string> { ["id"] = "acme" },
                new[] { "ct_1" }
            );

            Assert.That(AcquiredCompanyIds(), Is.Empty);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public void The_Prefix_Decides_Which_Value_Is_A_Company_Id()
    {
        Assert.That(
            Schematic.SchematicId(
                new Dictionary<string, string> { ["id"] = "acme", ["org"] = "comp_2" },
                "comp_"
            ),
            Is.EqualTo("comp_2")
        );
        Assert.That(
            Schematic.SchematicId(new Dictionary<string, string> { ["id"] = "acme" }, "comp_"),
            Is.Null
        );
    }

    private string[] AcquiredCompanyIds() =>
        _server
            .LogEntries.Where(entry => entry.RequestMessage.Path == AcquirePath)
            .Select(entry =>
                JsonSerializer
                    .Deserialize<JsonElement>(entry.RequestMessage.Body ?? "{}")
                    .GetProperty("company_id")
                    .GetString()!
            )
            .ToArray();

    private Schematic Client(ICacheProvider cache) =>
        new(
            "sch_test",
            new ClientOptions
            {
                BaseUrl = _server.Url,
                HttpClient = new HttpClient { BaseAddress = new Uri(_server.Url!) },
                MaxRetries = 0,
                CacheProvider = cache,
                UseDatastream = true,
                CreditLeases = new CreditLeaseConfig
                {
                    Mode = CreditLeaseMode.Client,
                    // Cache-only resolution: the socket never comes up in a
                    // test, and waiting on it would only buy a timeout.
                    PrewarmResolveTimeout = TimeSpan.Zero,
                },
            }
        );
}
