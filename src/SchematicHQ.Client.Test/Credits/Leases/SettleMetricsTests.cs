using System.Net.Http;
using NUnit.Framework;
using SchematicHQ.Client.Leases;
using WireMock.Server;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Whether a settle moves the cached company metric. The bump is a local
/// prediction of what the stream will push back, so it belongs only to a settle
/// that moved local state with it.
/// </summary>
[TestFixture]
public class SettleMetricsTests
{
    private WireMockServer _server = null!;
    private FakeCompanyCache _cache = null!;
    private Schematic _schematic = null!;

    [SetUp]
    public void SetUp()
    {
        _server = WireMockServer.Start();
        _cache = new FakeCompanyCache("id", "comp_1", "comp_1");
        _schematic = new Schematic(
            "sch_test",
            new ClientOptions
            {
                BaseUrl = _server.Url,
                HttpClient = new HttpClient { BaseAddress = new Uri(_server.Url!) },
                MaxRetries = 0,
                CacheProvider = _cache,
                UseDatastream = true,
                CreditLeases = new CreditLeaseConfig { Mode = CreditLeaseMode.Client },
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
    public async Task A_Settle_That_Did_Not_Land_Locally_Leaves_The_Metric_Alone()
    {
        // An ordinary track predicts the usage it records, so it moves the
        // cached metric.
        _schematic.Track(
            "inference_tokens",
            company: new Dictionary<string, string> { ["id"] = "comp_1" },
            quantity: 20
        );
        Assert.That(_cache.CompanyReads, Is.GreaterThan(0));

        // This reservation was never in the store, so the settle changes
        // nothing locally and the server drops the event on its idempotency
        // key. Bumping the metric for it would deny the company's next
        // numeric-limit check on usage nobody recorded.
        _cache.ResetCounts();
        await _schematic.TrackWithReservation(Reservation(), 20);

        Assert.That(_cache.CompanyReads, Is.Zero);
    }

    private static ReservationRecord Reservation() =>
        new()
        {
            Id = "res_1",
            LeaseId = "lse_1",
            Mode = CreditLeaseMode.Client,
            CompanyId = "comp_1",
            CreditTypeId = "ct_1",
            EventSubtype = "inference_tokens",
            QuantityReserved = 20,
            CreditsReserved = 200,
            ConsumptionRate = 10,
            ExpiresAt = DateTime.UtcNow.AddMinutes(1),
            Company = new Dictionary<string, string> { ["id"] = "comp_1" },
        };
}
