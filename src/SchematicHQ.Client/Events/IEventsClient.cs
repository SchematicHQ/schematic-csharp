namespace SchematicHQ.Client;

public partial interface IEventsClient
{
    WithRawResponseTask<CreateEventBatchResponse> CreateEventBatchAsync(
        CreateEventBatchRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetEventSummariesResponse> GetEventSummariesAsync(
        GetEventSummariesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListEventsResponse> ListEventsAsync(
        ListEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateEventResponse> CreateEventAsync(
        CreateEventRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetEventResponse> GetEventAsync(
        string eventId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetSegmentIntegrationStatusResponse> GetSegmentIntegrationStatusAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
