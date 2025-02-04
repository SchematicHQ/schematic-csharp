using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public partial class SchematicApi
{
    private RawClient _client;

    public SchematicApi(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Schematic-Api-Key", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "SchematicHQ.Client" },
                { "X-Fern-SDK-Version", Version.Current },
                { "User-Agent", "SchematicHQ.Client/1.0.6" },
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
        Checkout = new CheckoutClient(_client);
        Companies = new CompaniesClient(_client);
        Entitlements = new EntitlementsClient(_client);
        Plans = new PlansClient(_client);
        Components = new ComponentsClient(_client);
        Crm = new CrmClient(_client);
        Events = new EventsClient(_client);
        Plangroups = new PlangroupsClient(_client);
        Accesstokens = new AccesstokensClient(_client);
        Webhooks = new WebhooksClient(_client);
    }

    public AccountsClient Accounts { get; init; }

    public FeaturesClient Features { get; init; }

    public BillingClient Billing { get; init; }

    public CheckoutClient Checkout { get; init; }

    public CompaniesClient Companies { get; init; }

    public EntitlementsClient Entitlements { get; init; }

    public PlansClient Plans { get; init; }

    public ComponentsClient Components { get; init; }

    public CrmClient Crm { get; init; }

    public EventsClient Events { get; init; }

    public PlangroupsClient Plangroups { get; init; }

    public AccesstokensClient Accesstokens { get; init; }

    public WebhooksClient Webhooks { get; init; }
}
