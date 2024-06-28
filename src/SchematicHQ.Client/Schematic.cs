using OneOf;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client;

public partial class Schematic
{
    private readonly ClientOptions _options;
    private readonly IEventBuffer<CreateEventRequestBody> _eventBuffer;
    private readonly ISchematicLogger _logger;
    private readonly List<ICacheProvider<bool?>> _flagCheckCacheProviders;
    private readonly bool _offline;
    public readonly SchematicApi API;

    public Schematic(string apiKey, ClientOptions? options = null)
    {
        _options = options ?? new ClientOptions();
        _offline = _options.Offline;
        _logger = _options.Logger ?? new ConsoleLogger();

        var httpClient = _offline ? new OfflineHttpClient() : _options.HttpClient;
        API = new SchematicApi(apiKey, _options.WithHttpClient(httpClient));

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
            flushPeriod: options.DefaultEventBufferPeriod  // default flush period
        );
        _eventBuffer.Start();

        _flagCheckCacheProviders = _options.CacheProviders ?? new List<ICacheProvider<bool?>>
        {
            new LocalCache<bool?>()
        };
    }

    public void Shutdown()
    {
        _eventBuffer.Dispose();
    }

    public async Task<bool> CheckFlag(string flagKey, Dictionary<string, string>? company = null, Dictionary<string, string>? user = null)
    {
       
        if (_offline)
            return GetFlagDefault(flagKey);

        try
        {
            string cacheKey = flagKey;
             if (company != null && company.Count > 0)
            {
                cacheKey += ":" + string.Join(";", company.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }

            if (user != null && user.Count > 0)
            {
                cacheKey += ":" + string.Join(";", user.Select(kvp => $"{kvp.Key}={kvp.Value}"));
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

    public void Identify(Dictionary<string, string> keys, EventBodyIdentifyCompany? company = null, string? name = null, Dictionary<string, OneOf<string, double, bool, OneOf<string, double, bool>>>? traits = null)
    {
        EnqueueEvent(CreateEventRequestBodyEventType.Identify, new EventBodyIdentify
        {
            Company = company,
            Keys = keys,
            Name = name,
            Traits = traits
        });
    }

    public void Track(string eventName, Dictionary<string, string>? company = null, Dictionary<string, string>? user = null, Dictionary<string, OneOf<string, double, bool, OneOf<string, double, bool>>>? traits = null)
    {
        EnqueueEvent(CreateEventRequestBodyEventType.Track, new EventBodyTrack
        {
            Company = company,
            Event = eventName,
            Traits = traits,
            User = user
        });
    }

    private void EnqueueEvent(CreateEventRequestBodyEventType eventType, OneOf<EventBodyTrack, EventBodyIdentify> body)
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

    public int GetBufferWaitingEventCount()
    {
        return this._eventBuffer.GetEventCount();
    }

    private bool GetFlagDefault(string flagKey)
    {
        return _options.FlagDefaults?.GetValueOrDefault(flagKey) ?? false;
    }

}

public class OfflineHttpClient : HttpClient
{
    public override HttpResponseMessage Send(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent("{\"data\":{}}")
        };
        return response;
    }
}