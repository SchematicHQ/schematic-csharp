using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public partial class ClientOptions
{
    public Dictionary<string, bool> FlagDefaults { get; set; }
    public ISchematicLogger Logger { get; set; }
    public List<ICacheProvider<bool>> CacheProviders { get; set; }
    public bool Offline { get; set; }
    public int? EventBufferPeriod { get; set; }
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
            FlagDefaults = options.FlagDefaults,
            Logger = options.Logger,
            CacheProviders = options.CacheProviders,
            Offline = options.Offline,
            EventBufferPeriod = options.EventBufferPeriod
        };
    }
}
