using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Scheduledcheckout;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateScheduledCheckoutTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {}
            """;

        const string mockResponse = """
            {
              "data": {
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
                    .WithPath("/scheduled-checkout/scheduled_checkout_id")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Scheduledcheckout.UpdateScheduledCheckoutAsync(
            "scheduled_checkout_id",
            new UpdateScheduledCheckoutRequest()
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
