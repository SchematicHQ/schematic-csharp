using System.Net.Http;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client.Core;

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
            HttpClient = httpClient,
            MaxRetries = options.MaxRetries,
            TimeoutInSeconds = options.TimeoutInSeconds,
            FlagDefaults = options.FlagDefaults ?? new Dictionary<string, bool>(),
            Logger = options.Logger ?? new ConsoleLogger(),
            CacheProviders = options.CacheProviders ?? new List<ICacheProvider<bool?>>(),
            Offline = options.Offline,
            DefaultEventBufferPeriod = options.DefaultEventBufferPeriod,
            EventBuffer = options.EventBuffer
        };
    }
}
