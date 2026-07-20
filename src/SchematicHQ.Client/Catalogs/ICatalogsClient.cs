namespace SchematicHQ.Client;

public partial interface ICatalogsClient
{
    WithRawResponseTask<ListCatalogsResponse> ListCatalogsAsync(
        ListCatalogsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateCatalogResponse> CreateCatalogAsync(
        CreateCatalogRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetCatalogResponse> GetCatalogAsync(
        string catalogId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateCatalogResponse> UpdateCatalogAsync(
        string catalogId,
        UpdateCatalogRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteCatalogResponse> DeleteCatalogAsync(
        string catalogId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetConfigurationResponse> GetConfigurationAsync(
        string catalogId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateConfigurationResponse> UpdateConfigurationAsync(
        string catalogId,
        UpdateCatalogConfigurationRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetCreditBundlesInCatalogResponse> GetCreditBundlesInCatalogAsync(
        string catalogId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask AddCreditBundleAsync(
        string catalogId,
        string creditBundleId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask RemoveCreditBundleAsync(
        string catalogId,
        string creditBundleId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetDerivedFeaturesResponse> GetDerivedFeaturesAsync(
        string catalogId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetPlansInCatalogResponse> GetPlansInCatalogAsync(
        string catalogId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask AddPlanAsync(
        string catalogId,
        string planId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask RemovePlanAsync(
        string catalogId,
        string planId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
