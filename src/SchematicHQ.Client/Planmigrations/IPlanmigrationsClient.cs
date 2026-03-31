namespace SchematicHQ.Client;

public partial interface IPlanmigrationsClient
{
    WithRawResponseTask<ListCompanyMigrationsResponse> ListCompanyMigrationsAsync(
        ListCompanyMigrationsRequest request,
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

    WithRawResponseTask<GetMigrationResponse> GetMigrationAsync(
        string planVersionMigrationId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountMigrationsResponse> CountMigrationsAsync(
        CountMigrationsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
