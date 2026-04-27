namespace SchematicHQ.Client;

public partial interface ICreditsClient
{
    WithRawResponseTask<ListBillingCreditsResponse> ListBillingCreditsAsync(
        ListBillingCreditsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateBillingCreditResponse> CreateBillingCreditAsync(
        CreateBillingCreditRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetSingleBillingCreditResponse> GetSingleBillingCreditAsync(
        string creditId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateBillingCreditResponse> UpdateBillingCreditAsync(
        string creditId,
        UpdateBillingCreditRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<SoftDeleteBillingCreditResponse> SoftDeleteBillingCreditAsync(
        string creditId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListCompanyCreditBalancesResponse> ListCompanyCreditBalancesAsync(
        ListCompanyCreditBalancesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListCreditBundlesResponse> ListCreditBundlesAsync(
        ListCreditBundlesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateCreditBundleResponse> CreateCreditBundleAsync(
        CreateCreditBundleRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetCreditBundleResponse> GetCreditBundleAsync(
        string bundleId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateCreditBundleDetailsResponse> UpdateCreditBundleDetailsAsync(
        string bundleId,
        UpdateCreditBundleDetailsRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteCreditBundleResponse> DeleteCreditBundleAsync(
        string bundleId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountCreditBundlesResponse> CountCreditBundlesAsync(
        CountCreditBundlesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountBillingCreditsResponse> CountBillingCreditsAsync(
        CountBillingCreditsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ZeroOutGrantResponse> ZeroOutGrantAsync(
        string grantId,
        ZeroOutGrantRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GrantBillingCreditsToCompanyResponse> GrantBillingCreditsToCompanyAsync(
        CreateCompanyCreditGrant request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountCompanyGrantsResponse> CountCompanyGrantsAsync(
        CountCompanyGrantsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListCompanyGrantsResponse> ListCompanyGrantsAsync(
        ListCompanyGrantsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountBillingCreditsGrantsResponse> CountBillingCreditsGrantsAsync(
        CountBillingCreditsGrantsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListGrantsForCreditResponse> ListGrantsForCreditAsync(
        ListGrantsForCreditRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetEnrichedCreditLedgerResponse> GetEnrichedCreditLedgerAsync(
        GetEnrichedCreditLedgerRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountCreditLedgerResponse> CountCreditLedgerAsync(
        CountCreditLedgerRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListBillingPlanCreditGrantsResponse> ListBillingPlanCreditGrantsAsync(
        ListBillingPlanCreditGrantsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateBillingPlanCreditGrantResponse> CreateBillingPlanCreditGrantAsync(
        CreateBillingPlanCreditGrantRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetSingleBillingPlanCreditGrantResponse> GetSingleBillingPlanCreditGrantAsync(
        string planGrantId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateBillingPlanCreditGrantResponse> UpdateBillingPlanCreditGrantAsync(
        string planGrantId,
        UpdateBillingPlanCreditGrantRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteBillingPlanCreditGrantResponse> DeleteBillingPlanCreditGrantAsync(
        string planGrantId,
        DeleteBillingPlanCreditGrantRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountBillingPlanCreditGrantsResponse> CountBillingPlanCreditGrantsAsync(
        CountBillingPlanCreditGrantsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListCreditEventLedgerResponse> ListCreditEventLedgerAsync(
        ListCreditEventLedgerRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountCreditEventLedgerResponse> CountCreditEventLedgerAsync(
        CountCreditEventLedgerRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
