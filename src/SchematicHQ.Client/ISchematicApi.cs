namespace SchematicHQ.Client;

public partial interface ISchematicApi
{
    public IAccountsClient Accounts { get; }
    public IBillingClient Billing { get; }
    public ICreditsClient Credits { get; }
    public ICatalogsClient Catalogs { get; }
    public ICheckoutClient Checkout { get; }
    public ICompaniesClient Companies { get; }
    public IEntitlementsClient Entitlements { get; }
    public IPlansClient Plans { get; }
    public IComponentsClient Components { get; }
    public IPlanbundleClient Planbundle { get; }
    public IDataexportsClient Dataexports { get; }
    public IEventsClient Events { get; }
    public IFeaturesClient Features { get; }
    public IInsightsClient Insights { get; }
    public IIntegrationsapiClient Integrationsapi { get; }
    public ILicensesClient Licenses { get; }
    public IPlangroupsClient Plangroups { get; }
    public IPlanmigrationsClient Planmigrations { get; }
    public IComponentspublicClient Componentspublic { get; }
    public IScheduledcheckoutClient Scheduledcheckout { get; }
    public IAccesstokensClient Accesstokens { get; }
    public IWebhooksClient Webhooks { get; }
    WithRawResponseTask PostApiKeysAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask PutApiKeysApiKeyIdAsync(
        string apiKeyId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask DeleteApiKeysApiKeyIdAsync(
        string apiKeyId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask PostEnvironmentsAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask PutEnvironmentsEnvironmentIdAsync(
        string environmentId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask DeleteEnvironmentsEnvironmentIdAsync(
        string environmentId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask GetCreditLedgerAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
