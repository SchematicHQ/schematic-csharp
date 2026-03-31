namespace SchematicHQ.Client;

public partial interface IFeaturesClient
{
    WithRawResponseTask<ListFeaturesResponse> ListFeaturesAsync(
        ListFeaturesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateFeatureResponse> CreateFeatureAsync(
        CreateFeatureRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetFeatureResponse> GetFeatureAsync(
        string featureId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateFeatureResponse> UpdateFeatureAsync(
        string featureId,
        UpdateFeatureRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteFeatureResponse> DeleteFeatureAsync(
        string featureId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountFeaturesResponse> CountFeaturesAsync(
        CountFeaturesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListFlagsResponse> ListFlagsAsync(
        ListFlagsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateFlagResponse> CreateFlagAsync(
        CreateFlagRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetFlagResponse> GetFlagAsync(
        string flagId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateFlagResponse> UpdateFlagAsync(
        string flagId,
        CreateFlagRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteFlagResponse> DeleteFlagAsync(
        string flagId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateFlagRulesResponse> UpdateFlagRulesAsync(
        string flagId,
        UpdateFlagRulesRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CheckFlagResponse> CheckFlagAsync(
        string key,
        CheckFlagRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CheckFlagsResponse> CheckFlagsAsync(
        CheckFlagRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CheckFlagsBulkResponse> CheckFlagsBulkAsync(
        CheckFlagsBulkRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountFlagsResponse> CountFlagsAsync(
        CountFlagsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
