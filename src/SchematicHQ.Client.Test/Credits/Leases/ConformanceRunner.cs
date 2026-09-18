using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Executes one conformance vector against one store backend. The vectors and
/// the spec they implement are copied verbatim from the reference
/// implementation; this runner is the only language-specific piece.
/// </summary>
public sealed partial class ConformanceRunner
{
    /// <summary>
    /// Credit amounts round-trip through strings in the Redis backend, so
    /// compare with a tolerance rather than for exact bit equality.
    /// </summary>
    private const double Tolerance = 1e-9;

    private readonly string _backend;
    private readonly CrashInjectingLeaseStore _leases;
    private readonly IReservationStore _reservations;
    private readonly ScriptedLeaseWireClient _wire = new();
    private readonly CreditLeaseManager _manager;
    private readonly Dictionary<string, string> _handles = new();

    private DateTime _now = ConformanceVectors.T0;

    public ConformanceRunner(string backend, JsonElement vector)
    {
        _backend = backend;
        LeaseClock clock = () => _now;

        var config = Config(vector);
        var sweepInterval = TimeSpan.FromMilliseconds(config.SweepIntervalMs);

        if (backend == "redis")
        {
            var redis = new FakeLeaseRedis(clock);
            _leases = new CrashInjectingLeaseStore(
                new RedisLeaseStore(redis, clock: clock, defaultLeaseDuration: config.LeaseDuration)
            );
            _reservations = new RedisReservationStore(
                redis,
                _leases,
                sweepInterval: sweepInterval,
                clock: clock
            );
        }
        else
        {
            _leases = new CrashInjectingLeaseStore(new InMemoryLeaseStore(clock));
            _reservations = new InMemoryReservationStore(_leases, sweepInterval, clock);
        }

        Config_ = config;
        _manager = new CreditLeaseManager(
            _wire,
            // The manager gets the backend itself, not the crash decorator, so
            // its release-on-close guard keys off the real store's type.
            _leases.Inner,
            new CreditLeaseConfig
            {
                DefaultLeaseDuration = config.LeaseDuration,
                DefaultReservationTTL = config.ReservationTTL,
                DefaultLeaseSize = config.LeaseSize,
                LowWaterMark = config.LowWaterMark,
            },
            NullLogger.Instance,
            clock
        );
    }

    private VectorConfig Config_ { get; }

    public async Task RunAsync(JsonElement vector)
    {
        if (vector.TryGetProperty("given", out var given))
        {
            await InstallGivenAsync(given).ConfigureAwait(false);
        }

        var index = 0;
        foreach (var op in vector.GetProperty("operations").EnumerateArray())
        {
            var name = op.GetProperty("op").GetString()!;
            try
            {
                await ExecuteAsync(name, op).ConfigureAwait(false);
            }
            catch (Exception ex) when (ex is not AssertionException && !Expects(op, "throws"))
            {
                Assert.Fail(
                    $"operation {index} ({name}) threw unexpectedly on the {_backend} backend: {ex}"
                );
            }
            index++;
        }

        _reservations.Stop();
    }

    private async Task InstallGivenAsync(JsonElement given)
    {
        if (!given.TryGetProperty("leases", out var leases))
        {
            return;
        }
        foreach (var lease in leases.EnumerateArray())
        {
            var written = await _leases
                .ReplaceAsync(
                    new LeaseGrant
                    {
                        LeaseId = Str(lease, "lease_id"),
                        CompanyId = Str(lease, "company_id"),
                        CreditTypeId = Str(lease, "credit_type_id"),
                        GrantedAmount = Num(lease, "granted_amount"),
                        ExpiresAt = At(Num(lease, "expires_at_ms")),
                    }
                )
                .ConfigureAwait(false);
            Assert.That(written, Is.True, "given.leases must install cleanly");
        }
    }

