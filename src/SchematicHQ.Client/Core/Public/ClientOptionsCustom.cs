using System.Net.Http;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public partial class ClientOptions
{
    public Dictionary<string, bool> FlagDefaults { get; set; }
    public ISchematicLogger Logger { get; set; }
    public List<ICacheProvider<bool?>> CacheProviders { get; set; }
    public bool Offline { get; set; }
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
            CacheProviders = options.CacheProviders ?? new List<ICacheProvider<bool?>>(),
            DefaultEventBufferPeriod = options.DefaultEventBufferPeriod,
            EventBuffer = options.EventBuffer,
            FlagDefaults = options.FlagDefaults ?? new Dictionary<string, bool>(),
            Headers = new Headers(new Dictionary<string, HeaderValue>(options.Headers)),
            HttpClient = httpClient,
            Logger = options.Logger ?? new ConsoleLogger(),
            MaxRetries = options.MaxRetries,
            Offline = options.Offline,
            Timeout = options.Timeout,
        };
    }
}
