namespace SchematicHQ.Client;

public partial interface IAccountsClient
{
    WithRawResponseTask<ListAccountMembersResponse> ListAccountMembersAsync(
        ListAccountMembersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetAccountMemberResponse> GetAccountMemberAsync(
        string accountMemberId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListApiKeysResponse> ListApiKeysAsync(
        ListApiKeysRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateApiKeyResponse> CreateApiKeyAsync(
        CreateApiKeyRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetApiKeyResponse> GetApiKeyAsync(
        string apiKeyId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateApiKeyResponse> UpdateApiKeyAsync(
        string apiKeyId,
        UpdateApiKeyRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteApiKeyResponse> DeleteApiKeyAsync(
        string apiKeyId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountApiKeysResponse> CountApiKeysAsync(
        CountApiKeysRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListAuditLogsResponse> ListAuditLogsAsync(
        ListAuditLogsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetAuditLogResponse> GetAuditLogAsync(
        string auditLogId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountAuditLogsResponse> CountAuditLogsAsync(
        CountAuditLogsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListEnvironmentsResponse> ListEnvironmentsAsync(
        ListEnvironmentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateEnvironmentResponse> CreateEnvironmentAsync(
        CreateEnvironmentRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetEnvironmentResponse> GetEnvironmentAsync(
        string environmentId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateEnvironmentResponse> UpdateEnvironmentAsync(
        string environmentId,
        UpdateEnvironmentRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteEnvironmentResponse> DeleteEnvironmentAsync(
        string environmentId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<QuickstartResponse> QuickstartAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetWhoAmIResponse> GetWhoAmIAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
