using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// The client wiring around the lease flows: what a settle puts on the wire,
/// and the startup warnings that are the only outward sign of a lease setup
/// that will not do what the caller expects.
/// </summary>
[TestFixture]
public class SchematicCreditLeaseTests
{
    private const string IdempotencyPrefix = "lease-reservation:";

    private RecordingEventBuffer _events = null!;
    private RecordingLoggerFactory _logs = null!;

    [SetUp]
    public void SetUp()
    {
        _events = new RecordingEventBuffer();
        _logs = new RecordingLoggerFactory();
    }

    [TearDown]
    public void TearDown() => _logs.Dispose();

    [Test]
    public async Task A_Settle_Carries_The_Lease_Id_And_A_Deterministic_Idempotency_Key()
    {
        var schematic = Client();
        try
        {
            await schematic.TrackWithReservation(Record(CreditLeaseMode.Client), 4);

            var track = _events.Tracks.Single();
            Assert.That(track.Event, Is.EqualTo("inference_tokens"));
            Assert.That(track.Quantity, Is.EqualTo(4));
            Assert.That(track.LeaseId, Is.EqualTo("lse_1"));
            Assert.That(track.ReservationId, Is.Null);
            Assert.That(
                _events.Events.Single().IdempotencyKey,
                Is.EqualTo(IdempotencyPrefix + "res_1")
            );
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task A_Server_Handle_Settles_By_Reservation_Id()
    {
        var schematic = Client();
        try
        {
            await schematic.TrackWithReservation(Record(CreditLeaseMode.Server), 4);

            var track = _events.Tracks.Single();
            Assert.That(track.ReservationId, Is.EqualTo("res_1"));
            Assert.That(track.LeaseId, Is.Null);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task A_Settle_Without_A_Reservation_Bills_Nothing()
    {
        var schematic = Client();
        try
        {
            // A check allows without a hold in several ordinary cases, and
            // callers pass the result straight through, so this must not throw.
            await schematic.TrackWithReservation(null, 4);

            Assert.That(_events.Events, Is.Empty);
            Assert.That(_logs.Logged(LogLevel.Error, "without a reservation"), Is.True);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task An_Invalid_Quantity_Skips_The_Settle_Entirely()
    {
        var schematic = Client();
        try
        {
            await schematic.TrackWithReservation(Record(CreditLeaseMode.Client), double.NaN);
            await schematic.TrackWithReservation(Record(CreditLeaseMode.Client), -1);

            // The untouched hold is refunded at its TTL, so nothing is lost and
            // nothing bogus is billed.
            Assert.That(_events.Events, Is.Empty);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task Track_Threads_The_Lease_And_Reservation_Ids_From_Its_Options()
    {
        var schematic = Client();
        try
        {
            schematic.Track(
                "inference_tokens",
                quantity: 3,
                options: new TrackOptions { LeaseId = "lse_9", ReservationId = "res_9" }
            );

            var track = _events.Tracks.Single();
            Assert.That(track.LeaseId, Is.EqualTo("lse_9"));
            Assert.That(track.ReservationId, Is.EqualTo("res_9"));
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task An_Offline_Client_Bills_Nothing_And_Says_So_At_Startup()
    {
        var schematic = Client(offline: true);
        try
        {
            await schematic.TrackWithReservation(Record(CreditLeaseMode.Client), 4);

            Assert.That(_events.Events, Is.Empty);
            Assert.That(_logs.Logged(LogLevel.Warning, "offline mode"), Is.True);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task Auto_Without_Datastream_Resolves_To_Server_Mode()
    {
        var schematic = Client(
            new CreditLeaseConfig { DefaultLeaseSize = 5000, LowWaterMark = 0.5 }
        );
        try
        {
            Assert.That(_logs.Logged(LogLevel.Information, "server mode"), Is.True);
            // The knobs that only steer the local plumbing would silently do
            // nothing, so the client says so once.
            Assert.That(_logs.Logged(LogLevel.Warning, "DefaultLeaseSize, LowWaterMark"), Is.True);

            // No local lease to warm, and no throw for asking.
            await schematic.Prewarm(new Dictionary<string, string> { ["id"] = "co_1" }, new[] { "ct_1" });
            Assert.That(_events.Events, Is.Empty);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task Prewarm_Without_A_Datastream_Resolves_Nothing_Rather_Than_Throwing()
    {
        // Client mode builds the local plumbing even with no datastream, so a
        // prewarm reaches the company resolver. With only secondary keys there
        // is no cache to read and nothing to fetch over, so it has to give up
        // quietly.
        var schematic = Client(new CreditLeaseConfig { Mode = CreditLeaseMode.Client });
        try
        {
            Assert.DoesNotThrowAsync(
                () =>
                    schematic.Prewarm(
                        new Dictionary<string, string> { ["email"] = "wcoyote@acme.net" },
                        new[] { "ct_1" }
                    )
            );
            Assert.That(_events.Events, Is.Empty);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task A_Datastream_That_Cannot_Start_Leaves_The_Client_Gating_Server_Side()
    {
        // A base URL the stream cannot parse. Failing the constructor over it
        // would take down a client REST can still answer for, so the stream is
        // dropped instead, and the auto mode has to see that.
        var schematic = new Schematic(
            "sch_test",
            new ClientOptions
            {
                EventBuffer = _events,
                LoggerFactory = _logs,
                UseDatastream = true,
                BaseUrl = "not a url",
                CreditLeases = new CreditLeaseConfig(),
            }
        );
        try
        {
            Assert.That(
                _logs.Logged(LogLevel.Error, "Failed to start the datastream client"),
                Is.True
            );
            Assert.That(_logs.Logged(LogLevel.Information, "server mode"), Is.True);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task Client_Mode_Without_Datastream_Warns_That_Usage_Is_Ignored()
    {
        var schematic = Client(new CreditLeaseConfig { Mode = CreditLeaseMode.Client });
        try
        {
            Assert.That(_logs.Logged(LogLevel.Warning, "NO credit gating"), Is.True);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public async Task A_Reservation_TTL_Above_The_API_Cap_Is_Clamped_In_Server_Mode()
    {
        var schematic = Client(
            new CreditLeaseConfig
            {
                Mode = CreditLeaseMode.Server,
                DefaultReservationTTL = TimeSpan.FromHours(2),
            }
        );
        try
        {
            Assert.That(_logs.Logged(LogLevel.Warning, "longer than the API will hold"), Is.True);
        }
        finally
        {
            await schematic.Shutdown();
        }
    }

    [Test]
    public void Per_Credit_Overrides_Beat_The_Client_Wide_Values()
    {
        var config = new CreditLeaseConfig
        {
            DefaultLeaseSize = 5000,
            LowWaterMark = 0.5,
            Overrides = new Dictionary<string, LeaseOverride>
            {
                ["ct_1"] = new() { DefaultLeaseSize = 25 },
            },
        };

        var overridden = config.Resolve("ct_1");
        Assert.That(overridden.LeaseSize, Is.EqualTo(25));
        // A field the override leaves unset keeps the client-wide value.
        Assert.That(overridden.LowWaterMark, Is.EqualTo(0.5));
        Assert.That(overridden.LeaseDuration, Is.EqualTo(LeaseDefaults.LeaseDuration));

        var other = config.Resolve("ct_2");
        Assert.That(other.LeaseSize, Is.EqualTo(5000));
    }

    private Schematic Client(CreditLeaseConfig? leases = null, bool offline = false) =>
        new(
            "sch_test",
            new ClientOptions
            {
                EventBuffer = _events,
                LoggerFactory = _logs,
                Offline = offline,
                CreditLeases = leases ?? new CreditLeaseConfig(),
            }
        );

    private static ReservationRecord Record(CreditLeaseMode mode) =>
        new()
        {
            Id = "res_1",
            LeaseId = mode == CreditLeaseMode.Server ? "res_1" : "lse_1",
            Mode = mode,
            CompanyId = "co_1",
            CreditTypeId = "ct_1",
            EventSubtype = "inference_tokens",
            QuantityReserved = 10,
            CreditsReserved = 100,
            ConsumptionRate = 10,
            ExpiresAt = DateTime.UtcNow.AddMinutes(1),
            Company = new Dictionary<string, string> { ["id"] = "co_1" },
        };
}