    /// <summary>
    /// The expect keys each operation asserts on. An assertion this runner does
    /// not implement is skipped rather than failed, so a vector carrying a key
    /// from a newer reference implementation would pass while pinning nothing.
    /// Keep an entry here in step with every operation below.
    /// </summary>
    private static readonly Dictionary<string, string[]> RecognizedExpectKeys = new()
    {
        ["advance_clock"] = Array.Empty<string>(),
        ["replace_lease"] = new[] { "written" },
        ["drop_lease"] = Array.Empty<string>(),
        ["try_reserve"] = new[] { "balance", "lease_id" },
        ["refund_lease"] = Array.Empty<string>(),
        ["extend_lease"] = Array.Empty<string>(),
        ["get_lease"] = new[]
        {
            "exists",
            "lease_id",
            "granted_amount",
            "local_remaining_credits",
        },
        ["add_reservation"] = Array.Empty<string>(),
        ["consume_reservation"] = new[] { "consumed" },
        ["get_reservation"] = new[] { "exists" },
        ["reserved_credits"] = new[] { "total" },
        ["reservation_count"] = new[] { "count" },
        ["sweep_expired"] = new[] { "swept" },
        ["acquire_if_needed"] = new[]
        {
            "lease_id",
            "wire_acquires",
            "last_acquire_requested_amount",
            "released_lease_ids",
        },
        ["maybe_extend"] = new[]
        {
            "wire_extends",
            "last_extend_additional_amount",
            "last_extend_lease_id",
        },
        ["release_all_local_leases"] = new[] { "released_lease_ids", "remaining_slots" },
        ["check"] = new[]
        {
            "allowed",
            "reason",
            "err",
            "has_reservation",
            "fallback_called",
            "reservation",
            "engine_calls",
            "wire_extends",
            "last_extend_additional_amount",
        },
        ["track"] = new[] { "settled_locally", "track" },
    };

    /// <summary>
    /// Any operation may be expected to throw, so this key is recognized
    /// everywhere: the catch in <see cref="RunAsync"/> reads it, not the
    /// operation handlers.
    /// </summary>
    private const string ThrowsKey = "throws";

    internal static void AssertExpectKeysRecognized(string name, JsonElement op)
    {
        if (
            Expect(op) is not { } expect
            || expect.ValueKind != JsonValueKind.Object
            || !RecognizedExpectKeys.TryGetValue(name, out var recognized)
        )
        {
            return;
        }
        foreach (var property in expect.EnumerateObject())
        {
            if (property.Name == ThrowsKey || recognized.Contains(property.Name))
            {
                continue;
            }
            Assert.Fail(
                $"the '{name}' operation does not assert on the expect key '{property.Name}', so the vector would pin nothing"
            );
        }
    }

