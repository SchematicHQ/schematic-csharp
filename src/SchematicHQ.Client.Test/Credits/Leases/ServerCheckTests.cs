using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Server mode: one check-and-reserve call answers the flag and takes the hold.
/// The conformance vectors cover the client flow on both store backends; server
/// mode has no store to drive, so it is pinned here.
/// </summary>
[TestFixture]
public class ServerCheckTests
{
    private const string FlagKey = "inference";

    private ScriptedServerReservationClient _client = null!;
    private bool _fellBack;
    private bool _flagDefault;

    [SetUp]
    public void SetUp()
    {
        _client = new ScriptedServerReservationClient();
        _fellBack = false;
        _flagDefault = false;
    }

    [Test]
    public async Task Returns_A_Server_Mode_Handle_Built_From_The_Response()
    {
        _client.NextResponse = Allowed(Held());

        var result = await CheckAsync(new CheckOptions { Usage = 10, EventSubtype = "inference_tokens" });

        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation, Is.Not.Null);
        Assert.That(result.Reservation!.Mode, Is.EqualTo(CreditLeaseMode.Server));
        Assert.That(result.Reservation.Id, Is.EqualTo("res_1"));
        // No lease exists in server mode, so the id is mirrored rather than left
        // empty for code that reads it.
        Assert.That(result.Reservation.LeaseId, Is.EqualTo("res_1"));
        Assert.That(result.Reservation.CreditsReserved, Is.EqualTo(100));
        Assert.That(result.Reservation.EventSubtype, Is.EqualTo("inference_tokens"));

        Assert.That(_client.LastFlagKey, Is.EqualTo(FlagKey));
        Assert.That(_client.LastBody!.Quantity, Is.EqualTo(10));
        Assert.That(_client.LastBody.Preflight!.EventUsage!.EventSubtype, Is.EqualTo("inference_tokens"));
        Assert.That(_client.LastBody.Preflight.EventUsage.Quantity, Is.EqualTo(10));
        // A hold is a side effect, so the request carries a key that collapses a
        // retried attempt onto the same hold.
        Assert.That(_client.LastBody.IdempotencyKey, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task Sends_A_Fresh_Idempotency_Key_Per_Check()
    {
        _client.NextResponse = Allowed(Held());
        await CheckAsync(new CheckOptions { Usage = 10, EventSubtype = "inference_tokens" });
        var first = _client.LastBody!.IdempotencyKey;

        _client.NextResponse = Allowed(Held());
        await CheckAsync(new CheckOptions { Usage = 10, EventSubtype = "inference_tokens" });

        Assert.That(_client.LastBody!.IdempotencyKey, Is.Not.EqualTo(first));
    }

    [Test]
    public async Task Sends_The_Generic_Usage_Preflight_Without_An_Event_Subtype()
    {
        _client.NextResponse = Allowed(Held());

        await CheckAsync(new CheckOptions { Usage = 7 });

        Assert.That(_client.LastBody!.Preflight!.EventUsage, Is.Null);
        Assert.That(_client.LastBody.Preflight.Usage, Is.EqualTo(7));
    }

    [Test]
    public async Task Denies_Without_A_Handle_When_Credits_Are_Insufficient()
    {
        _client.NextResponse = new CheckAndReserveFlagResponseData
        {
            Flag = FlagKey,
            Reason = ServerCheck.InsufficientCreditsReason,
            Value = false,
        };

        var result = await CheckAsync(new CheckOptions { Usage = 10 });

        Assert.That(result.Allowed, Is.False);
        Assert.That(result.Reservation, Is.Null);
        Assert.That(result.Reason, Is.EqualTo(ServerCheck.InsufficientCreditsReason));
    }

    [Test]
    public async Task Allows_Without_A_Handle_When_The_Feature_Is_Not_Credit_Metered()
    {
        _client.NextResponse = new CheckAndReserveFlagResponseData
        {
            Flag = FlagKey,
            Reason = "plan_entitlement",
            Value = true,
        };

        var result = await CheckAsync(new CheckOptions { Usage = 10 });

        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation, Is.Null);
    }

    [Test]
    public async Task Treats_A_402_As_A_Definitive_Denial_Even_When_Failing_Open()
    {
        _client.NextError = new PaymentRequiredError(new ApiError { Error = "no credits" });

        var result = await CheckAsync(
            new CheckOptions { Usage = 10, OnAcquireFailure = OnAcquireFailure.FailOpen }
        );

        Assert.That(result.Allowed, Is.False);
        Assert.That(result.Reason, Is.EqualTo(ServerCheck.InsufficientCreditsReason));
        Assert.That(result.Error, Is.EqualTo("no credits"));
    }

