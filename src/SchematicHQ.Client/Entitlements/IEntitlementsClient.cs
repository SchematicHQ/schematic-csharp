namespace SchematicHQ.Client;

public partial interface IEntitlementsClient
{
    WithRawResponseTask<ListCompanyOverridesResponse> ListCompanyOverridesAsync(
        ListCompanyOverridesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateCompanyOverrideResponse> CreateCompanyOverrideAsync(
        CreateCompanyOverrideRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetCompanyOverrideResponse> GetCompanyOverrideAsync(
        string companyOverrideId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateCompanyOverrideResponse> UpdateCompanyOverrideAsync(
        string companyOverrideId,
        UpdateCompanyOverrideRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteCompanyOverrideResponse> DeleteCompanyOverrideAsync(
        string companyOverrideId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountCompanyOverridesResponse> CountCompanyOverridesAsync(
        CountCompanyOverridesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListFeatureCompaniesResponse> ListFeatureCompaniesAsync(
        ListFeatureCompaniesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountFeatureCompaniesResponse> CountFeatureCompaniesAsync(
        CountFeatureCompaniesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListFeatureUsageResponse> ListFeatureUsageAsync(
        ListFeatureUsageRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetFeatureUsageTimeSeriesResponse> GetFeatureUsageTimeSeriesAsync(
        GetFeatureUsageTimeSeriesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountFeatureUsageResponse> CountFeatureUsageAsync(
        CountFeatureUsageRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListFeatureUsersResponse> ListFeatureUsersAsync(
        ListFeatureUsersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountFeatureUsersResponse> CountFeatureUsersAsync(
        CountFeatureUsersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListPlanEntitlementsResponse> ListPlanEntitlementsAsync(
        ListPlanEntitlementsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreatePlanEntitlementResponse> CreatePlanEntitlementAsync(
        CreatePlanEntitlementRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetPlanEntitlementResponse> GetPlanEntitlementAsync(
        string planEntitlementId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdatePlanEntitlementResponse> UpdatePlanEntitlementAsync(
        string planEntitlementId,
        UpdatePlanEntitlementRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeletePlanEntitlementResponse> DeletePlanEntitlementAsync(
        string planEntitlementId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountPlanEntitlementsResponse> CountPlanEntitlementsAsync(
        CountPlanEntitlementsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DuplicatePlanEntitlementsResponse> DuplicatePlanEntitlementsAsync(
        DuplicatePlanEntitlementsRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetFeatureUsageByCompanyResponse> GetFeatureUsageByCompanyAsync(
        GetFeatureUsageByCompanyRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
