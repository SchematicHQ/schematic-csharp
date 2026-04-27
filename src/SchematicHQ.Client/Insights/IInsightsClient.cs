namespace SchematicHQ.Client;

public partial interface IInsightsClient
{
    WithRawResponseTask<GetActivityResponse> GetActivityAsync(
        GetActivityRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetEnvironmentFeatureUsageTimeSeriesResponse> GetEnvironmentFeatureUsageTimeSeriesAsync(
        GetEnvironmentFeatureUsageTimeSeriesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetPlanGrowthResponse> GetPlanGrowthAsync(
        GetPlanGrowthRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetSummaryResponse> GetSummaryAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetTopFeaturesByUsageResponse> GetTopFeaturesByUsageAsync(
        GetTopFeaturesByUsageRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetEnvironmentTraitUsageTimeSeriesResponse> GetEnvironmentTraitUsageTimeSeriesAsync(
        GetEnvironmentTraitUsageTimeSeriesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