    [Test]
    public async Task Fails_Closed_When_The_Call_Errors()
    {
        _client.NextError = new InvalidOperationException("wire down");

        var result = await CheckAsync(new CheckOptions { Usage = 10 });

        Assert.That(result.Allowed, Is.False);
        Assert.That(result.Reason, Is.EqualTo("server_reservation_failed"));
        Assert.That(result.Error, Is.EqualTo("server_reservation_failed"));
    }

    [Test]
    public async Task Fails_Open_To_The_Per_Check_Default_When_The_Call_Errors()
    {
        _client.NextError = new InvalidOperationException("wire down");

        var result = await CheckAsync(
            new CheckOptions
            {
                Usage = 10,
                OnAcquireFailure = OnAcquireFailure.FailOpen,
                DefaultValue = true,
            }
        );

        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reason, Is.EqualTo("server_reservation_failed_fail_open"));
        Assert.That(result.Error, Is.EqualTo("server_reservation_failed"));
    }

    [Test]
    public async Task Fails_Open_To_The_Client_Flag_Default_When_No_Default_Is_Passed()
    {
        _flagDefault = true;
        _client.NextError = new InvalidOperationException("wire down");

        var result = await CheckAsync(
            new CheckOptions { Usage = 10, OnAcquireFailure = OnAcquireFailure.FailOpen }
        );

        Assert.That(result.Allowed, Is.True);
    }

    [Test]
    public async Task Falls_Back_To_A_Plain_Check_For_Zero_Usage()
    {
        var result = await CheckAsync(new CheckOptions { Usage = 0 });

        Assert.That(_fellBack, Is.True);
        Assert.That(_client.CheckCount, Is.EqualTo(0));
        Assert.That(result.Reservation, Is.Null);
    }

    [Test]
    public async Task Resolves_An_Invalid_Usage_Without_Calling_The_API()
    {
        var result = await CheckAsync(new CheckOptions { Usage = double.NaN });

        Assert.That(_client.CheckCount, Is.EqualTo(0));
        Assert.That(_fellBack, Is.False);
        Assert.That(result.Allowed, Is.False);
        Assert.That(result.Reason, Is.EqualTo("invalid_usage"));
    }

    [Test]
    public async Task Releases_A_Hold_That_Names_No_Event_Subtype()
    {
        _client.NextResponse = Allowed(Held(eventSubtype: null));

        var result = await CheckAsync(new CheckOptions { Usage = 10 });

        Assert.That(_client.ReleasedReservationIds, Is.EqualTo(new[] { "res_1" }));
        Assert.That(result.Allowed, Is.False);
        Assert.That(result.Reason, Is.EqualTo("missing_event_subtype"));
    }

    [Test]
    public async Task Keeps_The_Server_Verdict_For_An_Unsettleable_Hold_When_Failing_Open()
    {
        _client.NextResponse = Allowed(Held(eventSubtype: null));

        var result = await CheckAsync(
            new CheckOptions { Usage = 10, OnAcquireFailure = OnAcquireFailure.FailOpen }
        );

        Assert.That(_client.ReleasedReservationIds, Is.EqualTo(new[] { "res_1" }));
        // The server already evaluated and allowed; only the settle is
        // impossible, so its verdict stands rather than the caller's default.
        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation, Is.Null);
        Assert.That(result.Error, Is.EqualTo("missing_event_subtype"));
    }

    private Task<CheckResult> CheckAsync(CheckOptions options) =>
        ServerCheck.CheckWithServerReservationAsync(
            new ServerCheckDeps
            {
                Client = _client,
                Logger = NullLogger.Instance,
                ReservationTTL = TimeSpan.FromSeconds(60),
                GetDefault = () => options.DefaultValue ?? _flagDefault,
            },
            FlagKey,
            new Dictionary<string, string> { ["id"] = "co_1" },
            null,
            options,
            () =>
            {
                _fellBack = true;
                return Task.FromResult(
                    new CheckResult
                    {
                        Allowed = true,
                        Value = true,
                        Reason = "fallback",
                        FlagKey = FlagKey,
                    }
                );
            }
        );

    private static CheckAndReserveFlagResponseData Allowed(
        FlagCheckReservationResponseData reservation
    ) =>
        new()
        {
            Flag = FlagKey,
            Reason = "credit_balance",
            Value = true,
            Reservation = reservation,
        };

    private static FlagCheckReservationResponseData Held(
        string? eventSubtype = "inference_tokens"
    ) =>
        new()
        {
            Id = "res_1",
            CompanyId = "co_1",
            CreditTypeId = "ct_1",
            ConsumptionRate = 10,
            CreditsReserved = 100,
            QuantityReserved = 10,
            EventSubtype = eventSubtype,
            ExpiresAt = DateTime.UtcNow.AddMinutes(1),
        };
}
