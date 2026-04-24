using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Webhooks;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListWebhookEventsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
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
                }
              ],
              "params": {
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "q": "q",
                "webhook_id": "webhook_id"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/webhook-events")
                    .WithParam("q", "q")
                    .WithParam("webhook_id", "webhook_id")
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

        var response = await Client.Webhooks.ListWebhookEventsAsync(
            new ListWebhookEventsRequest
            {
                Ids = new List<string>() { "ids" },
                Q = "q",
                WebhookId = "webhook_id",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
