using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchematicHQ.Client.Datastream;
using StackExchange.Redis;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Backs <see cref="ILeaseRedis"/> with StackExchange.Redis, the same client
/// the SDK's Redis cache uses.
/// </summary>
public sealed class StackExchangeLeaseRedis : ILeaseRedis, IDisposable
{
    private readonly IDatabase _db;
    private readonly IConnectionMultiplexer? _owned;

    public StackExchangeLeaseRedis(IDatabase db)
        : this(db, null) { }

    private StackExchangeLeaseRedis(IDatabase db, IConnectionMultiplexer? owned)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _owned = owned;
    }

    /// <summary>
    /// Whether this instance opened the connection behind it, and so is the one
    /// that has to close it. A multiplexer a caller's factory handed back is
    /// shared with whatever else that factory serves, so it stays the caller's
    /// to close even though the SDK is the one that asked for it.
    /// </summary>
    internal bool OwnsConnection => _owned != null;

    /// <summary>
    /// The connection this instance opened, or null when it is using one that
    /// belongs to the caller.
    /// </summary>
    internal IConnectionMultiplexer? OwnedConnection => _owned;

    /// <summary>
    /// Opens a connection from the same configuration shape the datastream
    /// Redis cache takes, so lease state can reuse an existing Redis setup.
    /// A factory supplies a connection the caller already manages; a
    /// configuration string or options object makes a second one, which this
    /// instance then owns and closes on <see cref="Dispose"/>.
    /// </summary>
    public static StackExchangeLeaseRedis FromConfig(RedisCacheConfig config)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        IConnectionMultiplexer multiplexer;
        var owned = true;
        if (config.ConnectionMultiplexerFactory != null)
        {
            multiplexer = config.ConnectionMultiplexerFactory.Invoke();
            owned = false;
        }
        else if (config.ConfigurationOptions != null)
        {
            multiplexer = ConnectionMultiplexer.Connect(config.ConfigurationOptions);
        }
        else if (config.Configuration != null)
        {
            multiplexer = ConnectionMultiplexer.Connect(config.Configuration);
        }
        else
        {
            throw new ArgumentException(
                "Redis configuration needs one of ConnectionMultiplexerFactory, ConfigurationOptions or Configuration",
                nameof(config)
            );
        }

        return new StackExchangeLeaseRedis(
            multiplexer.GetDatabase(config.Database),
            owned ? multiplexer : null
        );
    }

    /// <summary>
    /// Closes the connection, if this instance is the one that opened it.
    /// </summary>
    public void Dispose() => _owned?.Dispose();

    public async Task<IReadOnlyDictionary<string, string>> HashGetAllAsync(string key)
    {
        var entries = await _db.HashGetAllAsync(key).ConfigureAwait(false);
        var result = new Dictionary<string, string>(entries.Length);
        foreach (var entry in entries)
        {
            if (!entry.Name.IsNull && !entry.Value.IsNull)
            {
                result[entry.Name!] = entry.Value!;
            }
        }
        return result;
    }

    public async Task<string?> HashGetAsync(string key, string field)
    {
        var value = await _db.HashGetAsync(key, field).ConfigureAwait(false);
        return value.IsNull ? null : (string?)value;
    }

    public Task HashSetAsync(string key, IReadOnlyList<KeyValuePair<string, string>> entries)
    {
        var hashEntries = entries
            .Select(entry => new HashEntry(entry.Key, entry.Value))
            .ToArray();
        return _db.HashSetAsync(key, hashEntries);
    }

    public async Task HashSetWithExpiryAsync(
        string key,
        IReadOnlyList<KeyValuePair<string, string>> entries,
        long unixTimeMilliseconds
    )
    {
        // One MULTI/EXEC instead of two round trips. Both commands name the
        // same key, so this stays Cluster-safe, and it needs no Lua: the script
        // set has to stay byte-identical to the other SDKs' so a mixed fleet
        // shares leases.
        var transaction = _db.CreateTransaction();
        var set = transaction.HashSetAsync(
            key,
            entries.Select(entry => new HashEntry(entry.Key, entry.Value)).ToArray()
        );
        var expire = transaction.KeyExpireAsync(
            key,
            DateTimeOffset.FromUnixTimeMilliseconds(unixTimeMilliseconds).UtcDateTime
        );
        await transaction.ExecuteAsync().ConfigureAwait(false);
        await Task.WhenAll(set, expire).ConfigureAwait(false);
    }

    public Task HashDeleteAsync(string key, string field) => _db.HashDeleteAsync(key, field);

    public Task KeyDeleteAsync(string key) => _db.KeyDeleteAsync(key);

    public Task KeyExpireAtAsync(string key, long unixTimeMilliseconds) =>
        _db.KeyExpireAsync(key, DateTimeOffset.FromUnixTimeMilliseconds(unixTimeMilliseconds).UtcDateTime);

    public Task SortedSetAddAsync(string key, string member, double score) =>
        _db.SortedSetAddAsync(key, member, score);

    public Task SortedSetRemoveAsync(string key, string member) =>
        _db.SortedSetRemoveAsync(key, member);

    public async Task<IReadOnlyList<string>> SortedSetRangeByScoreAsync(
        string key,
        double min,
        double max,
        long skip,
        long take
    )
    {
        var members = await _db.SortedSetRangeByScoreAsync(
                key,
                min,
                max,
                Exclude.None,
                Order.Ascending,
                skip,
                take
            )
            .ConfigureAwait(false);
        return members.Where(member => !member.IsNull).Select(member => (string)member!).ToArray();
    }

    public Task<long> SortedSetLengthAsync(string key) => _db.SortedSetLengthAsync(key);

    public async Task<object?> EvalAsync(string script, string[] keys, string[] args)
    {
        var result = await _db.ScriptEvaluateAsync(
                script,
                keys.Select(key => (RedisKey)key).ToArray(),
                args.Select(arg => (RedisValue)arg).ToArray()
            )
            .ConfigureAwait(false);

        if (result.IsNull)
        {
            return null;
        }
        if (result.Resp2Type == ResultType.MultiBulk)
        {
            return ((RedisValue[])result!)
                .Select(value => value.IsNull ? string.Empty : (string)value!)
                .ToArray();
        }
        return (long)result;
    }
}
