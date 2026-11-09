namespace SchematicHQ.Client;

public partial interface ICustomplangrouprolloutsClient
{
    WithRawResponseTask<ListGroupRolloutsResponse> ListGroupRolloutsAsync(
        ListGroupRolloutsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetGroupRolloutResponse> GetGroupRolloutAsync(
        string customPlanGroupRolloutId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CopyCustomPlanResponse> CopyCustomPlanAsync(
        string planId,
        CopyCustomPlanRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<PreviewGroupRolloutResponse> PreviewGroupRolloutAsync(
        string planId,
        PreviewGroupRolloutRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<StartGroupRolloutResponse> StartGroupRolloutAsync(
        string planId,
        StartGroupRolloutRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
