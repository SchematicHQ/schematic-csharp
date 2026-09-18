using SchematicHQ.Client.Cache;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// A datastream cache holding at most one company, matched on the key and value
/// the datastream would have indexed it under. It counts the company lookups
/// made through it, which is how a test sees whether a code path consulted the
/// cached company at all.
/// </summary>
public sealed class FakeCompanyCache : ICacheProvider
{
    private readonly string? _keySuffix;
    private readonly string? _companyId;

    public FakeCompanyCache() { }

    public FakeCompanyCache(string key, string value, string companyId)
    {
        _keySuffix = $":{key.ToLowerInvariant()}:{value.ToLowerInvariant()}";
        _companyId = companyId;
    }

    /// <summary>
    /// How many times a caller has asked this cache for a company.
    /// </summary>
    public int CompanyReads { get; private set; }

    public void ResetCounts() => CompanyReads = 0;

    public ValueTask<T?> Get<T>(string key, CancellationToken token = default)
        where T : notnull
    {
        if (key.StartsWith("company:", StringComparison.Ordinal))
        {
            CompanyReads++;
        }
        if (_companyId == null)
        {
            return new ValueTask<T?>(default(T));
        }
        if (typeof(T) == typeof(string) && key.EndsWith(_keySuffix!, StringComparison.Ordinal))
        {
            return new ValueTask<T?>((T)(object)_companyId);
        }
        if (
            typeof(T) == typeof(RulesengineCompany)
            && key.EndsWith(":" + _companyId, StringComparison.Ordinal)
        )
        {
            return new ValueTask<T?>(
                (T)
                    (object)
                        new RulesengineCompany
                        {
                            Id = _companyId,
                            AccountId = "acct_1",
                            EnvironmentId = "env_1",
                        }
            );
        }
        return new ValueTask<T?>(default(T));
    }

    public ValueTask Set<T>(
        string key,
        T val,
        TimeSpan? ttlOverride = null,
        CancellationToken token = default
    )
        where T : notnull => default;

    public async ValueTask<T> GetOrSet<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? ttlOverride = null,
        CancellationToken token = default
    )
        where T : notnull => await factory(token).ConfigureAwait(false);

    public ValueTask<bool> Delete(string key, CancellationToken token = default) =>
        new ValueTask<bool>(false);

    public ValueTask DeleteMissing(IEnumerable<string> keys, string? scanPattern = null) => default;
}
