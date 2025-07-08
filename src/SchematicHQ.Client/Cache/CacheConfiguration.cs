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
        /// Redis cache configuration
        /// </summary>
        public Datastream.RedisCacheConfig? RedisConfig { get; set; }

        /// <summary>
        /// Cache capacity for local cache
        /// </summary>
        public int LocalCacheCapacity { get; set; } = LocalCache<bool?>.DEFAULT_CACHE_CAPACITY;
    }
}
