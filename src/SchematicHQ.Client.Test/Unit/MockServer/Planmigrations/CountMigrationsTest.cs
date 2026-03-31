using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Planmigrations;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountMigrationsTest : BaseMockServerTest
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
                    .WithPath("/plan-version-migrations/count")
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

        var response = await Client.Planmigrations.CountMigrationsAsync(
            new CountMigrationsRequest
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
