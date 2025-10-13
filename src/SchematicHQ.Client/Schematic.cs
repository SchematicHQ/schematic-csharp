using OneOf;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public partial class Schematic
{
    private readonly ClientOptions _options;
    private readonly IEventBuffer<CreateEventRequestBody> _eventBuffer;
    private readonly ISchematicLogger _logger;
    private readonly List<ICacheProvider<bool?>> _flagCheckCacheProviders;
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
    public CrmClient Crm { get; init; }
    public EntitlementsClient Entitlements { get; init; }
    public EventsClient Events { get; init; }
    public FeaturesClient Features { get; init; }
    public PlangroupsClient Plangroups { get; init; }
    public PlansClient Plans { get; init; }
    public WebhooksClient Webhooks { get; init; }

    public Schematic(string apiKey, ClientOptions? options = null)
    {
        _options = options ?? new ClientOptions();
        _offline = _options.Offline;
        _replicatorMode = _options.ReplicatorMode;
        _logger = _options.Logger ?? new ConsoleLogger();

        // Validate replicator mode configuration
        if (_replicatorMode && string.IsNullOrWhiteSpace(_options.ReplicatorHealthUrl))
        {
            throw new ArgumentException("ReplicatorHealthUrl is required when ReplicatorMode is enabled");
        }

        var httpClient = _offline ? new HttpClient(new OfflineHttpMessageHandler()) : _options.HttpClient;
        API = new SchematicApi(apiKey, _options.WithHttpClient(httpClient));
        Accesstokens = API.Accesstokens;
        Accounts = API.Accounts;
        Billing = API.Billing;
        Checkout = API.Checkout;
        Companies = API.Companies;
        Components = API.Components;
        Crm = API.Crm;
        Entitlements = API.Entitlements;
        Events = API.Events;
        Features = API.Features;
        Plangroups = API.Plangroups;
        Plans = API.Plans;
        Webhooks = API.Webhooks;

        _eventBuffer = _options.EventBuffer ?? new EventBuffer<CreateEventRequestBody>(
            async items =>
            {
                var request = new CreateEventBatchRequestBody
                {
                    Events = items
                };
                await API.Events.CreateEventBatchAsync(request);
            },
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
            _flagCheckCacheProviders = new List<ICacheProvider<bool?>>();

            switch (_options.CacheConfiguration.ProviderType)
            {
                case CacheProviderType.Redis:
                    if (_options.CacheConfiguration.RedisConfig == null)
                    {
                        _logger.Warn("Redis configuration not provided, falling back to local cache");
                        _flagCheckCacheProviders.Add(new LocalCache<bool?>());
                    }
                    else
                    {
                        // Ensure the config has the cache TTL set
                        if (!_options.CacheConfiguration.RedisConfig.CacheTTL.HasValue && _options.CacheConfiguration.CacheTtl.HasValue)
                        {
                            _options.CacheConfiguration.RedisConfig.CacheTTL = _options.CacheConfiguration.CacheTtl;
                        }

                        RedisCache<bool?> redisCache = new RedisCache<bool?>(_options.CacheConfiguration.RedisConfig);
                        _flagCheckCacheProviders.Add(redisCache);
                    }
                    break;

                case CacheProviderType.Local:
                default:
                    _flagCheckCacheProviders.Add(new LocalCache<bool?>(
                        _options.CacheConfiguration.LocalCacheCapacity,
                        _options.CacheConfiguration.CacheTtl
                    ));
                    break;
            }
        }
        else
        {
            // Default to local cache
            _flagCheckCacheProviders = new List<ICacheProvider<bool?>>
            {
                new LocalCache<bool?>()
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
                _logger.Info("Replicator mode enabled - datastream client created for cache access only");
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
                                _logger.Info("Datastream connection established");
                            }
                            else
                            {
                                _logger.Warn("Datastream connection lost, falling back to API");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"Error monitoring datastream connection: {ex.Message}");
                    }

                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Connection monitoring stopped: {ex.Message}");
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
        if (_offline)
            return GetFlagDefault(flagKey);

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
                return flagResult.Value;
            }
            catch (Exception ex)
            {
                // Fall back to API if datastream fails
                _logger.Debug("Datastream flag check failed ({0}), falling back to API", ex.Message);
                return await CheckFlagApi(flagKey, company, user);
            }
        }

        // Fall back to API request
        return await CheckFlagApi(flagKey, company, user);
    }

    private async Task<bool> CheckFlagApi(string flagKey, Dictionary<string, string>? company, Dictionary<string, string>? user)
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
                if (provider.Get(cacheKey) is bool cachedValue)
                {
                    // Submit flag check event for cached value
                    SubmitFlagCheckEventForValue(flagKey, cachedValue, company, user, "cache");
                    return cachedValue;
                }
            }

            // Make API request
            var response = await API.Features.CheckFlagAsync(flagKey, requestBody);

            if (response == null)
            {
                // If the client was not initialized with an API key, we'll have a no-op here which returns an empty response
                return GetFlagDefault(flagKey);
            }

            // Cache the result
            foreach (var provider in _flagCheckCacheProviders)
            {
                try
                {
                    provider.Set(cacheKey, response.Data.Value);
                }
                catch (Exception cacheEx)
                {
                    _logger.Error("Error caching flag result: {0}", cacheEx.Message);
                }
            }

            return response.Data.Value;
        }
        catch (Exception ex)
        {
            _logger.Error("Error checking flag via API: {0}", ex.Message);
            return GetFlagDefault(flagKey);
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
        EnqueueEvent(CreateEventRequestBodyEventType.Identify, new EventBodyIdentify
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
        
        EnqueueEvent(CreateEventRequestBodyEventType.Track, eventBody);
        
        // Update company metrics in datastream if available and connected
        if (company != null && UseDatastream() && _datastreamClient != null && _datastreamConnected)
        {
            try
            {
                var success = _datastreamClient.UpdateCompanyMetrics(eventBody);
                if (!success)
                {
                    _logger.Error("Failed to update company metrics: datastream update failed");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to update company metrics: {0}", ex.Message);
            }
        }
    }

    private void EnqueueEvent(CreateEventRequestBodyEventType eventType, OneOf<EventBodyTrack, EventBodyFlagCheck, EventBodyIdentify> body)
    {
        if (_offline)
            return;

        try
        {
            var eventBody = new CreateEventRequestBody
            {
                EventType = eventType,
                Body = body
            };

            _eventBuffer.Push(eventBody);
        }
        catch (Exception ex)
        {
            _logger.Error("Error enqueueing event: {0}", ex.Message);
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

        _logger.Debug("Submitting flag check event: {0}", flagKey);

        EnqueueEvent(CreateEventRequestBodyEventType.FlagCheck, eventBody);
    }
    catch (Exception ex)
    {
        _logger.Error("Error submitting flag check event: {0}", ex.Message);
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