    private async Task ExecuteAsync(string name, JsonElement op)
    {
        AssertExpectKeysRecognized(name, op);

        switch (name)
        {
            case "advance_clock":
                _now = _now.AddMilliseconds(Num(op, "ms"));
                return;

            case "replace_lease":
                {
                    var written = await _leases
                        .ReplaceAsync(
                            new LeaseGrant
                            {
                                LeaseId = Str(op, "lease_id"),
                                CompanyId = Str(op, "company_id"),
                                CreditTypeId = Str(op, "credit_type_id"),
                                GrantedAmount = Num(op, "granted_amount"),
                                ExpiresAt = At(Num(op, "expires_at_ms")),
                            }
                        )
                        .ConfigureAwait(false);
                    if (Expect(op) is { } expect && expect.TryGetProperty("written", out var expected))
                    {
                        Assert.That(written, Is.EqualTo(expected.GetBoolean()));
                    }
                    return;
                }

            case "drop_lease":
                await _leases
                    .DropAsync(Str(op, "company_id"), Str(op, "credit_type_id"))
                    .ConfigureAwait(false);
                return;

            case "try_reserve":
                {
                    var result = await _leases
                        .TryReserveAsync(
                            Str(op, "company_id"),
                            Str(op, "credit_type_id"),
                            Num(op, "credits")
                        )
                        .ConfigureAwait(false);
                    AssertReserve(op, result);
                    return;
                }

            case "refund_lease":
                await _leases
                    .RefundAsync(
                        Str(op, "company_id"),
                        Str(op, "credit_type_id"),
                        Num(op, "credits"),
                        StrOpt(op, "pin_lease_id")
                    )
                    .ConfigureAwait(false);
                return;

            case "extend_lease":
                await _leases
                    .ExtendAsync(
                        Str(op, "company_id"),
                        Str(op, "credit_type_id"),
                        Num(op, "granted_total"),
                        NumOpt(op, "expires_at_ms") is { } expiresAt ? At(expiresAt) : null,
                        StrOpt(op, "pin_lease_id")
                    )
                    .ConfigureAwait(false);
                return;

            case "get_lease":
                {
                    var lease = await _leases
                        .GetAsync(Str(op, "company_id"), Str(op, "credit_type_id"))
                        .ConfigureAwait(false);
                    AssertLease(op, lease);
                    return;
                }

            case "add_reservation":
                await _reservations
                    .AddAsync(
                        new ReservationRecord
                        {
                            Id = Str(op, "id"),
                            LeaseId = Str(op, "lease_id"),
                            CompanyId = Str(op, "company_id"),
                            CreditTypeId = Str(op, "credit_type_id"),
                            EventSubtype = Str(op, "event_subtype"),
                            QuantityReserved = Num(op, "quantity_reserved"),
                            CreditsReserved = Num(op, "credits_reserved"),
                            ConsumptionRate = Num(op, "consumption_rate"),
                            ExpiresAt = At(Num(op, "expires_at_ms")),
                        }
                    )
                    .ConfigureAwait(false);
                return;

            case "consume_reservation":
                {
                    if (BoolOpt(op, "crash_before_refund") == true)
                    {
                        _leases.FailNextRefund = true;
                    }
                    double? consumed;
                    try
                    {
                        consumed = await _reservations
                            .ConsumeAsync(ReservationId(op), Num(op, "credits"))
                            .ConfigureAwait(false);
                    }
                    catch (Exception) when (Expects(op, "throws"))
                    {
                        return;
                    }
                    Assert.That(
                        Expects(op, "throws"),
                        Is.False,
                        "the vector expected this consume to throw"
                    );
                    AssertNullableNumber(op, "consumed", consumed);
                    return;
                }

            case "get_reservation":
                {
                    var reservation = await _reservations
                        .GetAsync(ReservationId(op))
                        .ConfigureAwait(false);
                    if (Expect(op) is { } expect && expect.TryGetProperty("exists", out var exists))
                    {
                        Assert.That(reservation != null, Is.EqualTo(exists.GetBoolean()));
                    }
                    return;
                }

            case "reserved_credits":
                {
                    var total = await _reservations
                        .ReservedCreditsAsync(Str(op, "company_id"), Str(op, "credit_type_id"))
                        .ConfigureAwait(false);
                    if (Expect(op) is { } expect && expect.TryGetProperty("total", out var expected))
                    {
                        Assert.That(total, Is.EqualTo(expected.GetDouble()).Within(Tolerance));
                    }
                    return;
                }

            case "reservation_count":
                {
                    var count = await _reservations.CountAsync().ConfigureAwait(false);
                    if (Expect(op) is { } expect && expect.TryGetProperty("count", out var expected))
                    {
                        Assert.That(count, Is.EqualTo(expected.GetInt64()));
                    }
                    return;
                }

            case "sweep_expired":
                {
                    var swept = await _reservations.SweepExpiredAsync(_now).ConfigureAwait(false);
                    if (Expect(op) is { } expect && expect.TryGetProperty("swept", out var expected))
                    {
                        Assert.That(swept, Is.EqualTo(expected.GetInt32()));
                    }
                    return;
                }

            case "acquire_if_needed":
                {
                    ScriptServer(op, acquire: true);
                    if (op.TryGetProperty("install_during_wire", out var install))
                    {
                        var grant = new LeaseGrant
                        {
                            LeaseId = Str(install, "lease_id"),
                            CompanyId = Str(install, "company_id"),
                            CreditTypeId = Str(install, "credit_type_id"),
                            GrantedAmount = Num(install, "granted_amount"),
                            ExpiresAt = At(Num(install, "expires_at_ms")),
                        };
                        _wire.InstallDuringWire = () => _leases.ReplaceAsync(grant);
                    }

                    var lease = await _manager
                        .AcquireIfNeededAsync(Str(op, "company_id"), Str(op, "credit_type_id"))
                        .ConfigureAwait(false);
                    // The redundant-lease release a lost race fires is unawaited,
                    // so settle the manager's background work before reading the
                    // release log.
                    await _manager.DrainAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                    if (Expect(op) is { } expect)
                    {
                        if (expect.TryGetProperty("lease_id", out var leaseId))
                        {
                            if (leaseId.ValueKind == JsonValueKind.Null)
                            {
                                Assert.That(lease, Is.Null);
                            }
                            else
                            {
                                Assert.That(lease, Is.Not.Null);
                                Assert.That(lease!.LeaseId, Is.EqualTo(leaseId.GetString()));
                            }
                        }
                        if (expect.TryGetProperty("wire_acquires", out var acquires))
                        {
                            Assert.That(_wire.AcquireCount, Is.EqualTo(acquires.GetInt32()));
                        }
                        if (
                            expect.TryGetProperty(
                                "last_acquire_requested_amount",
                                out var requestedAmount
                            )
                        )
                        {
                            Assert.That(
                                _wire.LastAcquireRequestedAmount,
                                Is.EqualTo(requestedAmount.GetDouble()).Within(Tolerance)
                            );
                        }
                        AssertReleasedLeaseIds(expect);
                    }
                    return;
                }

            case "maybe_extend":
                {
                    ScriptServer(op, acquire: false);
                    await _manager
                        .MaybeExtendInBackgroundAsync(
                            Str(op, "company_id"),
                            Str(op, "credit_type_id"),
                            NumOpt(op, "required_credits")
                        )
                        .ConfigureAwait(false);
                    await _manager.DrainAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                    if (Expect(op) is { } expect)
                    {
                        if (expect.TryGetProperty("wire_extends", out var extends))
                        {
                            Assert.That(_wire.ExtendCount, Is.EqualTo(extends.GetInt32()));
                        }
                        if (
                            expect.TryGetProperty(
                                "last_extend_additional_amount",
                                out var additionalAmount
                            )
                        )
                        {
                            Assert.That(
                                _wire.LastExtendAdditionalAmount,
                                Is.EqualTo(additionalAmount.GetDouble()).Within(Tolerance)
                            );
                        }
                        if (expect.TryGetProperty("last_extend_lease_id", out var extendLeaseId))
                        {
                            Assert.That(
                                _wire.LastExtendLeaseId,
                                Is.EqualTo(extendLeaseId.GetString())
                            );
                        }
                    }
                    return;
                }

            case "release_all_local_leases":
                {
                    await _manager.ReleaseAllLocalLeasesAsync().ConfigureAwait(false);
                    if (Expect(op) is { } expect)
                    {
                        AssertReleasedLeaseIds(expect);
                        if (expect.TryGetProperty("remaining_slots", out var remaining))
                        {
                            var lister = (ILeaseLister)_leases.Inner;
                            var slots = await lister.ListAsync().ConfigureAwait(false);
                            Assert.That(slots.Count, Is.EqualTo(remaining.GetInt32()));
                        }
                    }
                    return;
                }

            case "check":
                await ExecuteCheckAsync(op).ConfigureAwait(false);
                return;

            case "track":
                await ExecuteTrackAsync(op).ConfigureAwait(false);
                return;

            default:
                throw new NotSupportedException(
                    $"the conformance runner does not implement the '{name}' operation yet"
                );
        }
    }

