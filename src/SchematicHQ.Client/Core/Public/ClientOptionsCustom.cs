
using Microsoft.Extensions.Logging;
using SchematicHQ.Client.Core;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.RulesEngine;

#nullable enable

namespace SchematicHQ.Client;

public partial class ClientOptions
{
    private ILoggerFactory? _loggerFactory;
    private ILoggerFactory CreateDefaultLogger() => Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddSimpleConsole().SetMinimumLevel(LogLevel));

    public Dictionary<string, bool> FlagDefaults { get; set; } = new Dictionary<string, bool>();

    
    /// <summary>
    /// Override the logger factory for the client. Defaults to a simple console logger.
    /// </summary>
    public ILoggerFactory LoggerFactory
    {
        get => _loggerFactory ??= CreateDefaultLogger();
        set => _loggerFactory = value;
    }

    /// <summary>
    /// Sets the log level for the client. Defaults to LogLevel.Warning. No-op if a custom <see cref="LoggerFactory"/> is provided.
    /// </summary>
    public LogLevel LogLevel { get; set; } = LogLevel.Warning;
    
    public ICacheProvider? CacheProvider { get; set; }

    public CacheConfiguration? CacheConfiguration { get; set; }
    public bool Offline { get; set; }

    public bool UseDatastream { get; set; } = false;
    public Datastream.DatastreamOptions DatastreamOptions { get; set; } = new Datastream.DatastreamOptions();
    public TimeSpan? DefaultEventBufferPeriod { get; set; }
    public IEventBuffer<CreateEventRequestBody>? EventBuffer { get; set; }

    /// <summary>
    /// Base URL for the event capture service. Defaults to https://c.schematichq.com
    /// </summary>
    public string? EventCaptureBaseUrl { get; set; }

    /// <summary>
    /// Health check URL used in replicator mode when <see cref="ReplicatorHealthUrl"/> is not set.
    /// Matches the default used by the other Schematic SDKs.
    /// </summary>
    public const string DefaultReplicatorHealthUrl = "http://localhost:8090/ready";

    /// <summary>
    /// Enable replicator mode - uses only cached data from a replicator service
    /// </summary>
    public bool ReplicatorMode { get; set; } = false;

    /// <summary>
    /// Health check URL for the replicator service. Only used when <see cref="ReplicatorMode"/> is true.
    /// When null, empty or whitespace, replicator mode uses <see cref="DefaultReplicatorHealthUrl"/>
    /// (<c>http://localhost:8090/ready</c>). The replicator is polled every 30 seconds.
    /// </summary>
    public string? ReplicatorHealthUrl { get; set; }

    /// <summary>
    /// Resolves the health check URL the client should use: the configured
    /// <see cref="ReplicatorHealthUrl"/> if set, else <see cref="DefaultReplicatorHealthUrl"/>
    /// in replicator mode, else null.
    /// </summary>
    internal string? ResolveReplicatorHealthUrl() =>
        ResolveReplicatorHealthUrl(ReplicatorMode, ReplicatorHealthUrl);

    internal static string? ResolveReplicatorHealthUrl(bool replicatorMode, string? healthUrl)
    {
        if (!replicatorMode)
            return null;

        return string.IsNullOrWhiteSpace(healthUrl) ? DefaultReplicatorHealthUrl : healthUrl;
    }
}

public static class ClientOptionsExtensions
{
    public static ClientOptions WithHttpClient(this ClientOptions options, HttpClient httpClient)
    {
        return new ClientOptions
        {
            AdditionalHeaders = options.AdditionalHeaders,
            BaseUrl = options.BaseUrl,
            CacheProvider = options.CacheProvider,
            CacheConfiguration = options.CacheConfiguration,
            DatastreamOptions = options.DatastreamOptions,
            DefaultEventBufferPeriod = options.DefaultEventBufferPeriod,
            EventBuffer = options.EventBuffer,
            EventCaptureBaseUrl = options.EventCaptureBaseUrl,
            FlagDefaults = options.FlagDefaults,
            Headers = new Headers(new Dictionary<string, HeaderValue>(options.Headers)),
            HttpClient = httpClient,
            LoggerFactory = options.LoggerFactory,
            LogLevel = options.LogLevel,
            MaxRetries = options.MaxRetries,
            Offline = options.Offline,
            ReplicatorMode = options.ReplicatorMode,
            ReplicatorHealthUrl = options.ReplicatorHealthUrl,
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
        int capacity = Cache.LocalCache.DEFAULT_CACHE_CAPACITY,
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

    /// <summary>
    /// Configure the client to use replicator mode with the default health check URL,
    /// <see cref="ClientOptions.DefaultReplicatorHealthUrl"/> (<c>http://localhost:8090/ready</c>).
    /// </summary>
    /// <param name="options">Client options</param>
    /// <returns>Updated client options</returns>
    public static ClientOptions WithReplicatorMode(this ClientOptions options)
    {
        return WithReplicatorMode(options, ClientOptions.DefaultReplicatorHealthUrl);
    }

    /// <summary>
    /// Configure the client to use replicator mode with an explicit health check URL.
    /// Use the parameterless overload to get the default, <c>http://localhost:8090/ready</c>.
    /// </summary>
    /// <param name="options">Client options</param>
    /// <param name="healthUrl">Health check URL for the replicator service</param>
    /// <returns>Updated client options</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="healthUrl"/> is null, empty or whitespace. An explicitly passed blank URL
    /// usually means a missing config value, so it fails fast instead of silently using the default.
    /// </exception>
    public static ClientOptions WithReplicatorMode(
        this ClientOptions options,
        string healthUrl)
    {
        if (string.IsNullOrWhiteSpace(healthUrl))
            throw new ArgumentException(
                "Health URL cannot be empty. Call WithReplicatorMode() with no arguments to use the default " +
                ClientOptions.DefaultReplicatorHealthUrl, nameof(healthUrl));

        options.ReplicatorMode = true;
        options.ReplicatorHealthUrl = healthUrl;
        
        // Disable datastream when using replicator mode to avoid conflicts
        options.UseDatastream = false;

        return options;
    }
}
