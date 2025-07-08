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
        /// <param name="config">Redis configuration</param>
        /// <returns>A new Redis cache instance</returns>
        public static ICacheProvider<T> CreateRedisCache<T>(Datastream.RedisCacheConfig config)
        {
            return new RedisCache<T>(config);
        }
    }
}
