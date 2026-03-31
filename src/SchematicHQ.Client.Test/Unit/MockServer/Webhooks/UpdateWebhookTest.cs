using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Webhooks;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateWebhookTest : BaseMockServerTest
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

        var response = await Client.Webhooks.UpdateWebhookAsync(
            "webhook_id",
            new UpdateWebhookRequestBody()
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
