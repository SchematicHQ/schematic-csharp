using System.Globalization;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// In-memory implementation of the slice of Redis the lease and reservation
/// stores use. <see cref="EvalAsync"/> interprets the specific Lua scripts
/// those stores send rather than hosting a Lua engine: just enough to exercise
/// the atomic semantics the vectors pin.
///
/// <para>Atomicity: every script path below runs synchronously under one
/// monitor, so calling them from concurrent tasks emulates real Redis script
/// atomicity faithfully enough for these tests.</para>
///
/// <para>The real scripts read the current time from the Redis server clock
/// (redis.call('TIME')). This fake is in-process, so it reads the same virtual
/// clock the stores and the runner share.</para>
/// </summary>
public sealed class FakeLeaseRedis : ILeaseRedis
{
    private readonly object _gate = new();
    private readonly Dictionary<string, Dictionary<string, string>> _hashes = new();
    private readonly Dictionary<string, Dictionary<string, double>> _zsets = new();
    private readonly Dictionary<string, long> _expirations = new();
    private readonly LeaseClock _clock;

    public FakeLeaseRedis(LeaseClock? clock = null)
    {
        _clock = clock ?? (() => DateTime.UtcNow);
    }

    private long NowMs => LeaseTime.ToUnixMilliseconds(_clock());

    public Task<IReadOnlyDictionary<string, string>> HashGetAllAsync(string key)
    {
        lock (_gate)
        {
            ExpireIfDue(key);
            IReadOnlyDictionary<string, string> result = _hashes.TryGetValue(key, out var hash)
                ? new Dictionary<string, string>(hash)
                : new Dictionary<string, string>();
            return Task.FromResult(result);
        }
    }

    public Task<string?> HashGetAsync(string key, string field)
    {
        lock (_gate)
        {
            return Task.FromResult(HashGet(key, field));
        }
    }

    public Task HashSetAsync(string key, IReadOnlyList<KeyValuePair<string, string>> entries)
    {
        lock (_gate)
        {
            foreach (var entry in entries)
            {
                HashSet(key, entry.Key, entry.Value);
            }
            return Task.CompletedTask;
        }
    }

    public Task HashDeleteAsync(string key, string field)
    {
        lock (_gate)
        {
            if (_hashes.TryGetValue(key, out var hash))
            {
                hash.Remove(field);
            }
            return Task.CompletedTask;
        }
    }

    public Task KeyDeleteAsync(string key)
    {
        lock (_gate)
        {
            Delete(key);
            return Task.CompletedTask;
        }
    }

