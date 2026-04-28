using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class SchematicApi : ISchematicApi
{
    private readonly RawClient _client;

    public SchematicApi(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        clientOptions ??= new ClientOptions();
        var platformHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "SchematicHQ.Client" },
                { "X-Fern-SDK-Version", Version.Current },
                { "User-Agent", "SchematicHQ.Client/1.4.5" },
            }
        );
        foreach (var header in platformHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        var clientOptionsWithAuth = clientOptions.Clone();
        var authHeaders = new Headers(
            new Dictionary<string, string>() { { "X-Schematic-Api-Key", apiKey ?? "" } }
        );
        foreach (var header in authHeaders)
        {
            clientOptionsWithAuth.Headers[header.Key] = header.Value;
        }
        _client = new RawClient(clientOptionsWithAuth);
        Accounts = new AccountsClient(_client);
        Billing = new BillingClient(_client);
        Credits = new CreditsClient(_client);
        Checkout = new CheckoutClient(_client);
        Companies = new CompaniesClient(_client);
        Entitlements = new EntitlementsClient(_client);
        Plans = new PlansClient(_client);
        Components = new ComponentsClient(_client);
        Dataexports = new DataexportsClient(_client);
        Events = new EventsClient(_client);
        Features = new FeaturesClient(_client);
        Insights = new InsightsClient(_client);
        Integrationsapi = new IntegrationsapiClient(_client);
        Planbundle = new PlanbundleClient(_client);
        Plangroups = new PlangroupsClient(_client);
        Planmigrations = new PlanmigrationsClient(_client);
        Componentspublic = new ComponentspublicClient(_client);
        Scheduledcheckout = new ScheduledcheckoutClient(_client);
        Accesstokens = new AccesstokensClient(_client);
        Webhooks = new WebhooksClient(_client);
    }

    public IAccountsClient Accounts { get; }

    public IBillingClient Billing { get; }

    public ICreditsClient Credits { get; }

    public ICheckoutClient Checkout { get; }

    public ICompaniesClient Companies { get; }

    public IEntitlementsClient Entitlements { get; }

    public IPlansClient Plans { get; }

    public IComponentsClient Components { get; }

    public IDataexportsClient Dataexports { get; }

    public IEventsClient Events { get; }

    public IFeaturesClient Features { get; }

    public IInsightsClient Insights { get; }

    public IIntegrationsapiClient Integrationsapi { get; }

    public IPlanbundleClient Planbundle { get; }

    public IPlangroupsClient Plangroups { get; }

    public IPlanmigrationsClient Planmigrations { get; }

    public IComponentspublicClient Componentspublic { get; }

    public IScheduledcheckoutClient Scheduledcheckout { get; }

    public IAccesstokensClient Accesstokens { get; }

    public IWebhooksClient Webhooks { get; }
}
