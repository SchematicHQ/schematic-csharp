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
    private bool _datastreamConnected;
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
        _logger = _options.Logger ?? new ConsoleLogger();

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

        // Initialize datastream if enabled
        if (!_offline && _options.UseDatastream)
        {
            // Create DatastreamOptions with cache settings from _options.CacheConfiguration
            var datastreamOptions = _options.DatastreamOptions ?? new DatastreamOptions();

            // Apply cache settings from the main configuration
            if (_options.CacheConfiguration != null)
            {
                // Set cache provider type based on main configuration
                datastreamOptions.CacheProviderType = _options.CacheConfiguration.ProviderType == CacheProviderType.Redis
                    ? DatastreamCacheProviderType.Redis
                    : DatastreamCacheProviderType.Local;

                // Pass through the Redis settings if using Redis
                if (datastreamOptions.CacheProviderType == DatastreamCacheProviderType.Redis)
                {
                    datastreamOptions.RedisConfig = _options.CacheConfiguration.RedisConfig;
                }

                // Apply local cache settings
                datastreamOptions.LocalCacheCapacity = _options.CacheConfiguration.LocalCacheCapacity;

                // Apply cache TTL
                datastreamOptions.CacheTTL = _options.CacheConfiguration.CacheTtl;
            }
            _datastreamClient = new DatastreamClientAdapter(
                _options.BaseUrl,
                _logger,
                apiKey,
                datastreamOptions
            );
            _datastreamClient.Start();
            _datastreamConnected = true;
        }
    }

    public async Task Shutdown()
    {
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

        // Use datastream if enabled and connected
        if (_datastreamConnected && _datastreamClient != null)
        {
            try
            {
                var flagResult = await _datastreamClient.CheckFlag(company, user, flagKey);
                // Submit flag check event for successful API evaluation
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
                _logger.Error("Error checking flag via datastream: {0}", ex.Message);
                return GetFlagDefault(flagKey);
            }
        }

        // Fall back to API request
        try
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

            foreach (var provider in _flagCheckCacheProviders)
            {
                if (provider.Get(cacheKey) is bool cachedValue)
                    return cachedValue;
            }
            var requestBody = new CheckFlagRequestBody
            {
                Company = company,
                User = user
            };

            var response = await API.Features.CheckFlagAsync(flagKey, requestBody);

            if (response == null){

                return GetFlagDefault(flagKey);
            }

            foreach (var provider in _flagCheckCacheProviders)
            {
                provider.Set(cacheKey, response.Data.Value);
            }

            return response.Data.Value;
        }
        catch (Exception ex)
        {
            _logger.Error("Error checking flag: {0}", ex.Message);

            return GetFlagDefault(flagKey);
        }
    }

    public void Identify(Dictionary<string, string> keys, EventBodyIdentifyCompany? company = null, string? name = null, Dictionary<string, object>? traits = null)
    {
        EnqueueEvent(CreateEventRequestBodyEventType.Identify, new EventBodyIdentify
        {
            Company = company,
            Keys = keys,
            Name = name,
            Traits = traits
        });
    }

    public void Track(string eventName, Dictionary<string, string>? company = null, Dictionary<string, string>? user = null, Dictionary<string, object>? traits = null)
    {
        EnqueueEvent(CreateEventRequestBodyEventType.Track, new EventBodyTrack
        {
            Company = company,
            Event = eventName,
            Traits = traits,
            User = user
        });
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

    public int GetBufferWaitingEventCount()
    {
        return this._eventBuffer.GetEventCount();
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
