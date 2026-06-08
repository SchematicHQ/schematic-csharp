namespace SchematicHQ.Client;

public partial interface IPlanmigrationsClient
{
    WithRawResponseTask<ListCompanyMigrationsResponse> ListCompanyMigrationsAsync(
        ListCompanyMigrationsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<RetryCompanyMigrationResponse> RetryCompanyMigrationAsync(
        string planVersionCompanyMigrationId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountCompanyMigrationsResponse> CountCompanyMigrationsAsync(
        CountCompanyMigrationsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListMigrationsResponse> ListMigrationsAsync(
        ListMigrationsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateMigrationResponse> CreateMigrationAsync(
        CreateMigrationInput request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetMigrationResponse> GetMigrationAsync(
        string planVersionMigrationId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<RetryMigrationResponse> RetryMigrationAsync(
        string planVersionMigrationId,
        RetryMigrationRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountMigrationsResponse> CountMigrationsAsync(
        CountMigrationsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
