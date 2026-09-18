using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// The narrow slice of Redis the lease and reservation stores use. It is an
/// interface rather than StackExchange.Redis's own so a caller can point lease
/// state at a Redis this SDK did not open, and so tests can drive the stores
/// without a server.
/// </summary>
public interface ILeaseRedis
{
    Task<IReadOnlyDictionary<string, string>> HashGetAllAsync(string key);

    Task<string?> HashGetAsync(string key, string field);

    Task HashSetAsync(string key, IReadOnlyList<KeyValuePair<string, string>> entries);

    /// <summary>
    /// Writes a hash and sets its expiry as one unit. Splitting them leaves a
    /// window where a process that dies in between has written a key with no
    /// TTL, which nothing reaps and, since the indexes are written after, which
    /// nothing points at either.
    /// </summary>
    Task HashSetWithExpiryAsync(
        string key,
        IReadOnlyList<KeyValuePair<string, string>> entries,
        long unixTimeMilliseconds
    );

    Task HashDeleteAsync(string key, string field);

    Task KeyDeleteAsync(string key);

    Task SortedSetAddAsync(string key, string member, double score);

    Task SortedSetRemoveAsync(string key, string member);

    Task<IReadOnlyList<string>> SortedSetRangeByScoreAsync(
        string key,
        double min,
        double max,
        long skip,
        long take
    );

    Task<long> SortedSetLengthAsync(string key);

    /// <summary>
    /// Runs a Lua script. The reply is normalized to the three shapes the
    /// stores' scripts return: null for a nil reply, <see cref="long"/> for an
    /// integer, and <c>string[]</c> for a multi-bulk.
    /// </summary>
    Task<object?> EvalAsync(string script, string[] keys, string[] args);
}
