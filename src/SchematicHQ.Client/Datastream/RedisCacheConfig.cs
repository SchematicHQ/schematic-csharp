using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace SchematicHQ.Client.Datastream
{
    /// <summary>
    /// Configuration for Redis cache connection
    /// </summary>
    public class RedisCacheConfig
    {
        /// <summary>
        /// Key prefix for all Redis operations (default: "schematic:")
        /// </summary>
        public string? KeyPrefix { get; set; } = "schematic:";

        /// <summary>
        /// Time-to-live for cached items
        /// </summary>
        public TimeSpan? CacheTTL { get; set; }
        
        /// <summary>
        /// Database number to use (default: 0)
        /// </summary>
        public int Database { get; set; } = 0;
        
        /// <summary>
        /// The configuration string used to connect to Redis.
        /// </summary>
        public string? Configuration { get; set; }

        /// <summary>
        /// The configuration used to connect to Redis.
        /// This is prioritized over Configuration.
        /// </summary>
        public ConfigurationOptions? ConfigurationOptions { get; set; }

        /// <summary>
        /// Gets or sets a delegate to create or return the ConnectionMultiplexer instance.
        /// This is prioritized over Configuration and ConfigurationOptions.
        /// </summary>
        public Func<IConnectionMultiplexer>? ConnectionMultiplexerFactory { get; set; }
        
        /// <summary>
        /// The Redis server endpoints (host:port format)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public List<string> Endpoints { get; set; } = new List<string>();

        /// <summary>
        /// Redis username for authentication (Redis 6.0+)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public string? Username { get; set; }

        /// <summary>
        /// Redis password for authentication
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public string? Password { get; set; }
        
        /// <summary>
        /// Use SSL/TLS for connection
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public bool Ssl { get; set; } = false;

        /// <summary>
        /// SSL host (defaults to endpoint if not specified)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public string? SslHost { get; set; }

        /// <summary>
        /// Client name for connection identification
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public string? ClientName { get; set; }

        /// <summary>
        /// Connection timeout in milliseconds (default: 5000ms)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public int ConnectTimeout { get; set; } = 5000;

        /// <summary>
        /// Synchronous operation timeout in milliseconds (default: 5000ms)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public int SyncTimeout { get; set; } = 5000;

        /// <summary>
        /// Asynchronous operation timeout in milliseconds (default: 5000ms)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public int AsyncTimeout { get; set; } = 5000;

        /// <summary>
        /// Keep-alive interval in seconds (default: 60s)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public int KeepAlive { get; set; } = 60;

        /// <summary>
        /// Whether to abort connection on connect failure (default: true)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public bool AbortOnConnectFail { get; set; } = true;

        /// <summary>
        /// Connection retry count (default: 3)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public int ConnectRetry { get; set; } = 3;
        
        /// <summary>
        /// Allow admin operations (dangerous commands)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public bool AllowAdmin { get; set; } = false;

        /// <summary>
        /// Default database for commands (can be overridden per-operation)
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public int? DefaultDatabase { get; set; }

        /// <summary>
        /// Service name for Sentinel support
        /// </summary>
        [Obsolete("Use Configuration, ConfigurationOptions, or ConnectionMultiplexerFactory Instead")]
        public string? ServiceName { get; set; }
    }

    /// <summary>
    /// Configuration for Redis Cluster.
    /// Note: StackExchange.Redis handles most cluster operations automatically.
    /// These settings provide hints for optimization but have limited direct mapping.
    /// </summary>
    [Obsolete("Use RedisConnection on the base configuration instead.")]
    public class RedisCacheClusterConfig : RedisCacheConfig
    {
        /// <summary>
        /// Maximum number of redirects to follow (default: 10)
        /// Note: StackExchange.Redis handles redirects internally (max 16).
        /// This property is reserved for future use.
        /// </summary>
        public int MaxRedirects { get; set; } = 10;

        /// <summary>
        /// Route commands by latency measurements.
        /// When true, enables DNS resolution for better node discovery.
        /// </summary>
        public bool RouteByLatency { get; set; } = false;

        /// <summary>
        /// Route commands randomly between replicas.
        /// Note: StackExchange.Redis handles load balancing automatically.
        /// This property is reserved for future use.
        /// </summary>
        public bool RouteRandomly { get; set; } = false;
    }
}
