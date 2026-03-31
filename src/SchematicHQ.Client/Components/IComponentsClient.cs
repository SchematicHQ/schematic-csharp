namespace SchematicHQ.Client;

public partial interface IComponentsClient
{
    WithRawResponseTask<ListComponentsResponse> ListComponentsAsync(
        ListComponentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateComponentResponse> CreateComponentAsync(
        CreateComponentRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetComponentResponse> GetComponentAsync(
        string componentId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateComponentResponse> UpdateComponentAsync(
        string componentId,
        UpdateComponentRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteComponentResponse> DeleteComponentAsync(
        string componentId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountComponentsResponse> CountComponentsAsync(
        CountComponentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<PreviewComponentDataResponse> PreviewComponentDataAsync(
        PreviewComponentDataRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
