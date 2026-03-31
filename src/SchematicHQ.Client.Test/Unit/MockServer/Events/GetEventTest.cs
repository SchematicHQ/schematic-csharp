using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Events;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetEventTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "api_key": "api_key",
                "body": {
                  "key": "value"
                },
                "body_preview": "body_preview",
                "captured_at": "2024-01-15T09:30:00.000Z",
                "company": {
                  "description": "description",
                  "id": "id",
                  "image_url": "image_url",
                  "name": "name"
                },
                "company_id": "company_id",
                "environment_id": "environment_id",
                "error_message": "error_message",
                "feature_ids": [
                  "feature_ids"
                ],
                "features": [
                  {
                    "id": "id",
                    "name": "name"
                  }
                ],
                "id": "id",
                "loaded_at": "2024-01-15T09:30:00.000Z",
                "quantity": 1000000,
                "sent_at": "2024-01-15T09:30:00.000Z",
                "status": "enriched",
                "subtype": "subtype",
                "type": "flag_check",
                "user": {
                  "description": "description",
                  "id": "id",
                  "image_url": "image_url",
                  "name": "name"
                },
                "user_id": "user_id"
              },
              "params": {
                "key": "value"
              }
            }
            """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/events/event_id").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Events.GetEventAsync("event_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
