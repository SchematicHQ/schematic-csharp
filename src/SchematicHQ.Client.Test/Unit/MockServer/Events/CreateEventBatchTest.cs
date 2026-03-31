using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Events;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreateEventBatchTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "events": [
                {
                  "event_type": "flag_check"
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "events": [
                  {
                    "captured_at": "2024-01-15T09:30:00.000Z",
                    "remote_ip": "remote_ip",
                    "user_agent": "user_agent"
                  }
                ]
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
                    .WithPath("/event-batch")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Events.CreateEventBatchAsync(
            new CreateEventBatchRequestBody
            {
                Events = new List<CreateEventRequestBody>()
                {
                    new CreateEventRequestBody { EventType = EventType.FlagCheck },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
