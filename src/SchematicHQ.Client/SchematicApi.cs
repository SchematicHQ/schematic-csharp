using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class SchematicApi
{
    private readonly RawClient _client;

    public SchematicApi(string apiKey, ClientOptions? clientOptions = null)
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Schematic-Api-Key", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "SchematicHQ.Client" },
                { "X-Fern-SDK-Version", Version.Current },
                { "User-Agent", "SchematicHQ.Client/1.2.2" },
            }
        );
        clientOptions ??= new ClientOptions();
        foreach (var header in defaultHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        _client = new RawClient(clientOptions);
        Accounts = new AccountsClient(_client);
        Features = new FeaturesClient(_client);
        Billing = new BillingClient(_client);
        Credits = new CreditsClient(_client);
        Checkout = new CheckoutClient(_client);
        Companies = new CompaniesClient(_client);
        Entitlements = new EntitlementsClient(_client);
        Plans = new PlansClient(_client);
        Components = new ComponentsClient(_client);
        Crm = new CrmClient(_client);
        Dataexports = new DataexportsClient(_client);
        Events = new EventsClient(_client);
        Plangroups = new PlangroupsClient(_client);
        Accesstokens = new AccesstokensClient(_client);
        Webhooks = new WebhooksClient(_client);
    }

    public AccountsClient Accounts { get; }

    public FeaturesClient Features { get; }

    public BillingClient Billing { get; }

    public CreditsClient Credits { get; }

    public CheckoutClient Checkout { get; }

    public CompaniesClient Companies { get; }

    public EntitlementsClient Entitlements { get; }

    public PlansClient Plans { get; }

    public ComponentsClient Components { get; }

    public CrmClient Crm { get; }

    public DataexportsClient Dataexports { get; }

    public EventsClient Events { get; }

    public PlangroupsClient Plangroups { get; }

    public AccesstokensClient Accesstokens { get; }

    public WebhooksClient Webhooks { get; }
}
