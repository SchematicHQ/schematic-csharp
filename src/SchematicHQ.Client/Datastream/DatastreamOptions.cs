using System;
using SchematicHQ.Client.Cache;

namespace SchematicHQ.Client.Datastream
{
    /// <summary>
    /// Cache provider types for Datastream
    /// </summary>
    public enum DatastreamCacheProviderType
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
    /// Options for configuring the Datastream functionality
    /// </summary>
    public class DatastreamOptions
    {
        /// <summary>
        /// Time-to-live for cached resources (companies and users)
        /// </summary>
        public TimeSpan? CacheTTL { get; set; } = TimeSpan.FromHours(24);

        /// <summary>
        /// Type of cache provider to use for company and user data
        /// </summary>
        public DatastreamCacheProviderType CacheProviderType { get; set; } = DatastreamCacheProviderType.Local;

        /// <summary>
        /// Redis cache configuration
        /// </summary>
        public RedisCacheConfig? RedisConfig { get; set; }

        /// <summary>
        /// Cache capacity for local cache
        /// </summary>
        public int LocalCacheCapacity { get; set; } = LocalCache<object>.DEFAULT_CACHE_CAPACITY;
    }

    /// <summary>
    /// Extension methods for DatastreamOptions
    /// </summary>
    public static class DatastreamOptionsExtensions
    {
        /// <summary>
        /// Configure Datastream to use Redis cache with structured configuration (recommended)
        /// </summary>
        /// <param name="options">Datastream options</param>
        /// <param name="redisConfig">Redis configuration</param>
        /// <returns>Updated options</returns>
        public static DatastreamOptions WithRedisCache(
            this DatastreamOptions options,
            RedisCacheConfig redisConfig)
        {
            options.CacheProviderType = DatastreamCacheProviderType.Redis;
            options.RedisConfig = redisConfig ?? throw new ArgumentNullException(nameof(redisConfig));

            // Also set the TTL if specified in Redis config
            if (redisConfig.CacheTTL.HasValue)
            {
                options.CacheTTL = redisConfig.CacheTTL.Value;
            }

            return options;
        }

        /// <summary>
        /// Configure Datastream to use Redis cache with a configuration builder
        /// </summary>
        /// <param name="options">Datastream options</param>
        /// <param name="configureRedis">Action to configure Redis settings</param>
        /// <returns>Updated options</returns>
        public static DatastreamOptions WithRedisCache(
            this DatastreamOptions options,
            Action<RedisCacheConfig> configureRedis)
        {
            var redisConfig = new RedisCacheConfig();
            configureRedis(redisConfig);
            return WithRedisCache(options, redisConfig);
        }


        /// <summary>
        /// Configure Datastream to use local in-memory cache for company and user data
        /// </summary>
        /// <param name="options">Datastream options</param>
        /// <param name="capacity">Cache capacity</param>
        /// <param name="ttl">Cache TTL</param>
        /// <returns>Updated options</returns>
        public static DatastreamOptions WithLocalCache(
            this DatastreamOptions options,
            int capacity = LocalCache<object>.DEFAULT_CACHE_CAPACITY,
            TimeSpan? ttl = null)
        {
            options.CacheProviderType = DatastreamCacheProviderType.Local;
            options.LocalCacheCapacity = capacity;
            if (ttl.HasValue)
            {
                options.CacheTTL = ttl.Value;
            }
            return options;
        }
    }
}
