namespace SchematicHQ.Client;

public partial interface IWebhooksClient
{
    WithRawResponseTask<ListWebhookEventsResponse> ListWebhookEventsAsync(
        ListWebhookEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetWebhookEventResponse> GetWebhookEventAsync(
        string webhookEventId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountWebhookEventsResponse> CountWebhookEventsAsync(
        CountWebhookEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListWebhooksResponse> ListWebhooksAsync(
        ListWebhooksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateWebhookResponse> CreateWebhookAsync(
        CreateWebhookRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetWebhookResponse> GetWebhookAsync(
        string webhookId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateWebhookResponse> UpdateWebhookAsync(
        string webhookId,
        UpdateWebhookRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteWebhookResponse> DeleteWebhookAsync(
        string webhookId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountWebhooksResponse> CountWebhooksAsync(
        CountWebhooksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
