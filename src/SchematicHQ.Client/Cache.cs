using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

#nullable enable

namespace SchematicHQ.Client;

public interface ICacheProvider<T>
{
    T? Get(string key);
    void Set(string key, T val, int? ttlOverride = null);
}

public class LocalCache<T> : ICacheProvider<T>
{
    private const int DEFAULT_CACHE_CAPACITY = 1000;
    private const int DEFAULT_CACHE_TTL = 5000; // 5 seconds

    private readonly ConcurrentDictionary<string, CachedItem<T>> _cache;
    private readonly LinkedList<string> _lruList;
    private readonly object _lock = new object();
    private readonly int _maxItems;
    private readonly int _ttl;

    public LocalCache(int maxItems = DEFAULT_CACHE_CAPACITY, int ttl = DEFAULT_CACHE_TTL)
    {
        _cache = new ConcurrentDictionary<string, CachedItem<T>>();
        _lruList = new LinkedList<string>();
        _maxItems = maxItems;
        _ttl = ttl;
    }

    public T? Get(string key)
    {
        if (_maxItems == 0)
            return default;

        if (!_cache.TryGetValue(key, out var item))
            return default;

        if (DateTime.UtcNow > item.Expiration)
        {
            Remove(key);
            return default;
        }

        lock (_lock)
        {
            _lruList.Remove(item.Node);
            _lruList.AddFirst(item.Node);
        }

        return item.Value;
    }

    public void Set(string key, T val, int? ttlOverride = null)
    {
        if (_maxItems == 0)
            return;

        var ttl = ttlOverride ?? _ttl;
        var expiration = DateTime.UtcNow.AddMilliseconds(ttl);

        lock (_lock)
        {
            if (_cache.TryGetValue(key, out var existingItem))
            {
                existingItem.Value = val;
                existingItem.Expiration = expiration;
                _lruList.Remove(existingItem.Node);
                _lruList.AddFirst(existingItem.Node);
            }
            else
            {
                if (_cache.Count >= _maxItems)
                {
                    var lruKey = _lruList.Last!.Value;
                    Remove(lruKey);
                }

                var node = _lruList.AddFirst(key);
                var newItem = new CachedItem<T>(val, expiration, node);
                _cache[key] = newItem;
            }
        }
    }

    private void Remove(string key)
    {
        if (_cache.TryRemove(key, out var removedItem))
        {
            lock (_lock)
            {
                _lruList.Remove(removedItem.Node);
            }
        }
    }
}

public class CachedItem<T>
{
    public T Value { get; set; }
    public DateTime Expiration { get; set; }
    public LinkedListNode<string> Node { get; }

    public CachedItem(T value, DateTime expiration, LinkedListNode<string> node)
    {
        Value = value;
        Expiration = expiration;
        Node = node;
    }
}
