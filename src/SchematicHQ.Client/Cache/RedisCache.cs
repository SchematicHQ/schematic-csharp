using System.Text.Json;
using StackExchange.Redis;
using SchematicHQ.Client.Datastream;

namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// Redis cache provider for SchematicHQ client
    /// </summary>
    public class RedisCache : ICacheProvider
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

            try
            {
                _redis = GetRedis(config);
                _db = _redis.GetDatabase(config.Database);
                _keyPrefix = config.KeyPrefix ?? DEFAULT_KEY_PREFIX;
                _ttl = config.CacheTTL ?? DEFAULT_CACHE_TTL;
                _jsonOptions = SchematicCacheSerializerDefaults.Options;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to connect to Redis: {ex.Message}", ex);
            }
        }

        public ConnectionMultiplexer GetRedis(RedisCacheConfig config)
        {
            if (config.RedisConnection != null)
                return config.RedisConnection;
            
            // Obsolete configuration below
#pragma warning disable CS0618 // Type or member is obsolete
            if (config.Endpoints == null || !config.Endpoints.Any())
            {
                throw new ArgumentException("Redis endpoints cannot be null or empty", nameof(config));
            }
            
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

            // Apply cluster-specific configuration if provided
            if (config is RedisCacheClusterConfig clusterConfig)
            {
                // Note: StackExchange.Redis handles cluster operations internally.
                // Some cluster-specific behaviors in StackExchange.Redis:
                // - MaxRedirects: Not directly configurable, handled internally (default is 16)
                // - RouteByLatency: Use ResolveDns = true to enable latency-based routing
                // - RouteRandomly: Not directly supported, load balancing is automatic

                // Enable DNS resolution for better cluster node discovery
                options.ResolveDns = clusterConfig.RouteByLatency;

                // For Redis Cluster, we should not use proxy mode
                options.Proxy = Proxy.None;
            }

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
#pragma warning restore CS0618 // Type or member is obsolete
            
            return ConnectionMultiplexer.Connect(options);
        }

        /// <inheritdoc/>
        public async ValueTask<T?> Get<T>(string key)
        {
            var redisKey = GetRedisKey(key);
            var value = await _db.StringGetAsync(redisKey);

            if (value.IsNullOrEmpty)
            {
                return default;
            }

            try
            {
                return JsonSerializer.Deserialize<T>(value!, _jsonOptions);
            }
            catch (Exception e)
            {
                // If we can't deserialize, just remove the value and return null
                await Delete(key);
                return default;
            }
        }

        /// <inheritdoc/>
        public async ValueTask Set<T>(string key, T val, TimeSpan? ttlOverride = null)
        {
            var redisKey = GetRedisKey(key);
            var ttl = ttlOverride ?? _ttl;

            // Use infinite expiry for UNLIMITED_TTL
            var expiry = ttl == UNLIMITED_TTL ? (TimeSpan?)null : ttl;

            var json = JsonSerializer.Serialize(val, _jsonOptions);
            await _db.StringSetAsync(redisKey, json, expiry);
        }

        /// <inheritdoc/>
        public async ValueTask<bool> Delete(string key)
        {
            var redisKey = GetRedisKey(key);
            return await _db.KeyDeleteAsync(redisKey);
        }

        /// <inheritdoc/>
        public void DeleteMissing(IEnumerable<string> keys, string? scanPattern = null)
        {
            // Convert keys to Redis keys
            var keysToKeep = new HashSet<RedisKey>(keys.Select(k => GetRedisKey(k)));

            // Scope the scan as narrowly as the caller allows so that sibling caches sharing
            // the same keyPrefix + Redis DB aren't wiped.
            var pattern = scanPattern != null
                ? $"{_keyPrefix}{scanPattern}"
                : $"{_keyPrefix}*";

            // Use server to scan for keys matching the pattern
            foreach (var endpoint in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(endpoint);

                var fetchedKeys = await server.CommandGetKeysAsync([pattern]);
                
                var keysToDelete = fetchedKeys
                    .Where(key => !keysToKeep.Contains(key))
                    .ToArray();
                
                // Delete keys in batches
                if (keysToDelete.Length > 0)
                {
                    const int batchSize = 100;
                    for (int i = 0; i < keysToDelete.Length; i += batchSize)
                    {
                        var batch = keysToDelete.Skip(i).Take(batchSize).ToArray();
                        await _db.KeyDeleteAsync(batch);
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