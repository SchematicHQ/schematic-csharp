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

    WithRawResponseTask<StartDataImportResponse> StartDataImportAsync(
        StartDataImportRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<LoadSampleDataSetV2Response> LoadSampleDataSetV2Async(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UninstallIntegrationResponse> UninstallIntegrationAsync(
        string integrationId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
