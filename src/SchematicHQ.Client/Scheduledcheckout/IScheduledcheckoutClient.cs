namespace SchematicHQ.Client;

public partial interface IScheduledcheckoutClient
{
    WithRawResponseTask<ListScheduledCheckoutsResponse> ListScheduledCheckoutsAsync(
        ListScheduledCheckoutsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateScheduledCheckoutResponse> CreateScheduledCheckoutAsync(
        CreateScheduledCheckoutRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetScheduledCheckoutResponse> GetScheduledCheckoutAsync(
        string scheduledCheckoutId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateScheduledCheckoutResponse> UpdateScheduledCheckoutAsync(
        string scheduledCheckoutId,
        UpdateScheduledCheckoutRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
