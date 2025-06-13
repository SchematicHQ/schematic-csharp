#nullable enable

namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// Cache provider types
    /// </summary>
    public enum CacheProviderType
    {
        /// <summary>
        /// In-memory local cache (default)
        /// </summary>
        Local,
        
        /// <summary>
        /// Redis distributed cache
        /// </summary>
        Redis
    }

    /// <summary>
    /// Configuration for cache providers
    /// </summary>
    public class CacheConfiguration
    {
        /// <summary>
        /// Type of cache provider to use
        /// </summary>
        public CacheProviderType ProviderType { get; set; } = CacheProviderType.Local;

        /// <summary>
        /// Cache TTL (time to live)
        /// </summary>
        public TimeSpan? CacheTtl { get; set; }

        /// <summary>
        /// Redis connection string (required for Redis cache)
        /// </summary>
        public IEnumerable<string>? RedisConnectionStrings { get; set; }

        /// <summary>
        /// Redis key prefix
        /// </summary>
        public string? RedisKeyPrefix { get; set; }

        /// <summary>
        /// Redis database number
        /// </summary>
        public int RedisDatabase { get; set; } = 0;

        /// <summary>
        /// Cache capacity for local cache
        /// </summary>
        public int LocalCacheCapacity { get; set; } = LocalCache<bool?>.DEFAULT_CACHE_CAPACITY;
    }
}
