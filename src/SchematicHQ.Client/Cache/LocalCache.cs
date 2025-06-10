using System.Collections.Concurrent;

#nullable enable

namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// A thread-safe in-memory cache implementation with LRU eviction policy
    /// </summary>
    /// <typeparam name="T">Type of values stored in the cache</typeparam>
    public class LocalCache<T> : ICacheProvider<T>
    {
        public const int DEFAULT_CACHE_CAPACITY = 1000;
        public static readonly TimeSpan DEFAULT_CACHE_TTL = TimeSpan.FromMilliseconds(5000); // 5000 milliseconds
        public static readonly TimeSpan UNLIMITED_TTL = TimeSpan.MaxValue;
        private readonly ConcurrentDictionary<string, CachedItem<T>> _cache;
        private readonly LinkedList<string> _lruList;
        private readonly object _lock = new object();
        private readonly int _maxItems;
        private readonly TimeSpan _ttl;

        /// <summary>
        /// Creates a new LocalCache instance
        /// </summary>
        /// <param name="maxItems">Maximum number of items to cache</param>
        /// <param name="ttl">Default time-to-live for cached items</param>
        public LocalCache(int maxItems = DEFAULT_CACHE_CAPACITY, TimeSpan? ttl = null)
        {
            _cache = new ConcurrentDictionary<string, CachedItem<T>>();
            _lruList = new LinkedList<string>();
            _maxItems = maxItems;
            _ttl = ttl ?? DEFAULT_CACHE_TTL;
        }

        /// <inheritdoc/>
        public T? Get(string key)
        {
            if (_maxItems == 0)
                return default;

            if (!_cache.TryGetValue(key, out var item))
                return default;

            if (item.Expiration != DateTime.MaxValue && DateTime.UtcNow > item.Expiration)
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

        /// <inheritdoc/>
        public void Set(string key, T val, TimeSpan? ttlOverride = null)
        {
            if (_maxItems == 0)
                return;

            var ttl = ttlOverride ?? _ttl;
            // Determine expiration time - use MaxValue for unlimited TTL
            DateTime expiration = ttl == UNLIMITED_TTL
                ? DateTime.MaxValue
                : DateTime.UtcNow.Add(ttl);

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
                    while (_cache.Count >= _maxItems)
                    {
                        var lruKey = _lruList.Last!.Value;
                        Remove(lruKey);
                    }

                    var node = _lruList.AddFirst(key);
                    var newItem = new CachedItem<T>(val, expiration, node);
                    if (!_cache.TryAdd(key, newItem))
                    {
                        // If we couldn't add it, clean up the linked list node
                        _lruList.Remove(node);
                    }
                }
            }
        }

        /// <inheritdoc/>
        public bool Delete(string key)
        {
            if (_maxItems == 0)
                return false;

            Remove(key);
            return true;
        }

        /// <inheritdoc/>
        public void DeleteMissing(IEnumerable<string> keys)
        {
            if (_maxItems == 0)
                return;

            var keysSet = new HashSet<string>(keys);
            var keysToRemove = new List<string>();

            // Collect keys to remove (those not in the provided list)
            lock (_lock)
            {
                foreach (var cacheKey in _cache.Keys)
                {
                    if (!keysSet.Contains(cacheKey))
                    {
                        keysToRemove.Add(cacheKey);
                    }
                }
            }

            // Remove keys outside of the lock to avoid deadlocks
            foreach (var keyToRemove in keysToRemove)
            {
                Remove(keyToRemove);
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

        /// <summary>
        /// Represents a cached item with its value, expiration time, and position in the LRU list
        /// </summary>
        private class CachedItem<TValue>
        {
            public TValue Value { get; set; }
            public DateTime Expiration { get; set; }
            public LinkedListNode<string> Node { get; }

            public CachedItem(TValue value, DateTime expiration, LinkedListNode<string> node)
            {
                Value = value;
                Expiration = expiration;
                Node = node;
            }
        }
    }
}
