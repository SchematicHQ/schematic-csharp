namespace SchematicHQ.Client;

public partial interface IIntegrationsapiClient
{
    WithRawResponseTask<GetIntegrationWebhookUrlResponse> GetIntegrationWebhookUrlAsync(
        string type,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
