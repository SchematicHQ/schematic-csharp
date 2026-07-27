namespace SchematicHQ.Client;

public partial interface ILicensesClient
{
    WithRawResponseTask<ListLicensesResponse> ListLicensesAsync(
        ListLicensesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetSingleLicenseResponse> GetSingleLicenseAsync(
        string licenseId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountLicensesResponse> CountLicensesAsync(
        CountLicensesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
