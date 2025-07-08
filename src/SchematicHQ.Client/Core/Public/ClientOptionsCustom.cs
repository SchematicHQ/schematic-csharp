using System.Net.Http;
using SchematicHQ.Client.Core;
using SchematicHQ.Client.Cache;

#nullable enable

namespace SchematicHQ.Client;

public partial class ClientOptions
{
    public Dictionary<string, bool> FlagDefaults { get; set; } = new Dictionary<string, bool>();
    public ISchematicLogger Logger { get; set; } = new ConsoleLogger();
    public List<ICacheProvider<bool?>> CacheProviders { get; set; } = new List<ICacheProvider<bool?>>();
    public CacheConfiguration? CacheConfiguration { get; set; }
    public bool Offline { get; set; }

    public bool UseDatastream { get; set; } = false;
    public Datastream.DatastreamOptions DatastreamOptions { get; set; } = new Datastream.DatastreamOptions();
    public TimeSpan? DefaultEventBufferPeriod { get; set; }
    public IEventBuffer<CreateEventRequestBody>? EventBuffer { get; set; }
}

public static class ClientOptionsExtensions
{
    public static ClientOptions WithHttpClient(this ClientOptions options, HttpClient httpClient)
    {
        return new ClientOptions
        {
            BaseUrl = options.BaseUrl,
            CacheProviders = options.CacheProviders,
            CacheConfiguration = options.CacheConfiguration,
            DatastreamOptions = options.DatastreamOptions,
            DefaultEventBufferPeriod = options.DefaultEventBufferPeriod,
            EventBuffer = options.EventBuffer,
            FlagDefaults = options.FlagDefaults,
            Headers = new Headers(new Dictionary<string, HeaderValue>(options.Headers)),
            HttpClient = httpClient,
            Logger = options.Logger,
            MaxRetries = options.MaxRetries,
            Offline = options.Offline,
            Timeout = options.Timeout,
            UseDatastream = options.UseDatastream,
        };
    }

    /// <summary>
    /// Configure the client to use a Redis cache
    /// </summary>
    /// <param name="options">Client options</param>
    /// <param name="redisConfig">Redis configuration</param>
    /// <returns>Updated client options</returns>
    public static ClientOptions WithRedisCache(
        this ClientOptions options,
        Datastream.RedisCacheConfig redisConfig)
    {
        options.CacheConfiguration = new Cache.CacheConfiguration
        {
            ProviderType = Cache.CacheProviderType.Redis,
            RedisConfig = redisConfig,
            CacheTtl = redisConfig.CacheTTL
        };

        return options;
    }

    /// <summary>
    /// Configure the client to use a Redis cache with configuration builder
    /// </summary>
    /// <param name="options">Client options</param>
    /// <param name="configureRedis">Action to configure Redis settings</param>
    /// <returns>Updated client options</returns>
    public static ClientOptions WithRedisCache(
        this ClientOptions options,
        Action<Datastream.RedisCacheConfig> configureRedis)
    {
        var redisConfig = new Datastream.RedisCacheConfig();
        configureRedis(redisConfig);
        return WithRedisCache(options, redisConfig);
    }

    /// <summary>
    /// Configure the client to use a local in-memory cache
    /// </summary>
    /// <param name="options">Client options</param>
    /// <param name="capacity">Cache capacity</param>
    /// <param name="ttl">Cache TTL</param>
    /// <returns>Updated client options</returns>
    public static ClientOptions WithLocalCache(
        this ClientOptions options,
        int capacity = Cache.LocalCache<bool?>.DEFAULT_CACHE_CAPACITY,
        TimeSpan? ttl = null)
    {
        options.CacheConfiguration = new Cache.CacheConfiguration
        {
            ProviderType = Cache.CacheProviderType.Local,
            LocalCacheCapacity = capacity,
            CacheTtl = ttl
        };

        return options;
    }
}
