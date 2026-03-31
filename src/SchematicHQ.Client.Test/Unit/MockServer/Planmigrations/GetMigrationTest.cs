using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Planmigrations;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetMigrationTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "completed_at": "2024-01-15T09:30:00.000Z",
                "completed_companies": 1000000,
                "created_at": "2024-01-15T09:30:00.000Z",
                "error": "error",
                "failed_companies": 1000000,
                "id": "id",
                "plan_id": "plan_id",
                "plan_version_id_from": "plan_version_id_from",
                "plan_version_id_to": "plan_version_id_to",
                "skipped_companies": 1000000,
                "started_at": "2024-01-15T09:30:00.000Z",
                "status": "completed",
                "strategy": "immediate",
                "total_companies": 1000000,
                "updated_at": "2024-01-15T09:30:00.000Z"
              },
              "params": {
                "key": "value"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plan-version-migrations/plan_version_migration_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Planmigrations.GetMigrationAsync("plan_version_migration_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
