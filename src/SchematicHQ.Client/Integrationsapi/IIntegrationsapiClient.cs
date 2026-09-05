namespace SchematicHQ.Client;

public partial interface IIntegrationsapiClient
{
    WithRawResponseTask<RunIntegrationResponse> RunIntegrationAsync(
        string integrationId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListIntegrationsResponse> ListIntegrationsAsync(
        ListIntegrationsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetIntegrationWebhookUrlResponse> GetIntegrationWebhookUrlAsync(
        string type,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<InstallIntegrationResponse> InstallIntegrationAsync(
        InstallIntegrationRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<StartDataImportResponse> StartDataImportAsync(
        StartDataImportRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<LoadSampleDataSetResponse> LoadSampleDataSetAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetStripeSandboxClaimLinkResponse> GetStripeSandboxClaimLinkAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetStripeSandboxKeysResponse> GetStripeSandboxKeysAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ClaimStripeSandboxKeysResponse> ClaimStripeSandboxKeysAsync(
        ClaimStripeSandboxKeysRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<AssumeStripeInstalledResponse> AssumeStripeInstalledAsync(
        InstallIntegrationRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<InstallStripeResponse> InstallStripeAsync(
        InstallIntegrationRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<InstallStripeClaimableSandboxResponse> InstallStripeClaimableSandboxAsync(
        InstallStripeSandboxRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListStripeSandboxCountriesResponse> ListStripeSandboxCountriesAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UninstallIntegrationResponse> UninstallIntegrationAsync(
        string integrationId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
