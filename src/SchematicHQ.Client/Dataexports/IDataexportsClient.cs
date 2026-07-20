namespace SchematicHQ.Client;

public partial interface IDataexportsClient
{
    WithRawResponseTask<ListDataExportsResponse> ListDataExportsAsync(
        ListDataExportsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateDataExportResponse> CreateDataExportAsync(
        CreateDataExportRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetDataExportResponse> GetDataExportAsync(
        string dataExportId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<global::System.IO.Stream> GetDataExportArtifactAsync(
        string dataExportId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
