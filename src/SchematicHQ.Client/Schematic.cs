using OneOf;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Core;
using SchematicHQ.Client.Leases;
using SchematicHQ.Client.RulesEngine;

#nullable enable

namespace SchematicHQ.Client;

public partial class Schematic
{
    private readonly ClientOptions _options;
    private readonly IEventBuffer<CreateEventRequestBody> _eventBuffer;
    private readonly ILogger _logger;
    private readonly ICacheProvider _cache;
    private readonly bool _offline;
    private readonly DatastreamClientAdapter? _datastreamClient;
    private readonly bool _replicatorMode;
    private bool _datastreamConnected;
    private bool _disposed;
    public readonly SchematicApi API;

    // Idempotency-key namespace for the track event a reservation settles into.
    // Deterministic per reservation, so a recovery emit (work that outlived the
    // local reservation TTL) and an accidental double settle collapse to one
    // billed event: the pipeline drops duplicates by account, environment, event
    // type and key for 24h before any credit consumption runs.
    private const string ReservationTrackIdempotencyPrefix = "lease-reservation:";

    // Credit lease plumbing. Null unless CreditLeases is configured, and in
    // server mode only the reservation client is built.
    private CreditLeaseManager? _creditLeaseManager;
    private ILeaseStore? _leaseStore;
    private IReservationStore? _reservations;
    private IServerReservationClient? _serverReservations;
    // True when lease state lives in a shared Redis backend that sibling pods
    // may also be drawing on, so a shutdown must not release its leases.
    private bool _leaseBackendShared;
    private TimeSpan _prewarmResolveTimeout = LeaseDefaults.PrewarmResolveTimeout;
    // The configured mode. Null when CreditLeases is not configured.
    private CreditLeaseMode? _creditLeaseMode;
    // Applied to a server-side hold's expiry. Server mode only.
    private TimeSpan _serverReservationTTL = LeaseDefaults.ReservationTTL;
    // Set at the top of a shutdown, so work that is still starting is refused
    // rather than racing the teardown.
    private volatile bool _closing;
    // Prewarms an identify spawned and nobody awaits. A shutdown waits them out:
    // an acquire that lands after the release installs a lease nothing releases,
    // and its credits stay held until the server expires them.
    private readonly HashSet<Task> _pendingPrewarms = new();

    public AccesstokensClient Accesstokens { get; init; }
    public AccountsClient Accounts { get; init; }
    public BillingClient Billing { get; init; }
    public CheckoutClient Checkout { get; init; }
    public CompaniesClient Companies { get; init; }
    public ComponentsClient Components { get; init; }
    public ComponentspublicClient Componentspublic { get; init; }
    public CreditsClient Credits { get; init; }
    public DataexportsClient Dataexports { get; init; }
    public EntitlementsClient Entitlements { get; init; }
    public EventsClient Events { get; init; }
    public FeaturesClient Features { get; init; }
    public PlanbundleClient Planbundle { get; init; }
    public PlangroupsClient Plangroups { get; init; }
    public PlanmigrationsClient Planmigrations { get; init; }
    public PlansClient Plans { get; init; }
    public ScheduledcheckoutClient Scheduledcheckout { get; init; }
    public WebhooksClient Webhooks { get; init; }

    public Schematic(string apiKey, ClientOptions? options = null)
    {
        _options = options ?? new ClientOptions();
        _offline = _options.Offline;
        _replicatorMode = _options.ReplicatorMode;
        _logger = _options.LoggerFactory.CreateLogger("SchematicHQ.Client");

        // Validate replicator mode configuration
        if (_replicatorMode && string.IsNullOrWhiteSpace(_options.ReplicatorHealthUrl))
        {
            throw new ArgumentException("ReplicatorHealthUrl is required when ReplicatorMode is enabled");
        }

        // Validate that Redis cache is configured when replicator mode is enabled
        if (_replicatorMode)
        {
            var hasRedisCache = false;

            // Check if Redis is configured via CacheConfiguration
            if (_options.CacheConfiguration?.ProviderType == CacheProviderType.Redis && 
                _options.CacheConfiguration.RedisConfig != null)
            {
                hasRedisCache = true;
            }
            
            // Check if explicit Redis cache providers are configured
            if (_options.CacheProvider is RedisCache)
            {
                hasRedisCache = true;
            }

            if (!hasRedisCache)
            {
                throw new ArgumentException("Redis cache configuration is required when ReplicatorMode is enabled. " +
                    "Configure Redis either through CacheConfiguration.RedisConfig or DatastreamOptions.RedisConfig.");
            }
        }

        var httpClient = _offline ? new HttpClient(new OfflineHttpMessageHandler()) : _options.HttpClient;
        API = new SchematicApi(apiKey, _options.WithHttpClient(httpClient));
        Accesstokens = (AccesstokensClient)API.Accesstokens;
        Accounts = (AccountsClient)API.Accounts;
        Billing = (BillingClient)API.Billing;
        Checkout = (CheckoutClient)API.Checkout;
        Companies = (CompaniesClient)API.Companies;
        Components = (ComponentsClient)API.Components;
        Componentspublic = (ComponentspublicClient)API.Componentspublic;
        Credits = (CreditsClient)API.Credits;
        Dataexports = (DataexportsClient)API.Dataexports;
        Entitlements = (EntitlementsClient)API.Entitlements;
        Events = (EventsClient)API.Events;
        Features = (FeaturesClient)API.Features;
        Planbundle = (PlanbundleClient)API.Planbundle;
        Plangroups = (PlangroupsClient)API.Plangroups;
        Planmigrations = (PlanmigrationsClient)API.Planmigrations;
        Plans = (PlansClient)API.Plans;
        Scheduledcheckout = (ScheduledcheckoutClient)API.Scheduledcheckout;
        Webhooks = (WebhooksClient)API.Webhooks;

        var captureClient = new EventCaptureClient(
            httpClient,
            apiKey,
            _logger,
            _options.EventCaptureBaseUrl
        );
        _eventBuffer = _options.EventBuffer ?? new EventBuffer<CreateEventRequestBody>(
            async items => await captureClient.SendBatchAsync(items),
            _logger,
            flushPeriod: _options.DefaultEventBufferPeriod
        );
        _eventBuffer.Start();

        // Initialize cache providers based on configuration
        if (_options.CacheProvider is not null)
        {
            // Use explicitly provided cache providers
            _cache = _options.CacheProvider;
        }
        else if (_options.CacheConfiguration != null)
        {
            switch (_options.CacheConfiguration.ProviderType)
            {
                case CacheProviderType.Redis:
                    if (_options.CacheConfiguration.RedisConfig == null)
                    {
                        _logger.LogWarning("Redis configuration not provided, falling back to local cache");
                        _cache = new LocalCache();
                    }
                    else
                    {
                        // Ensure the config has the cache TTL set
                        if (!_options.CacheConfiguration.RedisConfig.CacheTTL.HasValue && _options.CacheConfiguration.CacheTtl.HasValue)
                        {
                            _options.CacheConfiguration.RedisConfig.CacheTTL = _options.CacheConfiguration.CacheTtl;
                        }

                        _cache = new RedisCache(_options.CacheConfiguration.RedisConfig);
                    }
                    break;

                case CacheProviderType.Local:
                default:
                    _cache = new LocalCache(
                        _options.CacheConfiguration.LocalCacheCapacity,
                        _options.CacheConfiguration.CacheTtl
                    );
                    break;
            }
        }
        else
        {
            // Default to local cache
            _cache = new LocalCache();
        }

        // Initialize datastream if enabled or in replicator mode (for cache access)
        if (!_offline && (_options.UseDatastream || _replicatorMode))
        {
            // Create DatastreamOptions with cache settings from _options.CacheConfiguration
            var datastreamOptions = _options.DatastreamOptions ?? new DatastreamOptions();
            
            if (_options.CacheConfiguration != null)
            {
                // Apply cache TTL if not set in DatastreamOptions
                datastreamOptions.CacheTTL ??= _options.CacheConfiguration.CacheTtl;
            }

            _datastreamClient = new DatastreamClientAdapter(
                _options.BaseUrl,
                _logger,
                apiKey,
                _cache,
                datastreamOptions,
                _replicatorMode,
                _options.ReplicatorHealthUrl
            );

            if (!_replicatorMode)
            {
                // Only start WebSocket connections when not in replicator mode
                _datastreamClient.Start();
                _datastreamConnected = true;

                // Start a background task to monitor connection status
                StartConnectionMonitoring();
            }
            else
            {
                _datastreamConnected = false;
                _logger.LogInformation("Replicator mode enabled - datastream client created for cache access only");
            }
        }

        ConfigureCreditLeases();
    }