    /// <summary>
    /// Installs the vector's scripted server response for the call the next
    /// operation makes. A vector with no <c>server</c> block expects no wire
    /// call, and the cumulative counts are what catch one happening anyway.
    /// </summary>
    private void ScriptServer(JsonElement op, bool acquire)
    {
        if (!op.TryGetProperty("server", out var server))
        {
            return;
        }

        if (server.TryGetProperty("error", out var error))
        {
            if (acquire)
            {
                _wire.NextAcquireError = error.GetString();
            }
            else
            {
                _wire.NextExtendError = error.GetString();
            }
            return;
        }

        var lease = server.GetProperty("lease");
        var grant = new LeaseGrant
        {
            LeaseId = StrOpt(lease, "lease_id") ?? string.Empty,
            // An acquire response names the whole grant; an extend response
            // names the new server-authoritative total.
            GrantedAmount = NumOpt(lease, "granted_amount") ?? Num(lease, "granted_total"),
            ExpiresAt = At(Num(lease, "expires_at_ms")),
        };
        if (acquire)
        {
            _wire.NextAcquire = grant;
        }
        else
        {
            _wire.NextExtend = grant;
        }
    }

    private void AssertReleasedLeaseIds(JsonElement expect)
    {
        if (!expect.TryGetProperty("released_lease_ids", out var released))
        {
            return;
        }
        var expected = released.EnumerateArray().Select(entry => entry.GetString()).ToArray();
        Assert.That(_wire.ReleasedLeaseIds, Is.EqualTo(expected));
    }

