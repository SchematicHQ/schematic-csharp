using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Keeps credit holds in this process only. Swap in
/// <see cref="RedisReservationStore"/> for cross-pod deployments; both
/// implement <see cref="IReservationStore"/>.
/// </summary>
public sealed class InMemoryReservationStore : IReservationStore
{
    private readonly object _gate = new();
    private readonly Dictionary<string, ReservationRecord> _reservations = new();
    private readonly ILeaseStore _leaseStore;
    private readonly TimeSpan _sweepInterval;
    private readonly LeaseClock _clock;

    private CancellationTokenSource? _sweepCancellation;
    private bool _stopped;

    public InMemoryReservationStore(
        ILeaseStore leaseStore,
        TimeSpan? sweepInterval = null,
        LeaseClock? clock = null
    )
    {
        _leaseStore = leaseStore;
        _sweepInterval = sweepInterval ?? LeaseDefaults.SweepInterval;
        _clock = clock ?? (() => DateTime.UtcNow);
    }

    public Task AddAsync(ReservationRecord reservation)
    {
        var stored = reservation.Copy();
        // Redis has no field for the mode, so a record read back from it is
        // always a client-mode one. Drop it here too, so the two backends are
        // interchangeable and nothing comes to depend on a round trip carrying
        // it. A server-mode hold lives on the server and never reaches a store.
        stored.Mode = CreditLeaseMode.Client;
        lock (_gate)
        {
            _reservations[reservation.Id] = stored;
        }
        return Task.CompletedTask;
    }

    public Task<ReservationRecord?> GetAsync(string id)
    {
        lock (_gate)
        {
            return Task.FromResult(
                _reservations.TryGetValue(id, out var record) ? record.Copy() : null
            );
        }
    }

    public async Task<double?> ConsumeAsync(string id, double creditsConsumed)
    {
        ReservationRecord? claimed;
        lock (_gate)
        {
            if (!_reservations.TryGetValue(id, out claimed))
            {
                return null;
            }
            _reservations.Remove(id);
        }

        var consumed = Math.Max(0, Math.Min(creditsConsumed, claimed.CreditsReserved));
        var refund = claimed.CreditsReserved - consumed;
        if (refund > 0)
        {
            // Pinned to the originating lease: if that lease has expired and a
            // successor occupies the slot, the refund is dropped, because the
            // expired lease's remainder was already returned server-side.
            await _leaseStore
                .RefundAsync(claimed.CompanyId, claimed.CreditTypeId, refund, claimed.LeaseId)
                .ConfigureAwait(false);
        }
        return consumed;
    }

    public Task<double> ReservedCreditsAsync(string companyId, string creditTypeId)
    {
        lock (_gate)
        {
            double total = 0;
            foreach (var record in _reservations.Values)
            {
                if (record.CompanyId == companyId && record.CreditTypeId == creditTypeId)
                {
                    total += record.CreditsReserved;
                }
            }
            return Task.FromResult(total);
        }
    }

    public async Task<int> SweepExpiredAsync(DateTime? now = null)
    {
        var cutoff = now ?? _clock();
        List<string> expired;
        lock (_gate)
        {
            expired = new List<string>();
            foreach (var pair in _reservations)
            {
                if (pair.Value.ExpiresAt <= cutoff)
                {
                    expired.Add(pair.Key);
                }
            }
        }

        var swept = 0;
        foreach (var id in expired)
        {
            // Consuming zero is the cancel path: it claims the record and
            // refunds the whole hold, so the sweeper shares the exactly-once
            // arbitration with a racing settle.
            if (await ConsumeAsync(id, 0).ConfigureAwait(false) != null)
            {
                swept++;
            }
        }
        return swept;
    }

    public void StartSweep()
    {
        CancellationToken token;
        lock (_gate)
        {
            if (_sweepCancellation != null || _stopped)
            {
                return;
            }
            _sweepCancellation = new CancellationTokenSource();
            // Take the token here, inside the lock. A Stop right after this
            // cancels the source and disposes it, and reading Token off a
            // disposed source throws; a token taken beforehand survives, already
            // cancelled, so the loop below sees that and exits at once.
            token = _sweepCancellation.Token;
        }

        _ = Task.Run(
            async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(_sweepInterval, token).ConfigureAwait(false);
                        await SweepExpiredAsync().ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    catch
                    {
                        // Swallow so one bad sweep does not end the loop.
                    }
                }
            },
            token
        );
    }

    public void Stop()
    {
        CancellationTokenSource? cancellation;
        lock (_gate)
        {
            _stopped = true;
            cancellation = _sweepCancellation;
            _sweepCancellation = null;
        }
        cancellation?.Cancel();
        cancellation?.Dispose();
    }

    public Task<long> CountAsync()
    {
        lock (_gate)
        {
            return Task.FromResult((long)_reservations.Count);
        }
    }

    public void Dispose() => Stop();
}
