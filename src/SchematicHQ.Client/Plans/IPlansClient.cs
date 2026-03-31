namespace SchematicHQ.Client;

public partial interface IPlansClient
{
    WithRawResponseTask<UpdateCompanyPlansResponse> UpdateCompanyPlansAsync(
        string companyPlanId,
        UpdateCompanyPlansRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListPlansResponse> ListPlansAsync(
        ListPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreatePlanResponse> CreatePlanAsync(
        CreatePlanRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetPlanResponse> GetPlanAsync(
        string planId,
        GetPlanRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdatePlanResponse> UpdatePlanAsync(
        string planId,
        UpdatePlanRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeletePlanResponse> DeletePlanAsync(
        string planId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertBillingProductPlanResponse> UpsertBillingProductPlanAsync(
        string planId,
        UpsertBillingProductRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountPlansResponse> CountPlansAsync(
        CountPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListPlanIssuesResponse> ListPlanIssuesAsync(
        ListPlanIssuesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeletePlanVersionResponse> DeletePlanVersionAsync(
        string planId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<PublishPlanVersionResponse> PublishPlanVersionAsync(
        string planId,
        PublishPlanVersionRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
