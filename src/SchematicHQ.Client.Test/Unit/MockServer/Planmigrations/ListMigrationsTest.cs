using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Planmigrations;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListMigrationsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
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
                }
              ],
              "params": {
                "limit": 1000000,
                "offset": 1000000,
                "plan_version_id": "plan_version_id",
                "status": "completed"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plan-version-migrations")
                    .WithParam("plan_version_id", "plan_version_id")
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

        var response = await Client.Planmigrations.ListMigrationsAsync(
            new ListMigrationsRequest
            {
                PlanVersionId = "plan_version_id",
                Status = PlanVersionMigrationStatus.Completed,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
