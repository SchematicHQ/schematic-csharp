namespace SchematicHQ.Client;

public partial interface ISchematicApi
{
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
    public IIntegrationsapiClient Integrationsapi { get; }
    public IPlanbundleClient Planbundle { get; }
    public IPlangroupsClient Plangroups { get; }
    public IPlanmigrationsClient Planmigrations { get; }
    public IComponentspublicClient Componentspublic { get; }
    public IScheduledcheckoutClient Scheduledcheckout { get; }
    public IAccesstokensClient Accesstokens { get; }
    public IWebhooksClient Webhooks { get; }
    Task PutPlanAudiencesPlanAudienceIdAsync(
        string planAudienceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    Task DeletePlanAudiencesPlanAudienceIdAsync(
        string planAudienceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
