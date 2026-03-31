using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Webhooks;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetWebhookTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "created_at": "2024-01-15T09:30:00.000Z",
                "credit_trigger_configs": [
                  {
                    "credit_id": "credit_id"
                  }
                ],
                "entitlement_trigger_configs": [
                  {
                    "feature_id": "feature_id"
                  }
                ],
                "id": "id",
                "name": "name",
                "request_types": [
                  "subscription.trial.ended"
                ],
                "secret": "secret",
                "status": "active",
                "updated_at": "2024-01-15T09:30:00.000Z",
                "url": "url"
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
                    .WithPath("/webhooks/webhook_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Webhooks.GetWebhookAsync("webhook_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
