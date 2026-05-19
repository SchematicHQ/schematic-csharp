namespace SchematicHQ.Client;

public partial interface IPlanbundleClient
{
    WithRawResponseTask<CreateCustomPlanBundleResponse> CreateCustomPlanBundleAsync(
        CreateCustomPlanBundleRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreatePlanBundleResponse> CreatePlanBundleAsync(
        CreatePlanBundleRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdatePlanBundleResponse> UpdatePlanBundleAsync(
        string planBundleId,
        UpdatePlanBundleRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