    public Task KeyExpireAtAsync(string key, long unixTimeMilliseconds)
    {
        lock (_gate)
        {
            _expirations[key] = unixTimeMilliseconds;
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Both writes under one monitor, matching the MULTI/EXEC the real backend
    /// sends: an observer never sees the hash without its expiry.
    /// </summary>
    public Task HashSetWithExpiryAsync(
        string key,
        IReadOnlyList<KeyValuePair<string, string>> entries,
        long unixTimeMilliseconds
    )
    {
        lock (_gate)
        {
            if (FailNextHashSetWithExpiry)
            {
                FailNextHashSetWithExpiry = false;
                throw new InvalidOperationException("scripted hash-with-expiry failure");
            }
            foreach (var entry in entries)
            {
                HashSet(key, entry.Key, entry.Value);
            }
            _expirations[key] = unixTimeMilliseconds;
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Makes the next combined write fail as a unit, the way a dropped
    /// connection mid-transaction does.
    /// </summary>
    public bool FailNextHashSetWithExpiry { get; set; }

    public Task SortedSetAddAsync(string key, string member, double score)
    {
        lock (_gate)
        {
            if (!_zsets.TryGetValue(key, out var zset))
            {
                zset = new Dictionary<string, double>();
                _zsets[key] = zset;
            }
            zset[member] = score;
            return Task.CompletedTask;
        }
    }

    public Task SortedSetRemoveAsync(string key, string member)
    {
        lock (_gate)
        {
            if (_zsets.TryGetValue(key, out var zset))
            {
                zset.Remove(member);
            }
            return Task.CompletedTask;
        }
    }

    public Task<IReadOnlyList<string>> SortedSetRangeByScoreAsync(
        string key,
        double min,
        double max,
        long skip,
        long take
    )
    {
        lock (_gate)
        {
            ExpireIfDue(key);
            if (!_zsets.TryGetValue(key, out var zset))
            {
                return Task.FromResult<IReadOnlyList<string>>(Array.Empty<string>());
            }
            var members = zset.Where(pair => pair.Value >= min && pair.Value <= max)
                .OrderBy(pair => pair.Value)
                .ThenBy(pair => pair.Key, StringComparer.Ordinal)
                .Skip((int)skip)
                .Take((int)take)
                .Select(pair => pair.Key)
                .ToArray();
            return Task.FromResult<IReadOnlyList<string>>(members);
        }
    }

    public Task<long> SortedSetLengthAsync(string key)
    {
        lock (_gate)
        {
            ExpireIfDue(key);
            return Task.FromResult(
                _zsets.TryGetValue(key, out var zset) ? (long)zset.Count : 0L
            );
        }
    }

    public Task<object?> EvalAsync(string script, string[] keys, string[] args)
    {
        lock (_gate)
        {
            return Task.FromResult(Interpret(script, keys, args));
        }
    }

    private object? Interpret(string script, string[] keys, string[] args)
    {
        // Each script is matched by a stable substring unique to it. All of
        // them are single-key (KEYS[1] only), mirroring the real stores, which
        // avoid multi-key Lua so they stay Cluster-safe.
        if (script.Contains("local raw = redis.call('HGETALL', KEYS[1])"))
        {
            return Claim(keys[0]);
        }
        if (script.Contains("if existing_id and existing_expiry > now then"))
        {
            return Replace(keys[0], args);
        }
        if (script.Contains("if remaining < requested then return false end"))
        {
            return TryReserve(keys[0], args);
        }
        if (script.Contains("local raw_remaining ="))
        {
            return Refund(keys[0], args);
        }
        if (script.Contains("local raw_granted ="))
        {
            return Extend(keys[0], args);
        }
        throw new NotSupportedException("FakeLeaseRedis received an unrecognized script");
    }

    private object Replace(string key, string[] args)
    {
        var newId = args[0];
        var newGranted = args[1];
        var newExpiry = ParseNumber(args[2]);
        var grace = ParseNumber(args[3]);
        var now = NowMs;

        var existingId = HashGet(key, "leaseId");
        var existingExpiry = ParseNumber(HashGet(key, "expiresAt") ?? "0");

        if (existingId != null && existingExpiry > now)
        {
            return 0L;
        }

        if (existingId == newId)
        {
            var granted = ParseNumber(HashGet(key, "grantedAmount") ?? "0");
            var add = ParseNumber(newGranted) - granted;
            if (add > 0)
            {
                var remaining = ParseNumber(HashGet(key, "localRemainingCredits") ?? "0");
                HashSet(key, "grantedAmount", newGranted);
                HashSet(key, "localRemainingCredits", Text(remaining + add));
            }
            if (newExpiry > existingExpiry)
            {
                HashSet(key, "expiresAt", args[2]);
                _expirations[key] = (long)(newExpiry + grace);
            }
            return 0L;
        }

        Delete(key);
        HashSet(key, "leaseId", newId);
        HashSet(key, "companyId", args[4]);
        HashSet(key, "creditTypeId", args[5]);
        HashSet(key, "grantedAmount", newGranted);
        HashSet(key, "localRemainingCredits", newGranted);
        HashSet(key, "expiresAt", args[2]);
        _expirations[key] = (long)(newExpiry + grace);
        return 1L;
    }

    private object? TryReserve(string key, string[] args)
    {
        var raw = HashGet(key, "localRemainingCredits");
        if (raw == null)
        {
            return null;
        }
        var leaseId = HashGet(key, "leaseId");
        if (leaseId == null)
        {
            return null;
        }
        var expiry = ParseNumber(HashGet(key, "expiresAt") ?? "0");
        if (expiry <= NowMs)
        {
            return null;
        }
        var remaining = ParseNumber(raw);
        var requested = ParseNumber(args[0]);
        if (remaining < requested)
        {
            return null;
        }
        var newRemaining = remaining - requested;
        HashSet(key, "localRemainingCredits", Text(newRemaining));
        return new[] { Text(newRemaining), leaseId };
    }

    private object Refund(string key, string[] args)
    {
        var rawRemaining = HashGet(key, "localRemainingCredits");
        if (rawRemaining == null)
        {
            return 0L;
        }
        var requiredLease = args[1];
        if (requiredLease.Length > 0 && HashGet(key, "leaseId") != requiredLease)
        {
            return 0L;
        }
        var remaining = ParseNumber(rawRemaining);
        var granted = ParseNumber(HashGet(key, "grantedAmount") ?? "0");
        var newBalance = remaining + ParseNumber(args[0]);
        if (newBalance > granted)
        {
            newBalance = granted;
        }
        HashSet(key, "localRemainingCredits", Text(newBalance));
        return 1L;
    }

    private object Extend(string key, string[] args)
    {
        var rawGranted = HashGet(key, "grantedAmount");
        if (rawGranted == null)
        {
            return 0L;
        }
        var requiredLease = args[3];
        if (requiredLease.Length > 0 && HashGet(key, "leaseId") != requiredLease)
        {
            return 0L;
        }
        var granted = ParseNumber(rawGranted);
        var target = ParseNumber(args[0]);
        var add = target - granted;
        if (add > 0)
        {
            var remaining = ParseNumber(HashGet(key, "localRemainingCredits") ?? "0");
            HashSet(key, "grantedAmount", Text(target));
            HashSet(key, "localRemainingCredits", Text(remaining + add));
        }
        var newExpiry = ParseNumber(args[1]);
        var grace = ParseNumber(args[2]);
        var currentExpiry = ParseNumber(HashGet(key, "expiresAt") ?? "0");
        if (newExpiry > currentExpiry)
        {
            HashSet(key, "expiresAt", args[1]);
            _expirations[key] = (long)(newExpiry + grace);
        }
        return 1L;
    }

    private object? Claim(string key)
    {
        ExpireIfDue(key);
        if (!_hashes.TryGetValue(key, out var hash) || hash.Count == 0)
        {
            return null;
        }
        var flat = new List<string>(hash.Count * 2);
        foreach (var entry in hash)
        {
            flat.Add(entry.Key);
            flat.Add(entry.Value);
        }
        Delete(key);
        return flat.ToArray();
    }

    private string? HashGet(string key, string field)
    {
        ExpireIfDue(key);
        return _hashes.TryGetValue(key, out var hash) && hash.TryGetValue(field, out var value)
            ? value
            : null;
    }

    private void HashSet(string key, string field, string value)
    {
        if (!_hashes.TryGetValue(key, out var hash))
        {
            hash = new Dictionary<string, string>();
            _hashes[key] = hash;
        }
        hash[field] = value;
    }

    private void Delete(string key)
    {
        _hashes.Remove(key);
        _zsets.Remove(key);
        _expirations.Remove(key);
    }

    private void ExpireIfDue(string key)
    {
        if (_expirations.TryGetValue(key, out var expiry) && expiry <= NowMs)
        {
            Delete(key);
        }
    }

    private static double ParseNumber(string value) =>
        double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed)
            ? parsed
            : 0;

    /// <summary>
    /// Renders a number the way the real scripts do. Redis runs Lua 5.1, whose
    /// tostring writes %.14g, so a balance that round-trips through a script
    /// loses everything past 14 significant digits. Formatting to full
    /// precision here would let the fake allow reserves real Redis refuses, and
    /// refuse ones it allows.
    /// </summary>
    private static string Text(double value) =>
        value.ToString("G14", CultureInfo.InvariantCulture);
}