    /// <summary>
    /// Builds the credit lease and reservation plumbing the caller opted into.
    /// Runs after the datastream client is wired, so the auto mode can resolve
    /// against it.
    /// </summary>
    private void ConfigureCreditLeases()
    {
        var config = _options.CreditLeases;
        if (config == null)
        {
            return;
        }

        if (_offline)
        {
            _logger.LogWarning(
                "CreditLeases is configured but the client is in offline mode; lease-gated checks are disabled and Check will return flag defaults with no credit gating.");
            return;
        }

        var mode = config.Mode;
        _creditLeaseMode = mode;
        _serverReservations = new ApiServerReservationClient(Features, Credits);

        var configuredTTL = config.DefaultReservationTTL ?? LeaseDefaults.ReservationTTL;
        // The API refuses a hold expiring more than an hour after its own clock,
        // and this TTL is applied to the caller's, so clamp a step below the cap
        // to leave room for skew. Only server mode sends the value to the API: in
        // client mode it sizes the local sweep, so clamping it there would
        // shorten holds for no reason and the warning would be untrue.
        var maxTTL = LeaseDefaults.MaxReservationTTL - LeaseDefaults.ReservationTTLSkewAllowance;
        _serverReservationTTL = mode == CreditLeaseMode.Client
            ? configuredTTL
            : (configuredTTL < maxTTL ? configuredTTL : maxTTL);
        if (mode != CreditLeaseMode.Client && configuredTTL > maxTTL)
        {
            _logger.LogWarning(
                "CreditLeases.DefaultReservationTTL of {Configured} is longer than the API will hold credits for; server-mode holds will be clamped to {Clamped} (the {Max} maximum, less {Skew} of room for clock skew).",
                configuredTTL,
                maxTTL,
                LeaseDefaults.MaxReservationTTL,
                LeaseDefaults.ReservationTTLSkewAllowance);
        }

        // Server mode holds credits over the API, so none of the local lease
        // plumbing is built and the options that only steer it would silently do
        // nothing. Say so once, at startup. Auto with no datastream lands in
        // server mode too, and is the likelier way to get here.
        if (mode == CreditLeaseMode.Server || (mode == CreditLeaseMode.Auto && _datastreamClient == null))
        {
            var clientOnly = new List<string>();
            if (config.DefaultLeaseDuration != null) clientOnly.Add(nameof(config.DefaultLeaseDuration));
            if (config.DefaultLeaseSize != null) clientOnly.Add(nameof(config.DefaultLeaseSize));
            if (config.LowWaterMark != null) clientOnly.Add(nameof(config.LowWaterMark));
            if (config.SweepInterval != null) clientOnly.Add(nameof(config.SweepInterval));
            if (config.RedisClient != null) clientOnly.Add(nameof(config.RedisClient));
            if (config.RedisConfig != null) clientOnly.Add(nameof(config.RedisConfig));
            if (config.RedisKeyPrefix != null) clientOnly.Add(nameof(config.RedisKeyPrefix));
            if (config.PrewarmResolveTimeout != null) clientOnly.Add(nameof(config.PrewarmResolveTimeout));
            if (config.Overrides != null) clientOnly.Add(nameof(config.Overrides));
            if (clientOnly.Count > 0)
            {
                _logger.LogWarning(
                    "CreditLeases resolves to server mode, so {Options} will be ignored: those options only apply to client mode (local leases over datastream).",
                    string.Join(", ", clientOnly));
            }
        }

        // Auto with no datastream is the server-mode default, not a
        // misconfiguration: check-and-reserve gates over the API instead. Client
        // mode without a datastream is the degraded path, where every check falls
        // back to a plain flag check with the usage ignored, so it still warns.
        if (mode == CreditLeaseMode.Auto && _datastreamClient == null)
        {
            _logger.LogInformation(
                "CreditLeases is configured and datastream is not enabled; credit reservations will run in server mode (one check-and-reserve API call per check). Set UseDatastream to true (or replicator mode) for client-side leases.");
        }
        if (mode == CreditLeaseMode.Client && _datastreamClient == null)
        {
            _logger.LogWarning(
                "CreditLeases is configured but datastream is not enabled; Check will fall back to plain flag checks with NO credit gating (usage is ignored). Set UseDatastream to true (or replicator mode) to enable lease-gated checks.");
        }

        if (!CreditLeaseModeUsesLeases())
        {
            return;
        }

        var sweepInterval = config.SweepInterval ?? LeaseDefaults.SweepInterval;
        // Lease and reservation state belongs in a shared cache so gating holds
        // across horizontally scaled pods. Prefer an explicit client, then an
        // explicit connection config, then the Redis the cache is already
        // configured with, so an existing Redis setup backs leases with no
        // second client to wire up.
        var redisConfig = config.RedisConfig ?? _options.CacheConfiguration?.RedisConfig;
        var redis = config.RedisClient;
        if (redis == null && redisConfig != null)
        {
            try
            {
                redis = StackExchangeLeaseRedis.FromConfig(redisConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreditLeases: failed to connect the lease Redis backend; falling back to per-process stores");
            }
        }
        var keyPrefix = config.RedisKeyPrefix ?? redisConfig?.KeyPrefix ?? LeaseDefaults.KeyPrefix;

        if (redis != null)
        {
            // A shared backend: the lease balance and the reservation table live
            // in Redis, and single-key Lua scripts give cross-pod gating without
            // a separate lock service.
            _leaseBackendShared = true;
            _leaseStore = new RedisLeaseStore(redis, keyPrefix, config.DefaultLeaseDuration);
            _reservations = new RedisReservationStore(redis, _leaseStore, sweepInterval, keyPrefix);
        }
        else
        {
            // No shared backend: each pod then acquires and gates against its own
            // leases, which defeats the cross-pod overspend protection that is
            // the point of leasing, so warn rather than degrade silently.
            _logger.LogWarning(
                "CreditLeases is enabled without a shared Redis backend; lease and reservation state will be kept per-process. Configure a Redis cache (or CreditLeases.RedisClient) so leases gate correctly across multiple SDK instances.");
            _leaseStore = new InMemoryLeaseStore();
            _reservations = new InMemoryReservationStore(_leaseStore, sweepInterval);
        }

        _reservations.StartSweep();
        _creditLeaseManager = new CreditLeaseManager(
            new ApiLeaseWireClient(Credits),
            _leaseStore,
            config,
            _logger);
        _prewarmResolveTimeout = config.PrewarmResolveTimeout ?? LeaseDefaults.PrewarmResolveTimeout;
    }

    /// <summary>
    /// Whether the configured mode wants the local lease plumbing. Read during
    /// construction, after the datastream client has been wired, so the auto mode
    /// can resolve against it.
    /// </summary>
    private bool CreditLeaseModeUsesLeases()
    {
        if (_creditLeaseMode == null || _creditLeaseMode == CreditLeaseMode.Server) return false;
        if (_creditLeaseMode == CreditLeaseMode.Client) return true;
        return _datastreamClient != null;
    }

    /// <summary>
    /// Which mode a check with a usage resolves to right now. Null means no
    /// credit gating at all: CreditLeases is not configured, or the client is
    /// offline.
    ///
    /// <para>The auto mode resolves per check rather than once at startup, so a
    /// datastream that failed to start after construction falls to server mode
    /// instead of silently dropping every check to a plain, ungated one.</para>
    /// </summary>
    private CreditLeaseMode? EffectiveLeaseMode()
    {
        if (_creditLeaseMode == null || _offline) return null;
        if (_creditLeaseMode == CreditLeaseMode.Server) return CreditLeaseMode.Server;
        if (_creditLeaseMode == CreditLeaseMode.Client) return CreditLeaseMode.Client;
        var clientPlumbingReady = _creditLeaseManager != null && _leaseStore != null && _reservations != null;
        return _datastreamClient != null && clientPlumbingReady
            ? CreditLeaseMode.Client
            : CreditLeaseMode.Server;
    }

    private void StartConnectionMonitoring()
    {
        if (_datastreamClient == null)
            return;

        // Start a background task that periodically checks the connection status
        Task.Run(async () =>
        {
            try
            {
                while (!_disposed)
                {
                    try
                    {
                        // Check connection status every 2 seconds
                        var isConnected = await _datastreamClient.IsConnectedAsync(TimeSpan.FromMilliseconds(2000));
                        if (_datastreamConnected != isConnected)
                        {
                            _datastreamConnected = isConnected;
                            if (isConnected)
                            {
                                _logger.LogInformation("Datastream connection established");
                            }
                            else
                            {
                                _logger.LogWarning("Datastream connection lost, falling back to API");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error monitoring datastream connection");
                    }

                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection monitoring stopped");
            }
        });
    }

    /// <summary>
    /// Stops the event buffer, the reservation sweeper, the lease manager and the
    /// datastream client.
    ///
    /// <para>Credit leases: with the per-process in-memory backend this process
    /// is the only holder of its leases, so they are released here (best effort)
    /// and their unspent remainder returns to the company balance immediately
    /// rather than waiting out the lease expiry. With a shared Redis backend
    /// leases are deliberately not released: one row per company and credit is
    /// shared by every SDK instance pointed at that backend, so a single pod
    /// shutting down must not refund a lease its siblings are still drawing on.
    /// Shared leases reclaim themselves by expiring or being consumed.</para>
    ///
    /// <para>Lease work already in flight is waited out, bounded by
    /// <see cref="LeaseDefaults.ShutdownDrainTimeout"/>, before the release, so an
    /// acquire that lands mid-shutdown is one the release can see.</para>
    /// </summary>
    public async Task Shutdown()
    {
        _disposed = true;
        _closing = true;

        if (_reservations != null)
        {
            _reservations.Stop();
        }

        if (_creditLeaseManager != null)
        {
            // Refuse new lease work first, so the waits below are waiting on work
            // that is already unwinding rather than work still starting. Both
            // steps run for a shared backend too: the work must not outlive the
            // client, even where there is nothing to release.
            _creditLeaseManager.Stop();
            // One budget across both waits, not each timeout in turn: a caller
            // shutting a client down wants a bounded shutdown, not the sum of
            // every wait inside it.
            var deadline = DateTime.UtcNow + LeaseDefaults.ShutdownDrainTimeout;
            Task[] prewarms;
            lock (_pendingPrewarms)
            {
                prewarms = _pendingPrewarms.ToArray();
            }
            if (!await CreditLeaseManager.SettleWithinAsync(prewarms, deadline - DateTime.UtcNow))
            {
                _logger.LogWarning(
                    "Timed out after {Timeout} waiting for in-flight prewarms on shutdown",
                    LeaseDefaults.ShutdownDrainTimeout);
            }
            await _creditLeaseManager.DrainAsync(deadline - DateTime.UtcNow);
            if (!_leaseBackendShared)
            {
                await _creditLeaseManager.ReleaseAllLocalLeasesAsync();
            }
        }
        
        if (_eventBuffer != null)
        {
            await _eventBuffer.Stop();
        }

        if (_datastreamClient != null)
        {
            _datastreamClient.Close();
        }
    }

    public async Task<bool> CheckFlag(string flagKey, Dictionary<string, string>? company = null, Dictionary<string, string>? user = null)
    {
        var resp = await CheckFlagWithEntitlement(flagKey, company, user);
        return resp.Value;
    }

    public Task<CheckFlagWithEntitlementResponse> CheckFlagWithEntitlement(string flagKey, Dictionary<string, string>? company = null, Dictionary<string, string>? user = null)
    {
        return CheckFlagWithEntitlementInternal(flagKey, company, user, null, null, null);
    }

    /// <summary>
    /// The plain flag check, with the knobs a credit-aware check threads through
    /// it: the caller's hypothetical usage, a per-call timeout, and a default to
    /// fall back to. Only a local evaluation honors the preflight; the REST path
    /// ignores it, as it has no way to ask the server a hypothetical.
    /// </summary>
    private async Task<CheckFlagWithEntitlementResponse> CheckFlagWithEntitlementInternal(
        string flagKey,
        Dictionary<string, string>? company,
        Dictionary<string, string>? user,
        PreflightRequestBody? preflight,
        TimeSpan? timeout,
        bool? defaultValue)
    {
        if (_offline)
            return new CheckFlagWithEntitlementResponse
            {
                FlagKey = flagKey,
                Value = defaultValue ?? GetFlagDefault(flagKey),
                Reason = "offline mode"
            };

        // Try datastream first if enabled
        if (_datastreamClient != null)
        {
            try
            {
                var request = new CheckFlagRequestBody
                {
                    Company = company,
                    User = user
                };
                var flagResult = await _datastreamClient.CheckFlag(request, flagKey, preflight);

                var response = CheckFlagWithEntitlementResponse.FromCheckFlagResult(flagResult);

                // Submit flag check event for successful datastream evaluation
                SubmitFlagCheckEvent(
                    flagKey,
                    flagResult.Value,
                    company,
                    user,
                    new EventBodyFlagCheck
                    {
                        FlagKey = flagKey,
                        Value = flagResult.Value,
                        FlagId = flagResult.FlagId,
                        RuleId = flagResult.RuleId,
                        CompanyId = flagResult.CompanyId,
                        UserId = flagResult.UserId,
                        Reason = flagResult.Reason,
                        Error = flagResult.Error?.Message
                    });
                return response;
            }
            catch (Exception ex)
            {
                // Fall back to API if datastream fails
                _logger.LogDebug(ex, "Datastream flag check failed, falling back to API");
                return await CheckFlagWithEntitlementApi(flagKey, company, user, timeout, defaultValue);
            }
        }

        // Fall back to API request
        return await CheckFlagWithEntitlementApi(flagKey, company, user, timeout, defaultValue);
    }

    private async Task<CheckFlagWithEntitlementResponse> CheckFlagWithEntitlementApi(string flagKey, Dictionary<string, string>? company, Dictionary<string, string>? user, TimeSpan? timeout = null, bool? defaultValue = null)
    {
        try
        {
            // If null, check flag with empty context
            var requestBody = new CheckFlagRequestBody
            {
                Company = company ?? new Dictionary<string, string>(),
                User = user ?? new Dictionary<string, string>()
            };

            string cacheKey = BuildFlagCacheKey(flagKey, company, user);

            // Check cache first
            var cachedResponse = await _cache.Get<CheckFlagWithEntitlementResponse>(cacheKey);
            if (cachedResponse != null)
            {
                // Submit flag check event for cached value
                SubmitFlagCheckEventForValue(flagKey, cachedResponse.Value, company, user, "cache");
                return cachedResponse;
            }

            // Make API request
            var apiResponse = await API.Features.CheckFlagAsync(
                flagKey,
                requestBody,
                timeout == null ? null : new RequestOptions { Timeout = timeout });

            if (apiResponse == null)
            {
                // If the client was not initialized with an API key, we'll have a no-op here which returns an empty response
                return new CheckFlagWithEntitlementResponse
                {
                    FlagKey = flagKey,
                    Value = defaultValue ?? GetFlagDefault(flagKey),
                    Reason = "no response"
                };
            }

            var result = CheckFlagWithEntitlementResponse.FromApiResponse(apiResponse.Data, flagKey);

            // Cache the result
            try
            {
                try
                {
                await _cache.Set(cacheKey, result);
                }
                catch (Exception cacheEx)
                {
                    _logger.LogError(cacheEx, "Error caching flag result");
                }
            }
            catch (Exception cacheEx)
            {
                _logger.LogError("Error caching flag result: {0}", cacheEx.Message);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking flag via API");
            return new CheckFlagWithEntitlementResponse
            {
                FlagKey = flagKey,
                Value = defaultValue ?? GetFlagDefault(flagKey),
                Reason = ex.Message
            };
        }
    }

    public async Task<List<CheckFlagResponseData>> CheckFlags(
        Dictionary<string, string>? company = null,
        Dictionary<string, string>? user = null,
        IEnumerable<string>? keys = null)
    {
        var keyList = keys?.ToList();

        if (_offline)
        {
            _logger.LogDebug("Offline mode enabled, returning default flag values");
            if (keyList == null || keyList.Count == 0)
            {
                return new List<CheckFlagResponseData>();
            }
            return keyList.Select(k => new CheckFlagResponseData
            {
                Flag = k,
                Value = GetFlagDefault(k),
                Reason = "Offline mode - using default value"
            }).ToList();
        }

        try
        {
            var requestBody = new CheckFlagRequestBody
            {
                Company = company,
                User = user
            };

            if (_datastreamClient != null && keyList != null && keyList.Count > 0)
            {
                var dsResults = await CheckFlagsViaDatastream(keyList, requestBody);
                if (dsResults != null)
                {
                    return dsResults;
                }
            }

            if (keyList == null || keyList.Count == 0)
            {
                _logger.LogDebug("No specific flag keys provided, calling CheckFlags API");
                var apiResp = await API.Features.CheckFlagsAsync(requestBody);
                return apiResp.Data.Flags.ToList();
            }

            var cachedResults = new Dictionary<string, CheckFlagResponseData>();
            var allCached = true;
            foreach (var key in keyList)
            {
                var cacheKey = BuildFlagCacheKey(key, company, user);
                var cached = await _cache.Get<CheckFlagWithEntitlementResponse>(cacheKey);
                if (cached != null)
                {
                    cachedResults[key] = new CheckFlagResponseData
                    {
                        Flag = key,
                        Value = cached.Value,
                        Reason = cached.Reason
                    };
                }
                else
                {
                    allCached = false;
                }
            }

            if (allCached)
            {
                _logger.LogDebug("All {KeyCount} flags found in cache", keyList.Count);
                return keyList.Select(k => cachedResults[k]).ToList();
            }

            _logger.LogDebug("Cache miss for some flags, calling API for all {KeyCount} keys", keyList.Count);
            var freshResp = await API.Features.CheckFlagsAsync(requestBody);
            var apiResults = freshResp.Data.Flags.ToDictionary(f => f.Flag);

            foreach (var kvp in apiResults)
            {
                var cacheKey = BuildFlagCacheKey(kvp.Key, company, user);
                var responseForCache = CheckFlagWithEntitlementResponse.FromApiResponse(kvp.Value, kvp.Key);
                
                try
                {
                    try
                    {
                        await _cache.Set(cacheKey, responseForCache);  
                    }
                    catch (Exception cacheEx)
                    {
                        _logger.LogError(cacheEx, "Error caching flag result");
                    }
                }
                catch (Exception cacheEx)
                {
                    _logger.LogError("Error caching flag result: {0}", cacheEx.Message);
                }
               
            }

            return keyList.Select(key =>
            {
                if (apiResults.TryGetValue(key, out var f))
                {
                    return f;
                }
                return new CheckFlagResponseData
                {
                    Flag = key,
                    Value = GetFlagDefault(key),
                    Reason = "Flag not found - using default value"
                };
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking flags");
            return (keyList ?? new List<string>()).Select(k => new CheckFlagResponseData
            {
                Flag = k,
                Value = GetFlagDefault(k),
                Reason = $"Error occurred - using default value: {ex.Message}"
            }).ToList();
        }
    }

    private async Task<List<CheckFlagResponseData>?> CheckFlagsViaDatastream(
        List<string> keys,
        CheckFlagRequestBody requestBody)
    {
        try
        {
            var results = new List<CheckFlagResponseData>();
            foreach (var key in keys)
            {
                var result = await _datastreamClient!.CheckFlag(requestBody, key);

                results.Add(new CheckFlagResponseData
                {
                    Flag = key,
                    Value = result.Value,
                    Reason = result.Reason,
                    FlagId = result.FlagId,
                    RuleId = result.RuleId,
                    CompanyId = result.CompanyId,
                    UserId = result.UserId
                });
            }
            _logger.LogDebug("All {KeyCount} flags evaluated via Datastream", keys.Count);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "Datastream CheckFlags failed, falling back to API");
            return null;
        }
    }

    // Helper method to build consistent cache keys
    private string BuildFlagCacheKey(string flagKey, Dictionary<string, string>? company, Dictionary<string, string>? user)
    {
        string cacheKey = flagKey;

        if (company != null && company.Count > 0)
        {
            cacheKey += ":c-" + string.Join(";", company.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        if (user != null && user.Count > 0)
        {
            cacheKey += ":u-" + string.Join(";", user.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        return cacheKey;
    }

    /// <summary>
    /// Credit-aware feature check. With CreditLeases configured and a usage
    /// passed (optionally qualified by an event subtype), this gates the check
    /// against the company's credit balance and hands back a reservation on
    /// success. Pass that handle to <see cref="TrackWithReservation"/> when the
    /// work completes.
    ///
    /// <para>In client mode (datastream enabled) the hold is carved out of a
    /// local lease and the flag is evaluated by the WASM engine. In server mode
    /// it is a single check-and-reserve API call that evaluates the flag and
    /// takes the hold server-side. CreditLeases.Mode picks; the default resolves
    /// to client mode when datastream is enabled and server mode otherwise.</para>
    ///
    /// <para>Without CreditLeases configured, or with no usage, this falls
    /// through to a plain flag check and returns the flag's value with no
    /// reservation. The caller's preflight is still threaded through that plain
    /// check, so a local evaluation gates on the post-call balance, just without
    /// a hold.</para>
    /// </summary>
    public async Task<CheckResult> Check(
        string flagKey,
        Dictionary<string, string>? company = null,
        Dictionary<string, string>? user = null,
        CheckOptions? options = null)
    {
        async Task<CheckResult> Fallback()
        {
            var resp = await CheckFlagWithEntitlementInternal(
                flagKey,
                company,
                user,
                LeasePreflight.Build(options),
                options?.Timeout,
                options?.DefaultValue);
            return new CheckResult
            {
                Allowed = resp.Value,
                Value = resp.Value,
                Reason = resp.Reason,
                Entitlement = resp.Entitlement,
                FlagKey = resp.FlagKey,
                FlagId = resp.FlagId
            };
        }

        var mode = EffectiveLeaseMode();
        if (options?.Usage == null || mode == null)
        {
            return await Fallback();
        }

        if (mode == CreditLeaseMode.Server)
        {
            return await ServerCheck.CheckWithServerReservationAsync(
                new ServerCheckDeps
                {
                    Client = _serverReservations!,
                    Logger = _logger,
                    ReservationTTL = _serverReservationTTL,
                    GetDefault = () => options.DefaultValue ?? GetFlagDefault(flagKey)
                },
                flagKey,
                company,
                user,
                options,
                Fallback);
        }

        // Client mode without the local plumbing (an explicit client mode and no
        // datastream) keeps the plain, ungated flag check.
        if (_creditLeaseManager == null || _leaseStore == null || _reservations == null || _datastreamClient == null)
        {
            return await Fallback();
        }

        return await LeaseCheck.CheckWithLeaseAsync(
            new CheckDeps
            {
                LeaseStore = _leaseStore,
                Reservations = _reservations,
                Manager = _creditLeaseManager,
                DataStream = new DatastreamCheckSource(_datastreamClient),
                Logger = _logger,
                // Lease-path checks must stay visible to flag-check analytics and
                // company last-seen, same as every plain check path.
                EmitFlagCheck = body => EnqueueEvent(EventType.FlagCheck, body)
            },
            flagKey,
            company,
            user,
            options,
            Fallback);
    }

    /// <summary>
    /// Consumes a reservation a <see cref="Check"/> issued. A client-mode hold
    /// refunds its unspent slice to the lease's local balance and emits a track
    /// event carrying the actual quantity; the server-side processor consumes
    /// that quantity times the consumption rate from the company's real balance.
    ///
    /// <para>A server-mode handle has no local hold to refund: the event carries
    /// the reservation id and the server settles the hold when it processes
    /// it.</para>
    ///
    /// <para>If the work outlived the reservation's TTL and the sweeper already
    /// returned the hold to the lease, the local refund has happened but the
    /// usage must still be billed, so the event goes out anyway as a recovery
    /// emit. The server bills it against the lease's sub-ledger while the lease
    /// is live, or falls through to a direct grant decrement once the server
    /// lease has expired.</para>
    ///
    /// <para>Double-billing is prevented server-side: the event carries a
    /// deterministic idempotency key derived from the reservation id, so a
    /// recovery emit racing the normal one, or an accidental second settle,
    /// collapses to a single billed event across pods and process restarts.</para>
    /// </summary>
    public async Task TrackWithReservation(
        ReservationRecord? reservation,
        double actualQuantity,
        TrackWithReservationOptions? options = null)
    {
        if (_offline) return;

        // A check allows without a hold in several ordinary cases: the feature is
        // not credit-metered, the check failed open, the usage was 0, or credit
        // leases are not configured. Callers pass the result's reservation
        // straight through, so take the null and tell them how to bill the usage
        // instead of throwing on a settle that has nothing to settle.
        if (reservation == null)
        {
            _logger.LogError(
                "TrackWithReservation called without a reservation: the check allowed without taking a hold, so there is nothing to settle. Report the usage with Track instead.");
            return;
        }

        // Mirror the check path's usage guard. A non-finite quantity must reach
        // neither the store (where the claim would take the hold with no refund
        // of the unspent slice) nor the billing event; a negative one would bill
        // negative usage. Skip the settle entirely: the untouched reservation
        // expires at its TTL and the sweeper refunds the whole hold, so no
        // credits are lost and nothing bogus is billed.
        if (!LeaseQuantity.IsValid(actualQuantity))
        {
            _logger.LogError(
                "TrackWithReservation: invalid actualQuantity {Quantity} for reservation {ReservationId}; must be a finite, non-negative number. Skipping the settle (the hold is refunded at its TTL).",
                actualQuantity,
                reservation.Id);
            return;
        }

        var trackOptions = new TrackOptions
        {
            IdempotencyKey = ReservationTrackIdempotencyPrefix + reservation.Id
        };

        // Server mode: the hold lives on the server and settles by id, so there
        // is nothing local to consume or refund.
        if (reservation.Mode == CreditLeaseMode.Server)
        {
            TrackEvent(ReservationTrack.BuildTrackEvent(reservation, actualQuantity, options), trackOptions);
            return;
        }

        if (_reservations == null)
        {
            _logger.LogWarning(
                "TrackWithReservation called but CreditLeases is not configured; emitting an unsettled track event");
            // No local store to settle against, but the event must still carry
            // the lease id (the handle came from a lease-configured client, and
            // dropping it would double-debit the grant) and the deterministic
            // idempotency key.
            TrackEvent(ReservationTrack.BuildTrackEvent(reservation, actualQuantity, options), trackOptions);
            return;
        }

        EventBodyTrack track;
        bool settledLocally;
        try
        {
            var outcome = await ReservationTrack.ConsumeAndBuildEventAsync(
                _reservations,
                reservation,
                actualQuantity,
                options);
            track = outcome.Track;
            settledLocally = outcome.SettledLocally;
        }
        catch (Exception ex)
        {
            // The local settle failed, likely an unreachable Redis. The usage
            // still has to be billed: build the event from the caller-held handle
            // and emit it anyway. The unsettled local hold is reclaimed by the
            // sweeper at its TTL or at lease expiry, and the idempotency key
            // keeps a retried settle from double-billing.
            _logger.LogWarning(
                ex,
                "TrackWithReservation: failed to settle reservation {ReservationId} locally; emitting the track event anyway",
                reservation.Id);
            track = ReservationTrack.BuildTrackEvent(reservation, actualQuantity, options);
            settledLocally = false;
        }

        if (!settledLocally)
        {
            _logger.LogDebug(
                "TrackWithReservation: reservation {ReservationId} was not settled locally (expired, swept, already settled, or the store was unreachable); emitting the track event keyed for idempotent server-side dedupe",
                reservation.Id);
        }

        TrackEvent(track, trackOptions);
    }

    /// <summary>
    /// Pre-warms a credit lease for each given credit type, so the first check
    /// against it does not pay the acquire round trip. Failures are logged rather
    /// than thrown.
    ///
    /// <para>When only secondary company keys are passed, this fetches the
    /// company over the datastream (waiting up to
    /// CreditLeases.PrewarmResolveTimeout for the socket to connect), which both
    /// resolves the id and warms the cache so the first check hits the lease path.
    /// It covers a brand-new company too: the fetch retries until the server has
    /// ingested the preceding identify and can stream it back.</para>
    /// </summary>
    public async Task Prewarm(Dictionary<string, string>? company, IEnumerable<string> creditTypeIds)
    {
        if (_creditLeaseManager == null || _leaseStore == null)
        {
            _logger.LogDebug(
                EffectiveLeaseMode() == CreditLeaseMode.Server
                    ? "Prewarm is a no-op in server mode; there is no local lease to warm"
                    : "Prewarm called but CreditLeases is not configured");
            return;
        }
        if (company == null || company.Count == 0)
        {
            _logger.LogDebug("Prewarm requires company keys");
            return;
        }
        if (_closing)
        {
            // A shutdown only waits out the prewarms it spawned; a caller
            // invoking this directly would otherwise install a lease after the
            // release has already listed the store.
            _logger.LogDebug("Prewarm: the client is closing, skipping the acquire");
            return;
        }

        var companyId = await ResolveCompanyIdWithWait(company);
        if (companyId == null)
        {
            _logger.LogDebug(
                "Prewarm: company not resolved within {Timeout} (the first check will acquire)",
                _prewarmResolveTimeout);
            return;
        }

        var acquires = new List<Task>();
        foreach (var creditTypeId in creditTypeIds)
        {
            acquires.Add(PrewarmOne(companyId, creditTypeId));
        }
        await Task.WhenAll(acquires);
    }

    private async Task PrewarmOne(string companyId, string creditTypeId)
    {
        try
        {
            await _creditLeaseManager!.AcquireIfNeededAsync(companyId, creditTypeId);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Prewarm: failed to acquire a lease for {CreditTypeId}", creditTypeId);
        }
    }

    /// <summary>
    /// Resolves a company id, actively fetching the company over the datastream
    /// when only secondary keys were passed, and warming the cache as a side
    /// effect. Null when the company never surfaced within the prewarm timeout.
    ///
    /// <para>An identify does not push a company into the datastream cache:
    /// companies are only streamed in response to a request. So this fetches
    /// (cache first, then over the socket) rather than polling a cache that would
    /// stay empty until the timeout.</para>
    /// </summary>
    private async Task<string?> ResolveCompanyIdWithWait(Dictionary<string, string> company)
    {
        if (company.TryGetValue("id", out var id) && !string.IsNullOrEmpty(id))
        {
            return id;
        }
        if (_datastreamClient == null || _prewarmResolveTimeout <= TimeSpan.Zero)
        {
            var cachedOnly = await _datastreamClient!.GetCachedCompany(company);
            return cachedOnly?.Id;
        }

        // Already cached by an earlier check or prewarm.
        var cached = await _datastreamClient.GetCachedCompany(company);
        if (cached != null && !string.IsNullOrEmpty(cached.Id))
        {
            return cached.Id;
        }

        // Fetch, retrying across the brief connecting window at boot, bounded by
        // the prewarm timeout.
        var deadline = DateTime.UtcNow + _prewarmResolveTimeout;
        while (true)
        {
            // A company resolved for a client that is shutting down warms
            // nothing, and the shutdown would be waiting out the rest of this
            // poll.
            if (_closing) return null;
            try
            {
                var resolved = await _datastreamClient.ResolveCompany(company);
                if (resolved != null && !string.IsNullOrEmpty(resolved.Id))
                {
                    return resolved.Id;
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "Prewarm: datastream company fetch failed");
            }
            if (DateTime.UtcNow >= deadline) return null;
            await Task.Delay(LeaseDefaults.PrewarmPollInterval);
        }
    }

    public void Identify(Dictionary<string, string> keys, EventBodyIdentifyCompany? company = null, string? name = null, Dictionary<string, object?>? traits = null, IdentifyOptions? options = null)
    {
        EnqueueEvent(
            EventType.Identify,
            new EventBodyIdentify
            {
                Company = company,
                Keys = keys,
                Name = name,
                Traits = traits
            },
            idempotencyKey: options?.IdempotencyKey);

        if (options?.Prewarm == null || options.Prewarm.Count == 0)
        {
            return;
        }

        // Force a flush so the server processes the identify as soon as it can:
        // without it the company may sit in the buffer for a whole flush period
        // before the server sees it, and the prewarm's bounded poll would only be
        // waiting on us.
        _ = _eventBuffer.Flush().ContinueWith(
            t => _logger.LogDebug(t.Exception, "Identify: the flush before a prewarm failed"),
            TaskContinuationOptions.OnlyOnFaulted);

        var prewarming = PrewarmQuietly(company?.Keys, options.Prewarm);
        lock (_pendingPrewarms)
        {
            _pendingPrewarms.Add(prewarming);
        }
        _ = prewarming.ContinueWith(t =>
        {
            lock (_pendingPrewarms)
            {
                _pendingPrewarms.Remove(t);
            }
        });
    }

    private async Task PrewarmQuietly(Dictionary<string, string>? company, List<string> creditTypeIds)
    {
        try
        {
            await Prewarm(company, creditTypeIds);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Identify prewarm failed");
        }
    }

    public void Track(string eventName, Dictionary<string, string>? company = null, Dictionary<string, string>? user = null, Dictionary<string, object?>? traits = null, int? quantity = null, TrackOptions? options = null)
    {
        TrackEvent(
            new EventBodyTrack
        {
            Company = company,
            Event = eventName,
            Traits = traits,
            User = user,
                Quantity = quantity,
                LeaseId = options?.LeaseId,
                ReservationId = options?.ReservationId
            },
            options);
    }

    /// <summary>
    /// Enqueues an already-built track event and updates the datastream's view
    /// of the company's metrics. Shared with the reservation settle, which
    /// builds its own body so it can carry the lease or reservation id the
    /// server routes the credit consumption by.
    /// </summary>
    private void TrackEvent(EventBodyTrack eventBody, TrackOptions? options)
    {
        EnqueueEvent(
            EventType.Track,
            eventBody,
            idempotencyKey: options?.IdempotencyKey,
            sentAt: options?.SentAt,
            trustedClientClock: options?.TrustedClientClock,
            backfill: options?.Backfill);

        // Update company metrics in datastream if available and connected
        if (eventBody.Company != null && UseDatastream() && _datastreamClient != null && _datastreamConnected)
        {
            try
            {
                var success = _datastreamClient.UpdateCompanyMetrics(eventBody);
                if (!success)
                {
                    _logger.LogError("Failed to update company metrics: datastream update failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update company metrics");
            }
        }
    }

    private void EnqueueEvent(
        EventType eventType,
        OneOf<EventBodyTrack, EventBodyFlagCheck, EventBodyIdentify, EventBodyInference> body,
        string? idempotencyKey = null,
        DateTime? sentAt = null,
        bool? trustedClientClock = null,
        bool? backfill = null)
    {
        if (_offline)
            return;

        try
        {
            var eventBody = new CreateEventRequestBody
            {
                EventType = eventType,
                Body = body,
                SentAt = sentAt ?? DateTime.UtcNow,
                IdempotencyKey = idempotencyKey,
                TrustedClientClock = trustedClientClock,
                Backfill = backfill
            };

            _eventBuffer.Push(eventBody);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error enqueueing event");
        }
    }

    /// <summary>
/// Submit a flag check event to track analytics about flag usage
/// </summary>
private void SubmitFlagCheckEvent(
    string flagKey,
    bool value,
    Dictionary<string, string>? company,
    Dictionary<string, string>? user,
    EventBodyFlagCheck? body = null,
    string? error = null)
{
    try
    {
        var eventBody = new EventBodyFlagCheck
        {
            FlagKey = flagKey,
            Value = value,
            Reason = body?.Reason ?? "",
            FlagId = body?.FlagId,
            RuleId = body?.RuleId,
            CompanyId = body?.CompanyId,
            UserId = body?.UserId,
            Error = error,
            ReqCompany = company,
            ReqUser = user
        };

        _logger.LogDebug("Submitting flag check event: {FlagKey}", flagKey);

        EnqueueEvent(EventType.FlagCheck, eventBody);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error submitting flag check event");
    }
}

    // Helper method to submit flag check event for cached/simplified values
    private void SubmitFlagCheckEventForValue(
        string flagKey,
        bool value,
        Dictionary<string, string>? company,
        Dictionary<string, string>? user,
        string reason)
    {
        SubmitFlagCheckEvent(
            flagKey,
            value,
            company,
            user,
            new EventBodyFlagCheck
            {
                FlagKey = flagKey,
                Value = value,
                Reason = reason
            });
    }

    public int GetBufferWaitingEventCount()
    {
        return this._eventBuffer.GetEventCount();
    }

    /// <summary>
    /// Gets whether the external replicator is healthy (only valid in replicator mode)
    /// </summary>
    public bool IsReplicatorHealthy()
    {
        return _datastreamClient?.IsReplicatorReady() == true;
    }

    /// <summary>
    /// Gets whether the client is running in replicator mode
    /// </summary>
    public bool IsReplicatorMode()
    {
        return _replicatorMode;
    }

    /// <summary>
    /// Gets whether the client is using datastream
    /// </summary>
    private bool UseDatastream()
    {
        return _datastreamClient != null;
    }

    public void SetFlagDefault(string flag, bool value)
    {
        if (_options.FlagDefaults == null)
        {
            _options.FlagDefaults = new Dictionary<string, bool>();
        }
        _options.FlagDefaults[flag] = value;
    }

    public void SetFlagDefaults(Dictionary<string, bool> values)
    {
        foreach (var kvp in values)
        {
            SetFlagDefault(kvp.Key, kvp.Value);
        }
    }

    private bool GetFlagDefault(string flagKey)
    {
        return _options.FlagDefaults != null && _options.FlagDefaults.TryGetValue(flagKey, out bool value) ? value : false;
    }
}

/// <summary>
/// Optional metadata for a track event. Fields map directly to the
/// corresponding <see cref="CreateEventRequestBody"/> properties; unset
/// fields are omitted from the wire payload.
/// </summary>
public class TrackOptions
{
    /// <summary>
    /// Client-supplied dedupe key. Duplicate events with the same key
    /// (scoped to the environment) are dropped server-side for 24 hours.
    /// </summary>
    public string? IdempotencyKey { get; set; }

    /// <summary>
    /// Timestamp the event was sent. Required when <see cref="TrustedClientClock"/>
    /// is true. When set, overrides the SDK's default of UtcNow at enqueue time.
    /// </summary>
    public DateTime? SentAt { get; set; }

    /// <summary>
    /// When true, use <see cref="SentAt"/> as the effective event timestamp
    /// instead of server receipt time. Requires a secret API key and SentAt.
    /// </summary>
    public bool? TrustedClientClock { get; set; }

    /// <summary>
    /// Import historical data without affecting billing. Requires a secret
    /// API key and <see cref="TrustedClientClock"/>.
    /// </summary>
    public bool? Backfill { get; set; }

    /// <summary>
    /// Credit lease this event redeems against. It routes the server-side credit
    /// consumption through the lease's sub-ledger instead of decrementing a grant
    /// the acquire already pre-debited.
    /// </summary>
    public string? LeaseId { get; set; }

    /// <summary>
    /// Credit reservation this event settles. A lease id takes precedence when
    /// both are set.
    /// </summary>
    public string? ReservationId { get; set; }
}

/// <summary>
/// Optional metadata for an identify event. Fields map directly to the
/// corresponding <see cref="CreateEventRequestBody"/> properties; unset
/// fields are omitted from the wire payload.
/// </summary>
public class IdentifyOptions
{
    /// <summary>
    /// Client-supplied dedupe key. Duplicate events with the same key
    /// (scoped to the environment) are dropped server-side for 24 hours.
    /// </summary>
    public string? IdempotencyKey { get; set; }

    /// <summary>
    /// Credit type ids to acquire leases for in the background once the identify
    /// event is enqueued. Failures are logged, never surfaced. Equivalent to
    /// calling <see cref="Schematic.Prewarm"/> with the same company keys, and a
    /// no-op unless CreditLeases is configured.
    /// </summary>
    public List<string>? Prewarm { get; set; }
}

public class OfflineHttpMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent("{\"data\":{}}")
        };
        return Task.FromResult(response);
    }
}
