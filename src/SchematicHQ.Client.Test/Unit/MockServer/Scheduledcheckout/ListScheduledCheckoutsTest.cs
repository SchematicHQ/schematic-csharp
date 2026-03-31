using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Scheduledcheckout;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListScheduledCheckoutsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "actor_type": "actor_type",
                  "company_id": "company_id",
                  "completed_at": "2024-01-15T09:30:00.000Z",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "error_message": "error_message",
                  "execute_after": "2024-01-15T09:30:00.000Z",
                  "from_plan_id": "from_plan_id",
                  "id": "id",
                  "scheduled_interval": "scheduled_interval",
                  "scheduled_price": 1000000,
                  "started_at": "2024-01-15T09:30:00.000Z",
                  "status": "cancelled",
                  "to_plan_id": "to_plan_id",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "company_id": "company_id",
                "limit": 1000000,
                "offset": 1000000,
                "status": "cancelled"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/scheduled-checkout")
                    .WithParam("company_id", "company_id")
                    .WithParam("status", "cancelled")
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

        var response = await Client.Scheduledcheckout.ListScheduledCheckoutsAsync(
            new ListScheduledCheckoutsRequest
            {
                CompanyId = "company_id",
                Status = ScheduledCheckoutStatus.Cancelled,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
