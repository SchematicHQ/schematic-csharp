using System.Text.Json;
using StackExchange.Redis;
using SchematicHQ.Client.Datastream;

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
    /// Creates a new RedisCache instance using structured configuration (recommended)
    /// </summary>
    /// <param name="config">Redis configuration</param>
    public RedisCache(RedisCacheConfig config)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (config.Endpoints == null || !config.Endpoints.Any())
        {
            throw new ArgumentException("Redis endpoints cannot be null or empty", nameof(config));
        }

        try
        {
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = config.AbortOnConnectFail,
                AllowAdmin = config.AllowAdmin,
                ClientName = config.ClientName,
                ConnectRetry = config.ConnectRetry,
                ConnectTimeout = config.ConnectTimeout,
                SyncTimeout = config.SyncTimeout,
                AsyncTimeout = config.AsyncTimeout,
                KeepAlive = config.KeepAlive,
                DefaultDatabase = config.DefaultDatabase,
                Ssl = config.Ssl,
                SslHost = config.SslHost,
                ServiceName = config.ServiceName
            };

            // Add endpoints
            foreach (var endpoint in config.Endpoints)
            {
                options.EndPoints.Add(endpoint);
            }

            // Set authentication
            if (!string.IsNullOrEmpty(config.Password))
            {
                options.Password = config.Password;
            }
            if (!string.IsNullOrEmpty(config.Username))
            {
                options.User = config.Username;
            }

            _redis = ConnectionMultiplexer.Connect(options);
            _db = _redis.GetDatabase(config.Database);
            _keyPrefix = config.KeyPrefix ?? DEFAULT_KEY_PREFIX;
            _ttl = config.CacheTTL ?? DEFAULT_CACHE_TTL;
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
