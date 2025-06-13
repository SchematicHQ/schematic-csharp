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
        /// Redis connection strings (required for Redis cache)
        /// </summary>
        public List<string> RedisConnectionStrings { get; set; } = new List<string>();
        
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
        public int LocalCacheCapacity { get; set; } = LocalCache<object>.DEFAULT_CACHE_CAPACITY;
    }

    /// <summary>
    /// Extension methods for DatastreamOptions
    /// </summary>
    public static class DatastreamOptionsExtensions
    {
        /// <summary>
        /// Configure Datastream to use Redis cache for company and user data
        /// </summary>
        /// <param name="options">Datastream options</param>
        /// <param name="connectionStrings">Redis connection strings</param>
        /// <param name="keyPrefix">Optional key prefix</param>
        /// <param name="cacheTtl">Optional cache TTL</param>
        /// <param name="database">Optional Redis database number</param>
        /// <returns>Updated options</returns>
        public static DatastreamOptions WithRedisCache(
            this DatastreamOptions options,
            IEnumerable<string> connectionStrings,
            string? keyPrefix = null,
            TimeSpan? cacheTtl = null,
            int database = 0)
        {
            options.CacheProviderType = DatastreamCacheProviderType.Redis;
            options.RedisConnectionStrings = connectionStrings.ToList();
            options.RedisKeyPrefix = keyPrefix;
            if (cacheTtl.HasValue)
            {
                options.CacheTTL = cacheTtl.Value;
            }
            options.RedisDatabase = database;
            return options;
        }
        
        /// <summary>
        /// Configure Datastream to use Redis cache for company and user data with a single connection string
        /// </summary>
        /// <param name="options">Datastream options</param>
        /// <param name="connectionString">Redis connection string</param>
        /// <param name="keyPrefix">Optional key prefix</param>
        /// <param name="cacheTtl">Optional cache TTL</param>
        /// <param name="database">Optional Redis database number</param>
        /// <returns>Updated options</returns>
        public static DatastreamOptions WithRedisCache(
            this DatastreamOptions options,
            string connectionString,
            string? keyPrefix = null,
            TimeSpan? cacheTtl = null,
            int database = 0)
        {
            return WithRedisCache(options, new[] { connectionString }, keyPrefix, cacheTtl, database);
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
