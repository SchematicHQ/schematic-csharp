using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Planmigrations;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountCompanyMigrationsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "count": 1
              },
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
                    .WithPath("/plan-version-company-migrations/count")
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

        var response = await Client.Planmigrations.CountCompanyMigrationsAsync(
            new CountCompanyMigrationsRequest
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
