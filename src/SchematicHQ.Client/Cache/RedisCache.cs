using System.Text.Json;
using StackExchange.Redis;

#nullable enable

namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// Redis cache provider for SchematicHQ client
    /// </summary>
    /// <typeparam name="T">Type of values stored in the cache</typeparam>
    public class RedisCache<T> : ICacheProvider<T>
    {
        public const string DEFAULT_KEY_PREFIX = "schematic:";
        public static readonly TimeSpan DEFAULT_CACHE_TTL = TimeSpan.FromMinutes(5);
        public static readonly TimeSpan UNLIMITED_TTL = TimeSpan.MaxValue;

        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private readonly string _keyPrefix;
        private readonly TimeSpan _ttl;
        private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Creates a new RedisCache instance
    /// </summary>
    /// <param name="configuration">Redis connection string or configuration</param>
    /// <param name="keyPrefix">Key prefix for all cache keys</param>
    /// <param name="ttl">Default time-to-live for cached items</param>
    /// <param name="database">Redis database number</param>
    public RedisCache(IEnumerable<string> endpoints, string? keyPrefix = null, TimeSpan? ttl = null, int database = 0)
    {

      if (endpoints == null || !endpoints.Any())
      {
        throw new ArgumentNullException(nameof(endpoints), "Redis endpoints cannot be null or empty");
      }

      try
      {
        var options = new ConfigurationOptions();
        foreach (var endpoint in endpoints)
        {
          options.EndPoints.Add(endpoint);
        }

        _redis = ConnectionMultiplexer.Connect(options);
        _db = _redis.GetDatabase(database);
        _keyPrefix = keyPrefix ?? DEFAULT_KEY_PREFIX;
        _ttl = ttl ?? DEFAULT_CACHE_TTL;
        _jsonOptions = new JsonSerializerOptions { WriteIndented = false };
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException($"Failed to connect to Redis: {ex.Message}", ex);
      }
    }

        /// <inheritdoc/>
        public T? Get(string key)
        {
            var redisKey = GetRedisKey(key);
            var value = _db.StringGet(redisKey);
            
            if (value.IsNullOrEmpty)
            {
                return default;
            }

            try
            {
                return JsonSerializer.Deserialize<T>(value!);
            }
            catch
            {
                // If we can't deserialize, just remove the value and return null
                Delete(key);
                return default;
            }
        }

        /// <inheritdoc/>
        public void Set(string key, T val, TimeSpan? ttlOverride = null)
        {
            var redisKey = GetRedisKey(key);
            var ttl = ttlOverride ?? _ttl;
            
            // Use infinite expiry for UNLIMITED_TTL
            var expiry = ttl == UNLIMITED_TTL ? (TimeSpan?)null : ttl;
            
            var json = JsonSerializer.Serialize(val, _jsonOptions);
            _db.StringSet(redisKey, json, expiry);
        }

        /// <inheritdoc/>
        public bool Delete(string key)
        {
            var redisKey = GetRedisKey(key);
            return _db.KeyDelete(redisKey);
        }

        /// <inheritdoc/>
        public void DeleteMissing(IEnumerable<string> keys)
        {
            // Convert keys to Redis keys
            var keysToKeep = new HashSet<RedisKey>(keys.Select(k => GetRedisKey(k)));
            
            // Use server to scan for all keys with our prefix
            foreach (var endpoint in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(endpoint);
                var pattern = $"{_keyPrefix}*";
                
                // Get keys to delete
                var keysToDelete = server.Keys(pattern: pattern)
                    .Where(key => !keysToKeep.Contains(key))
                    .ToArray();
                
                // Delete keys in batches
                if (keysToDelete.Length > 0)
                {
                    const int batchSize = 100;
                    for (int i = 0; i < keysToDelete.Length; i += batchSize)
                    {
                        var batch = keysToDelete.Skip(i).Take(batchSize).ToArray();
                        _db.KeyDelete(batch);
                    }
                }
            }
        }

        private RedisKey GetRedisKey(string key)
        {
            return $"{_keyPrefix}{key}";
        }
    }
}
