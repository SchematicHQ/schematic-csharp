using System.Net.Http;
using System.Text.Json;
using NUnit.Framework;
using SchematicHQ.Client.Leases;
using WireMock.Server;
using WireMockRequest = WireMock.RequestBuilders.Request;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// What the lease wire calls actually put on the wire. An extend is an
/// increment, so a retry without a key would grant the tranche twice; an
/// acquire is idempotent for an active slot, so it needs none and keeps the
/// default retry policy.
/// </summary>
[TestFixture]
public class LeaseWireClientTests
{
    private WireMockServer _server = null!;
    private Schematic _schematic = null!;
    private ApiLeaseWireClient _wire = null!;

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
        _wire = new ApiLeaseWireClient(_schematic.Credits);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _schematic.Shutdown();
        _server.Stop();
        _server.Dispose();
    }

    [Test]
    public async Task Extend_Sends_An_Idempotency_Key_And_Two_Extends_Send_Different_Keys()
    {
        StubLease("/billing/credits/lease/lse_1/extend", grantedAmount: 2000);

        await _wire.ExtendAsync("lse_1", 1000, DateTime.UtcNow.AddMinutes(5));
        await _wire.ExtendAsync("lse_1", 1000, DateTime.UtcNow.AddMinutes(5));

        var keys = SentBodies("/billing/credits/lease/lse_1/extend")
            .Select(body => body.GetProperty("idempotency_key").GetString())
            .ToArray();

        Assert.That(keys, Has.Length.EqualTo(2));
        Assert.That(keys[0], Is.Not.Null.And.Not.Empty);
        Assert.That(keys[1], Is.Not.EqualTo(keys[0]));
    }

    [Test]
    public async Task Acquire_Sends_No_Idempotency_Key()
    {
        StubLease("/billing/credits/lease", grantedAmount: 1000);

        await _wire.AcquireAsync("co_1", "ct_1", 1000, DateTime.UtcNow.AddMinutes(5));

        var body = SentBodies("/billing/credits/lease").Single();
        // The server hands back the slot's existing active lease rather than
        // opening a second one, so a retry after a lost response is safe
        // without a key.
        Assert.That(body.TryGetProperty("idempotency_key", out _), Is.False);
        Assert.That(body.GetProperty("company_id").GetString(), Is.EqualTo("co_1"));
        Assert.That(body.GetProperty("requested_amount").GetDouble(), Is.EqualTo(1000));
    }

    [Test]
    public async Task An_Extend_Retried_After_A_502_Resends_The_Same_Key()
    {
        var schematic = new Schematic(
            "sch_test",
            new ClientOptions
            {
                BaseUrl = _server.Url,
                HttpClient = new HttpClient { BaseAddress = new Uri(_server.Url!) },
                MaxRetries = 2,
            }
        );
        try
        {
            _server
                .Given(WireMockRequest.Create().WithPath("/billing/credits/lease/lse_2/extend").UsingPut())
                .InScenario("extend-retry")
                .WillSetStateTo("recovered")
                .RespondWith(WireMockResponse.Create().WithStatusCode(502));
            _server
                .Given(WireMockRequest.Create().WithPath("/billing/credits/lease/lse_2/extend").UsingPut())
                .InScenario("extend-retry")
                .WhenStateIs("recovered")
                .RespondWith(LeaseResponse(2000));

            var wire = new ApiLeaseWireClient(schematic.Credits);
            var grant = await wire.ExtendAsync("lse_2", 1000, DateTime.UtcNow.AddMinutes(5));

            var keys = SentBodies("/billing/credits/lease/lse_2/extend")
                .Select(body => body.GetProperty("idempotency_key").GetString())
                .ToArray();

            Assert.That(keys, Has.Length.EqualTo(2));
            // Both attempts of one extend carry the same key, so the server
            // collapses them onto a single grow.
            Assert.That(keys[1], Is.EqualTo(keys[0]));
            Assert.That(grant.GrantedAmount, Is.EqualTo(2000));
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    private void StubLease(string path, double grantedAmount)
    {
        _server
            .Given(WireMockRequest.Create().WithPath(path))
            .RespondWith(LeaseResponse(grantedAmount));
    }

    private static WireMock.ResponseBuilders.IResponseBuilder LeaseResponse(double grantedAmount) =>
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
                            company_id = "co_1",
                            credit_type_id = "ct_1",
                            granted_amount = grantedAmount,
                            tracked_amount = 0,
                            expires_at = "2030-01-01T00:00:00Z",
                            created_at = "2025-01-01T00:00:00Z",
                            updated_at = "2025-01-01T00:00:00Z",
                        },
                        @params = new { },
                    }
                )
            );

    private JsonElement[] SentBodies(string path) =>
        _server
            .LogEntries.Where(entry => entry.RequestMessage.Path == path)
            .Select(entry => JsonDocument.Parse(entry.RequestMessage.Body!).RootElement)
            .ToArray();
}
