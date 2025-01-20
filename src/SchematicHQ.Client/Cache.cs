using System.Collections.Concurrent;

#nullable enable

namespace SchematicHQ.Client;

    public interface ICacheProvider<T>
    {
        T? Get(string key);
        void Set(string key, T val, TimeSpan? ttlOverride = null);
    }

    public class LocalCache<T> : ICacheProvider<T>
    {
        public const int DEFAULT_CACHE_CAPACITY = 1000;
        public static readonly TimeSpan DEFAULT_CACHE_TTL = TimeSpan.FromMilliseconds(5000); // 5000 milliseconds

        private readonly ConcurrentDictionary<string, CachedItem<T>> _cache;
        private readonly LinkedList<string> _lruList;
        private readonly object _lock = new object();
        private readonly int _maxItems;
        private readonly TimeSpan _ttl;

        public LocalCache(int maxItems = DEFAULT_CACHE_CAPACITY, TimeSpan? ttl = null)
        {
            _cache = new ConcurrentDictionary<string, CachedItem<T>>();
            _lruList = new LinkedList<string>();
            _maxItems = maxItems;
            _ttl = ttl ?? DEFAULT_CACHE_TTL;
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

        public void Set(string key, T val, TimeSpan? ttlOverride = null)
        {
            if (_maxItems == 0)
                return;

            var ttl = ttlOverride ?? _ttl;
            var expiration = DateTime.UtcNow.Add(ttl);

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
