using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public partial class SchematicApi
{
    private RawClient _client;

    public SchematicApi(string apiKey = null, ClientOptions clientOptions = null)
    {
        _client = new RawClient(
            new Dictionary<string, string>()
            {
                { "X-Schematic-Api-Key", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "SchematicHQ.Client" },
                { "X-Fern-SDK-Version", "1.0.0" },
            },
            clientOptions ?? new ClientOptions()
        );
        Accounts = new AccountsClient(_client);
        Features = new FeaturesClient(_client);
        Billing = new BillingClient(_client);
        Companies = new CompaniesClient(_client);
        Entitlements = new EntitlementsClient(_client);
        Crm = new CrmClient(_client);
        Events = new EventsClient(_client);
        Plans = new PlansClient(_client);
        Webhooks = new WebhooksClient(_client);
    }

    public AccountsClient Accounts { get; }

    public FeaturesClient Features { get; }

    public BillingClient Billing { get; }

    public CompaniesClient Companies { get; }

    public EntitlementsClient Entitlements { get; }

    public CrmClient Crm { get; }

    public EventsClient Events { get; }

    public PlansClient Plans { get; }

    public WebhooksClient Webhooks { get; }

    private string GetFromEnvironmentOrThrow(string env, string message)
    {
        var value = Environment.GetEnvironmentVariable(env);
        if (value == null)
        {
            throw new Exception(message);
        }
        return value;
    }
}
