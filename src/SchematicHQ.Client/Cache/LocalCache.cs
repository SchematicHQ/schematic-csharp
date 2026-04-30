using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace SchematicHQ.Client.Cache;

    /// <summary>
    /// A thread-safe in-memory cache implementation with LRU eviction policy and background expiration
    /// </summary>
    public class LocalCache : ICacheProvider, IDisposable
    {
        public const int DEFAULT_CACHE_CAPACITY = 1000;
        public static readonly TimeSpan DEFAULT_CACHE_TTL = TimeSpan.FromMilliseconds(5000); // 5000 milliseconds
        public static readonly TimeSpan UNLIMITED_TTL = TimeSpan.MaxValue;
        private readonly ConcurrentDictionary<string, CachedItem> _cache;
        private readonly LinkedList<string> _lruList;
        private readonly object _lock = new object();
        private readonly int _maxItems;
        private readonly TimeSpan _ttl;
        private readonly Timer? _cleanupTimer;
        private bool _disposed = false;
        private int _cleanupInProgress = 0; // Flag to track if cleanup is in progress

        /// <summary>
        /// Creates a new LocalCache instance with background cleanup
        /// </summary>
        /// <param name="maxItems">Maximum number of items to cache</param>
        /// <param name="ttl">Default time-to-live for cached items</param>
        /// <param name="enableBackgroundCleanup">Whether to enable the background cleanup timer (defaults to true)</param>
        public LocalCache(int maxItems = DEFAULT_CACHE_CAPACITY, TimeSpan? ttl = null, bool enableBackgroundCleanup = true)
        {
            _cache = new ConcurrentDictionary<string, CachedItem>();
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
        public ValueTask<T?> Get<T>(string key, CancellationToken token = default) where T: notnull
        {
            return TryGet<T>(key, out var value)
                ? ValueTask.FromResult<T?>(value)
                : ValueTask.FromResult(default(T));
        }

        /// <inheritdoc/>
        public ValueTask Set<T>(string key, T val, TimeSpan? ttlOverride = null, CancellationToken token = default) where T: notnull
        {
            if (_maxItems == 0 || _disposed)
                return ValueTask.CompletedTask;

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
                    existingItem.Value = val!;
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
                    var newItem = new CachedItem(val!, expiration, node);
                    if (!_cache.TryAdd(key, newItem))
                    {
                        // If we couldn't add it, clean up the linked list node
                        _lruList.Remove(node);
                    }
                }
            }

            return ValueTask.CompletedTask;
        }

        private bool TryGet<T>(string key, [NotNullWhen(true)] out T? value) where T : notnull
        {
            value = default;

            if (_maxItems == 0 || _disposed)
                return false;

            if (!_cache.TryGetValue(key, out var item))
                return false;

            if (item.Expiration != DateTime.MaxValue && DateTime.UtcNow > item.Expiration)
            {
                Remove(key);
                return false;
            }

            // Update LRU position - same logic as Get<T>
            lock (_lock)
            {
                try
                {
                    if (item.Node.List != null)
                    {
                        _lruList.Remove(item.Node);
                        _lruList.AddFirst(item.Node);
                    }
                    else
                    {
                        item.Node = _lruList.AddFirst(key);
                    }
                }
                catch (InvalidOperationException)
                {
                    item.Node = _lruList.AddFirst(key);
                }
            }

            if (item.Value is T typedValue)
            {
                value = typedValue;
                return true;
            }

            return false; // Type mismatch — treat as a miss
        }

        /// <inheritdoc/>
        public async ValueTask<T> GetOrSet<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? ttlOverride = null, CancellationToken token = default) where T: notnull
        {
            if (TryGet<T>(key, out var existing))
                return existing;

            // Cache miss — invoke the factory and store the result
            var value = await factory(token);
            await Set(key, value, ttlOverride, token);
            return value;
        }

        /// <inheritdoc/>
        public ValueTask<bool> Delete(string key, CancellationToken token = default)
        {
            if (_maxItems == 0 || _disposed)
                return ValueTask.FromResult(false);

            Remove(key);
            return ValueTask.FromResult(true);
        }

        /// <inheritdoc/>
        public ValueTask DeleteMissing(IEnumerable<string> keys, string? scanPattern = null)
        {
            if (_maxItems == 0 || _disposed)
                return ValueTask.CompletedTask;

            var keysSet = new HashSet<string>(keys);
            var keysToRemove = new List<string>();

            // Collect keys to remove (those not in the provided list, optionally filtered by pattern)
            lock (_lock)
            {
                foreach (var cacheKey in _cache.Keys)
                {
                    // If a scanPattern is provided, only consider keys that match it
                    if (scanPattern != null && !GlobMatch(cacheKey, scanPattern))
                    {
                        continue;
                    }

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

            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// Simple glob pattern matcher supporting '*' (match any sequence) and '?' (match single char).
        /// This mirrors the Redis SCAN pattern semantics used by RedisCache.
        /// </summary>
        private static bool GlobMatch(string input, string pattern)
        {
            int i = 0, p = 0;
            int starI = -1, starP = -1;

            while (i < input.Length)
            {
                if (p < pattern.Length && (pattern[p] == '?' || pattern[p] == input[i]))
                {
                    i++;
                    p++;
                }
                else if (p < pattern.Length && pattern[p] == '*')
                {
                    starI = i;
                    starP = p;
                    p++;
                }
                else if (starP >= 0)
                {
                    p = starP + 1;
                    starI++;
                    i = starI;
                }
                else
                {
                    return false;
                }
            }

            while (p < pattern.Length && pattern[p] == '*')
            {
                p++;
            }

            return p == pattern.Length;
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
                
            // Check and set the cleanup in progress flag
            if (Interlocked.Exchange(ref _cleanupInProgress, 1) == 1)
            {
                // Another cleanup is already in progress
                return;
            }
            
            try
            {
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
            finally
            {
                // Reset the cleanup in progress flag
                Interlocked.Exchange(ref _cleanupInProgress, 0);
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
        private class CachedItem
        {
            public object Value { get; set; }
            public DateTime Expiration { get; set; }
            public LinkedListNode<string> Node { get; set; }

            public CachedItem(object value, DateTime expiration, LinkedListNode<string> node)
            {
                Value = value;
                Expiration = expiration;
                Node = node;
            }
        }
    }

