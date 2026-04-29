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
using SchematicHQ.Client.RulesEngine;

#nullable enable

namespace SchematicHQ.Client;

public partial class Schematic
{
    private readonly ClientOptions _options;
    private readonly IEventBuffer<CreateEventRequestBody> _eventBuffer;
    private readonly ILogger _logger;
    private readonly List<ICacheProvider<CheckFlagWithEntitlementResponse?>> _flagCheckCacheProviders;
    private readonly bool _offline;
    private readonly DatastreamClientAdapter? _datastreamClient;
    private readonly bool _replicatorMode;
    private bool _datastreamConnected;
    private bool _disposed;
    public readonly SchematicApi API;

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

            // Check if Redis is configured via DatastreamOptions
            if (_options.DatastreamOptions?.CacheProviderType == DatastreamCacheProviderType.Redis && 
                _options.DatastreamOptions.RedisConfig != null)
            {
                hasRedisCache = true;
            }

            // Check if explicit Redis cache providers are configured
            if (_options.CacheProviders.Count > 0)
            {
                foreach (var provider in _options.CacheProviders)
                {
                    if (provider is RedisCache<CheckFlagWithEntitlementResponse?>)
                    {
                        hasRedisCache = true;
                        break;
                    }
                }
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
        if (_options.CacheProviders.Count > 0)
        {
            // Use explicitly provided cache providers
            _flagCheckCacheProviders = _options.CacheProviders;
        }
        else if (_options.CacheConfiguration != null)
        {
            // Create cache providers based on configuration
            _flagCheckCacheProviders = new List<ICacheProvider<CheckFlagWithEntitlementResponse?>>();

            switch (_options.CacheConfiguration.ProviderType)
            {
                case CacheProviderType.Redis:
                    if (_options.CacheConfiguration.RedisConfig == null)
                    {
                        _logger.LogWarning("Redis configuration not provided, falling back to local cache");
                        _flagCheckCacheProviders.Add(new LocalCache<CheckFlagWithEntitlementResponse?>());
                    }
                    else
                    {
                        // Ensure the config has the cache TTL set
                        if (!_options.CacheConfiguration.RedisConfig.CacheTTL.HasValue && _options.CacheConfiguration.CacheTtl.HasValue)
                        {
                            _options.CacheConfiguration.RedisConfig.CacheTTL = _options.CacheConfiguration.CacheTtl;
                        }

                        RedisCache<CheckFlagWithEntitlementResponse?> redisCache = new RedisCache<CheckFlagWithEntitlementResponse?>(_options.CacheConfiguration.RedisConfig);
                        _flagCheckCacheProviders.Add(redisCache);
                    }
                    break;

                case CacheProviderType.Local:
                default:
                    _flagCheckCacheProviders.Add(new LocalCache<CheckFlagWithEntitlementResponse?>(
                        _options.CacheConfiguration.LocalCacheCapacity,
                        _options.CacheConfiguration.CacheTtl
                    ));
                    break;
            }
        }
        else
        {
            // Default to local cache
            _flagCheckCacheProviders = new List<ICacheProvider<CheckFlagWithEntitlementResponse?>>
            {
                new LocalCache<CheckFlagWithEntitlementResponse?>()
            };
        }

        // Initialize datastream if enabled or in replicator mode (for cache access)
        if (!_offline && (_options.UseDatastream || _replicatorMode))
        {
            // Create DatastreamOptions with cache settings from _options.CacheConfiguration
            var datastreamOptions = _options.DatastreamOptions ?? new DatastreamOptions();

            // Apply cache settings from the main configuration if not set in DatastreamOptions
            if (_options.CacheConfiguration != null && datastreamOptions.RedisConfig == null)
            {
                // Set cache provider type based on main configuration
                datastreamOptions.CacheProviderType = _options.CacheConfiguration.ProviderType == CacheProviderType.Redis
                    ? DatastreamCacheProviderType.Redis
                    : DatastreamCacheProviderType.Local;

                // Pass through the Redis settings if using Redis unless explicitly set in DatastreamOptions
                if (datastreamOptions.CacheProviderType == DatastreamCacheProviderType.Redis)
                {
                    datastreamOptions.RedisConfig = _options.CacheConfiguration.RedisConfig;
                }

                // Apply local cache settings
                datastreamOptions.LocalCacheCapacity = _options.CacheConfiguration.LocalCacheCapacity;

                // Apply cache TTL if not set in DatastreamOptions
                datastreamOptions.CacheTTL = datastreamOptions.CacheTTL ?? _options.CacheConfiguration.CacheTtl;
            }

            _datastreamClient = new DatastreamClientAdapter(
                _options.BaseUrl,
                _logger,
                apiKey,
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

    public async Task Shutdown()
    {
        _disposed = true;
        
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

    public async Task<CheckFlagWithEntitlementResponse> CheckFlagWithEntitlement(string flagKey, Dictionary<string, string>? company = null, Dictionary<string, string>? user = null)
    {
        if (_offline)
            return new CheckFlagWithEntitlementResponse
            {
                FlagKey = flagKey,
                Value = GetFlagDefault(flagKey),
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
                var flagResult = await _datastreamClient.CheckFlag(request, flagKey);

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
                return await CheckFlagWithEntitlementApi(flagKey, company, user);
            }
        }

        // Fall back to API request
        return await CheckFlagWithEntitlementApi(flagKey, company, user);
    }

    private async Task<CheckFlagWithEntitlementResponse> CheckFlagWithEntitlementApi(string flagKey, Dictionary<string, string>? company, Dictionary<string, string>? user)
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
            foreach (var provider in _flagCheckCacheProviders)
            {
                var cachedResponse = provider.Get(cacheKey);
                if (cachedResponse != null)
                {
                    // Submit flag check event for cached value
                    SubmitFlagCheckEventForValue(flagKey, cachedResponse.Value, company, user, "cache");
                    return cachedResponse;
                }
            }

            // Make API request
            var apiResponse = await API.Features.CheckFlagAsync(flagKey, requestBody);

            if (apiResponse == null)
            {
                // If the client was not initialized with an API key, we'll have a no-op here which returns an empty response
                return new CheckFlagWithEntitlementResponse
                {
                    FlagKey = flagKey,
                    Value = GetFlagDefault(flagKey),
                    Reason = "no response"
                };
            }

            var result = CheckFlagWithEntitlementResponse.FromApiResponse(apiResponse.Data, flagKey);

            // Cache the result
            foreach (var provider in _flagCheckCacheProviders)
            {
                try
                {
                    provider.Set(cacheKey, result);
                }
                catch (Exception cacheEx)
                {
                    _logger.LogError(cacheEx, "Error caching flag result");
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking flag via API");
            return new CheckFlagWithEntitlementResponse
            {
                FlagKey = flagKey,
                Value = GetFlagDefault(flagKey),
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
                CheckFlagWithEntitlementResponse? cached = null;
                foreach (var provider in _flagCheckCacheProviders)
                {
                    cached = provider.Get(cacheKey);
                    if (cached != null) break;
                }
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
                foreach (var provider in _flagCheckCacheProviders)
                {
                    try
                    {
                        provider.Set(cacheKey, responseForCache);
                    }
                    catch (Exception cacheEx)
                    {
                        _logger.LogError(cacheEx, "Error caching flag result");
                    }
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

    public void Identify(Dictionary<string, string> keys, EventBodyIdentifyCompany? company = null, string? name = null, Dictionary<string, object?>? traits = null)
    {
        EnqueueEvent(EventType.Identify, new EventBodyIdentify
        {
            Company = company,
            Keys = keys,
            Name = name,
            Traits = traits
        });
    }

    public void Track(string eventName, Dictionary<string, string>? company = null, Dictionary<string, string>? user = null, Dictionary<string, object?>? traits = null, int? quantity = null)
    {
        var eventBody = new EventBodyTrack
        {
            Company = company,
            Event = eventName,
            Traits = traits,
            User = user,
            Quantity = quantity
        };
        
        EnqueueEvent(EventType.Track, eventBody);
        
        // Update company metrics in datastream if available and connected
        if (company != null && UseDatastream() && _datastreamClient != null && _datastreamConnected)
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

    private void EnqueueEvent(EventType eventType, OneOf<EventBodyTrack, EventBodyFlagCheck, EventBodyIdentify> body)
    {
        if (_offline)
            return;

        try
        {
            var eventBody = new CreateEventRequestBody
            {
                EventType = eventType,
                Body = body,
                SentAt = DateTime.UtcNow
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
