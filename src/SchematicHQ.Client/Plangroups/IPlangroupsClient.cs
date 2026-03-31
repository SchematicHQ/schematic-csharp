namespace SchematicHQ.Client;

public partial interface IPlangroupsClient
{
    WithRawResponseTask<GetPlanGroupResponse> GetPlanGroupAsync(
        GetPlanGroupRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreatePlanGroupResponse> CreatePlanGroupAsync(
        CreatePlanGroupRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdatePlanGroupResponse> UpdatePlanGroupAsync(
        string planGroupId,
        UpdatePlanGroupRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
