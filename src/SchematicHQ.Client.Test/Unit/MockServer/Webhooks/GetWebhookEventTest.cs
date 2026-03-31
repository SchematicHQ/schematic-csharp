using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Webhooks;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetWebhookEventTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "created_at": "2024-01-15T09:30:00.000Z",
                "id": "id",
                "payload": "payload",
                "request_type": "subscription.trial.ended",
                "response_code": 1000000,
                "sent_at": "2024-01-15T09:30:00.000Z",
                "status": "failure",
                "updated_at": "2024-01-15T09:30:00.000Z",
                "webhook": {
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
                "webhook_id": "webhook_id"
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
                    .WithPath("/webhook-events/webhook_event_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Webhooks.GetWebhookEventAsync("webhook_event_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
