namespace SchematicHQ.Client;

public partial interface IAccesstokensClient
{
    WithRawResponseTask<IssueTemporaryAccessTokenResponse> IssueTemporaryAccessTokenAsync(
        IssueTemporaryAccessTokenRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
