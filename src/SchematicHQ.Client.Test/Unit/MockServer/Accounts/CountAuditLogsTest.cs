using global::System.Globalization;
using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountAuditLogsTest : BaseMockServerTest
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
                "actor_type": "api_key",
                "end_time": "2024-01-15T09:30:00.000Z",
                "environment_id": "environment_id",
                "limit": 1000000,
                "offset": 1000000,
                "q": "q",
                "start_time": "2024-01-15T09:30:00.000Z"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/audit-log/count")
                    .WithParam("actor_type", "api_key")
                    .WithParam("end_time", "2024-01-15T09:30:00.000Z")
                    .WithParam("environment_id", "environment_id")
                    .WithParam("q", "q")
                    .WithParam("start_time", "2024-01-15T09:30:00.000Z")
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

        var response = await Client.Accounts.CountAuditLogsAsync(
            new CountAuditLogsRequest
            {
                ActorType = ActorType.ApiKey,
                EndTime = DateTime.Parse(
                    "2024-01-15T09:30:00.000Z",
                    null,
                    DateTimeStyles.AdjustToUniversal
                ),
                EnvironmentId = "environment_id",
                Q = "q",
                StartTime = DateTime.Parse(
                    "2024-01-15T09:30:00.000Z",
                    null,
                    DateTimeStyles.AdjustToUniversal
                ),
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