    private void AssertReserve(JsonElement op, ReserveResult? result)
    {
        if (Expect(op) is not { } expect)
        {
            return;
        }
        if (expect.TryGetProperty("balance", out var balance))
        {
            if (balance.ValueKind == JsonValueKind.Null)
            {
                Assert.That(result, Is.Null, "the reserve was expected to be refused");
            }
            else
            {
                Assert.That(result, Is.Not.Null, "the reserve was expected to succeed");
                Assert.That(
                    result!.Value.Balance,
                    Is.EqualTo(balance.GetDouble()).Within(Tolerance)
                );
            }
        }
        if (expect.TryGetProperty("lease_id", out var leaseId))
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Value.LeaseId, Is.EqualTo(leaseId.GetString()));
        }
    }

    private void AssertLease(JsonElement op, LeaseState? lease)
    {
        if (Expect(op) is not { } expect)
        {
            return;
        }
        if (expect.TryGetProperty("exists", out var exists))
        {
            Assert.That(lease != null, Is.EqualTo(exists.GetBoolean()));
        }
        if (lease == null)
        {
            return;
        }
        if (expect.TryGetProperty("lease_id", out var leaseId))
        {
            Assert.That(lease.LeaseId, Is.EqualTo(leaseId.GetString()));
        }
        if (expect.TryGetProperty("granted_amount", out var granted))
        {
            Assert.That(lease.GrantedAmount, Is.EqualTo(granted.GetDouble()).Within(Tolerance));
        }
        if (expect.TryGetProperty("local_remaining_credits", out var remaining))
        {
            Assert.That(
                lease.LocalRemainingCredits,
                Is.EqualTo(remaining.GetDouble()).Within(Tolerance)
            );
        }
    }

    private static void AssertNullableNumber(JsonElement op, string field, double? actual)
    {
        if (Expect(op) is not { } expect || !expect.TryGetProperty(field, out var expected))
        {
            return;
        }
        if (expected.ValueKind == JsonValueKind.Null)
        {
            Assert.That(actual, Is.Null);
            return;
        }
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual!.Value, Is.EqualTo(expected.GetDouble()).Within(Tolerance));
    }

    private string ReservationId(JsonElement op)
    {
        var handle = StrOpt(op, "handle");
        if (handle != null)
        {
            Assert.That(
                _handles.ContainsKey(handle),
                Is.True,
                $"no reservation was saved under the handle '{handle}'"
            );
            return _handles[handle];
        }
        return Str(op, "id");
    }

    private DateTime At(double offsetMs) => ConformanceVectors.T0.AddMilliseconds(offsetMs);

    private static VectorConfig Config(JsonElement vector)
    {
        var config = new VectorConfig();
        if (
            !vector.TryGetProperty("given", out var given)
            || !given.TryGetProperty("config", out var raw)
        )
        {
            return config;
        }
        if (NumOpt(raw, "lease_duration_ms") is { } leaseDuration)
        {
            config.LeaseDuration = TimeSpan.FromMilliseconds(leaseDuration);
        }
        if (NumOpt(raw, "reservation_ttl_ms") is { } reservationTtl)
        {
            config.ReservationTTL = TimeSpan.FromMilliseconds(reservationTtl);
        }
        if (NumOpt(raw, "lease_size") is { } leaseSize)
        {
            config.LeaseSize = leaseSize;
        }
        if (NumOpt(raw, "low_water_mark") is { } lowWaterMark)
        {
            config.LowWaterMark = lowWaterMark;
        }
        if (NumOpt(raw, "sweep_interval_ms") is { } sweepInterval)
        {
            config.SweepIntervalMs = sweepInterval;
        }
        return config;
    }

    private static JsonElement? Expect(JsonElement op) =>
        op.TryGetProperty("expect", out var expect) ? expect : null;

    private static bool Expects(JsonElement op, string field) =>
        Expect(op) is { } expect
        && expect.TryGetProperty(field, out var value)
        && value.ValueKind == JsonValueKind.True;

    private static string Str(JsonElement element, string name) =>
        element.GetProperty(name).GetString()!;

    private static string? StrOpt(JsonElement element, string name) =>
        element.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String
            ? value.GetString()
            : null;

    private static double Num(JsonElement element, string name) =>
        element.GetProperty(name).GetDouble();

    private static double? NumOpt(JsonElement element, string name) =>
        element.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.Number
            ? value.GetDouble()
            : null;

    private static bool? BoolOpt(JsonElement element, string name) =>
        element.TryGetProperty(name, out var value)
        && (value.ValueKind == JsonValueKind.True || value.ValueKind == JsonValueKind.False)
            ? value.GetBoolean()
            : null;

    private sealed class VectorConfig
    {
        public TimeSpan LeaseDuration { get; set; } = LeaseDefaults.LeaseDuration;

        public TimeSpan ReservationTTL { get; set; } = LeaseDefaults.ReservationTTL;

        public double LeaseSize { get; set; } = LeaseDefaults.LeaseSize;

        public double LowWaterMark { get; set; } = LeaseDefaults.LowWaterMark;

        /// <summary>
        /// The vectors drive the sweep explicitly, so the background loop is
        /// parked far out rather than racing them.
        /// </summary>
        public double SweepIntervalMs { get; set; } = 3_600_000;
    }
}
