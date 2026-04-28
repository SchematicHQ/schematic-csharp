using SchematicHQ.Client.Cache;

namespace SchematicHQ.Client.Datastream;

// Datastream uses the same cache, but it has different default TTL
internal class DatastreamCacheDecorator : ICacheProvider
{
    private readonly ICacheProvider _inner;
    private readonly TimeSpan _cacheTtl;

    public DatastreamCacheDecorator(ICacheProvider inner, TimeSpan cacheTtl)
    {
        _inner = inner;
        _cacheTtl = cacheTtl;
    }

    public ValueTask<T?> Get<T>(string key)
    {
        return _inner.Get<T>(key);
    }

    public ValueTask Set<T>(string key, T val, TimeSpan? ttlOverride = null)
    {
        return _inner.Set(key, val, ttlOverride ?? _cacheTtl);
    }

    public ValueTask<bool> Delete(string key)
    {
        return _inner.Delete(key);
    }

    public ValueTask DeleteMissing(IEnumerable<string> keys, string? scanPattern = null)
    {
        return _inner.DeleteMissing(keys, scanPattern);
    }
}