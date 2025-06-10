#nullable enable

using StackExchange.Redis;

namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// Factory for creating cache providers
    /// </summary>
    public static class CacheFactory
    {
        /// <summary>
        /// Create a local in-memory cache
        /// </summary>
        /// <typeparam name="T">Type of values to cache</typeparam>
        /// <param name="capacity">Maximum number of items in the cache</param>
        /// <param name="ttl">Time-to-live for cached items</param>
        /// <returns>A new local cache instance</returns>
        public static ICacheProvider<T> CreateLocalCache<T>(int capacity = LocalCache<T>.DEFAULT_CACHE_CAPACITY, TimeSpan? ttl = null)
        {
            return new LocalCache<T>(capacity, ttl);
        }

        /// <summary>
        /// Create a Redis cache
        /// </summary>
        /// <typeparam name="T">Type of values to cache</typeparam>
        /// <param name="connectionString">Redis connection string</param>
        /// <param name="keyPrefix">Prefix for Redis keys</param>
        /// <param name="ttl">Time-to-live for cached items</param>
        /// <param name="database">Redis database number</param>
        /// <returns>A new Redis cache instance</returns>
        public static ICacheProvider<T> CreateRedisCache<T>(
            IEnumerable<string> connectionStrings, 
            string? keyPrefix = null, 
            TimeSpan? ttl = null, 
            int database = 0)
        {
            return new RedisCache<T>(connectionStrings, keyPrefix, ttl, database);
        }
    }
}
