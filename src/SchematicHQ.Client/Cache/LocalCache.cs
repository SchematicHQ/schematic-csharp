using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// A thread-safe in-memory cache implementation with LRU eviction policy and background expiration
    /// </summary>
    /// <typeparam name="T">Type of values stored in the cache</typeparam>
    public class LocalCache<T> : ICacheProvider<T>, IDisposable
    {
        public const int DEFAULT_CACHE_CAPACITY = 1000;
        public static readonly TimeSpan DEFAULT_CACHE_TTL = TimeSpan.FromMilliseconds(5000); // 5000 milliseconds
        public static readonly TimeSpan UNLIMITED_TTL = TimeSpan.MaxValue;
        private readonly ConcurrentDictionary<string, CachedItem<T>> _cache;
        private readonly LinkedList<string> _lruList;
        private readonly object _lock = new object();
        private readonly int _maxItems;
        private readonly TimeSpan _ttl;
        private readonly Timer? _cleanupTimer;
        private bool _disposed = false;

        /// <summary>
        /// Creates a new LocalCache instance with background cleanup
        /// </summary>
        /// <param name="maxItems">Maximum number of items to cache</param>
        /// <param name="ttl">Default time-to-live for cached items</param>
        /// <param name="enableBackgroundCleanup">Whether to enable the background cleanup timer (defaults to true)</param>
        public LocalCache(int maxItems = DEFAULT_CACHE_CAPACITY, TimeSpan? ttl = null, bool enableBackgroundCleanup = true)
        {
            _cache = new ConcurrentDictionary<string, CachedItem<T>>();
            _lruList = new LinkedList<string>();
            _maxItems = maxItems;
            _ttl = ttl ?? DEFAULT_CACHE_TTL;

            if (enableBackgroundCleanup && _maxItems > 0 && _ttl != UNLIMITED_TTL)
            {
                // Calculate cleanup interval as 1/4 of TTL but not less than 1 second
                TimeSpan cleanupInterval = TimeSpan.FromTicks(Math.Max(_ttl.Ticks / 4, TimeSpan.TicksPerSecond));
                
                // For very long TTLs, cap at 1 minute
                if (cleanupInterval > TimeSpan.FromMinutes(1))
                {
                    cleanupInterval = TimeSpan.FromMinutes(1);
                }

                // Start background timer for cleanup
                _cleanupTimer = new Timer(
                    callback: CleanupExpiredItems,
                    state: null,
                    dueTime: (int)cleanupInterval.TotalMilliseconds,
                    period: (int)cleanupInterval.TotalMilliseconds);
            }
        }

        /// <inheritdoc/>
        public T? Get(string key)
        {
            if (_maxItems == 0 || _disposed)
                return default;

            if (!_cache.TryGetValue(key, out var item))
                return default;

            if (item.Expiration != DateTime.MaxValue && DateTime.UtcNow > item.Expiration)
            {
                // This item has expired, remove it
                Remove(key);
                return default;
            }

            // Update LRU position - We need to check if the node is still part of the list
            // because it might have been removed by another thread
            lock (_lock)
            {
                try
                {
                    // Check if node is still in the list before removing it
                    if (item.Node.List != null)
                    {
                        _lruList.Remove(item.Node);
                        _lruList.AddFirst(item.Node);
                    }
                    else
                    {
                        // Node was already removed, add a new one
                        item.Node = _lruList.AddFirst(key);
                    }
                }
                catch (InvalidOperationException)
                {
                    // If the node was already removed by another thread,
                    // create a new node for the key
                    item.Node = _lruList.AddFirst(key);
                }
            }

            return item.Value;
        }

        /// <inheritdoc/>
        public void Set(string key, T val, TimeSpan? ttlOverride = null)
        {
            if (_maxItems == 0 || _disposed)
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
                    // Update the existing item
                    existingItem.Value = val;
                    existingItem.Expiration = expiration;
                    
                    try
                    {
                        // Move to front of LRU list
                        if (existingItem.Node.List != null)
                        {
                            _lruList.Remove(existingItem.Node);
                        }
                        existingItem.Node = _lruList.AddFirst(key);
                    }
                    catch (InvalidOperationException)
                    {
                        // If the node was already removed by another operation,
                        // create a new node
                        existingItem.Node = _lruList.AddFirst(key);
                    }
                }
                else
                {
                    // If we're at capacity, remove least recently used item(s)
                    while (_maxItems > 0 && _cache.Count >= _maxItems && _lruList.Count > 0)
                    {
                        // Get the last item (least recently used) and remove it
                        var lruKey = _lruList.Last!.Value;
                        
                        // Don't evict the key we're trying to add
                        if (lruKey == key)
                        {
                            break;
                        }
                        
                        // Evict this LRU item
                        Remove(lruKey);
                    }

                    // Add new item to cache and LRU list
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
            if (_maxItems == 0 || _disposed)
                return false;

            Remove(key);
            return true;
        }

        /// <inheritdoc/>
        public void DeleteMissing(IEnumerable<string> keys)
        {
            if (_maxItems == 0 || _disposed)
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
                    try
                    {
                        // Only remove the node if it's still part of a list
                        if (removedItem.Node.List != null)
                        {
                            _lruList.Remove(removedItem.Node);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // Node might have been removed already by another thread
                    }
                }
            }
        }
        
        /// <summary>
        /// Callback method for the timer to cleanup expired items
        /// </summary>
        private void CleanupExpiredItems(object? state)
        {
            if (_maxItems == 0 || _disposed)
                return;
                
            var now = DateTime.UtcNow;
            var keysToRemove = new List<string>();
            
            // First identify expired items without holding the lock
            foreach (var pair in _cache)
            {
                if (pair.Value.Expiration != DateTime.MaxValue && now > pair.Value.Expiration)
                {
                    keysToRemove.Add(pair.Key);
                }
            }
            
            // Remove the expired items
            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }
        
        /// <summary>
        /// Disposes the timer resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// Protected implementation of Dispose pattern
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                    _cleanupTimer?.Dispose();
                }
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Finalizer ensures cleanup if client forgets to call Dispose
        /// </summary>
        ~LocalCache()
        {
            Dispose(false);
        }

        /// <summary>
        /// Represents a cached item with its value, expiration time, and position in the LRU list
        /// </summary>
        private class CachedItem<TValue>
        {
            public TValue Value { get; set; }
            public DateTime Expiration { get; set; }
            public LinkedListNode<string> Node { get; set; }

            public CachedItem(TValue value, DateTime expiration, LinkedListNode<string> node)
            {
                Value = value;
                Expiration = expiration;
                Node = node;
            }
        }
    }
}
