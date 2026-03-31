using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Planmigrations;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListCompanyMigrationsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "company_id": "company_id",
                  "company_name": "company_name",
                  "completed_at": "2024-01-15T09:30:00.000Z",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "error": "error",
                  "id": "id",
                  "migration_id": "migration_id",
                  "plan_version_id_from": "plan_version_id_from",
                  "started_at": "2024-01-15T09:30:00.000Z",
                  "status": "completed",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "limit": 1000000,
                "migration_id": "migration_id",
                "offset": 1000000,
                "q": "q",
                "status": "completed"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plan-version-company-migrations")
                    .WithParam("migration_id", "migration_id")
                    .WithParam("q", "q")
                    .WithParam("status", "completed")
                    .WithParam("limit", "1000000")
                    .WithParam("offset", "1000000")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Planmigrations.ListCompanyMigrationsAsync(
            new ListCompanyMigrationsRequest
            {
                MigrationId = "migration_id",
                Q = "q",
                Status = PlanVersionCompanyMigrationStatus.Completed,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
