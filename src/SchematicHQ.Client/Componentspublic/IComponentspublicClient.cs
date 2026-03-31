namespace SchematicHQ.Client;

public partial interface IComponentspublicClient
{
    WithRawResponseTask<GetPublicPlansResponse> GetPublicPlansAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
