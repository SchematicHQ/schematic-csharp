using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.RulesEngine;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// The slice of the datastream client a lease-bearing check touches. Narrow on
/// purpose: it keeps the lease code off the wider datastream surface and lets
/// the conformance runner script the flow without a socket.
/// </summary>
public interface ICheckDataStream
{
    /// <summary>
    /// Reads a flag from the local cache. Null when it is not there.
    /// </summary>
    Task<RulesengineFlag?> GetFlagAsync(string flagKey);

    /// <summary>
    /// Resolves company keys, cache first, then over the wire.
    /// </summary>
    Task<RulesengineCompany?> GetCompanyAsync(Dictionary<string, string> keys);

    /// <summary>
    /// Resolves user keys, cache first, then over the wire.
    /// </summary>
    Task<RulesengineUser?> GetUserAsync(Dictionary<string, string> keys);

    /// <summary>
    /// Runs the rules engine. A null preflight means no preflight.
    /// </summary>
    Task<CheckFlagResult> EvaluateAsync(
        RulesengineFlag flag,
        RulesengineCompany company,
        RulesengineUser? user,
        PreflightRequestBody? preflight
    );
}

/// <summary>
/// Adapts the datastream client to <see cref="ICheckDataStream"/>.
/// </summary>
public sealed class DatastreamCheckSource : ICheckDataStream
{
    private readonly DatastreamClientAdapter _datastream;

    public DatastreamCheckSource(DatastreamClientAdapter datastream)
    {
        _datastream = datastream ?? throw new ArgumentNullException(nameof(datastream));
    }

    public async Task<RulesengineFlag?> GetFlagAsync(string flagKey) =>
        await _datastream.GetCachedFlag(flagKey).ConfigureAwait(false);

    public Task<RulesengineCompany?> GetCompanyAsync(Dictionary<string, string> keys) =>
        _datastream.ResolveCompany(keys);

    public Task<RulesengineUser?> GetUserAsync(Dictionary<string, string> keys) =>
        _datastream.ResolveUser(keys);

    public Task<CheckFlagResult> EvaluateAsync(
        RulesengineFlag flag,
        RulesengineCompany company,
        RulesengineUser? user,
        PreflightRequestBody? preflight
    ) => _datastream.Evaluate(company, user, flag, preflight);
}
