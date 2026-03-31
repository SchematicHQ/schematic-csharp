using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Events;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreateEventTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "event_type": "flag_check"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "captured_at": "2024-01-15T09:30:00.000Z",
                "event_id": "event_id",
                "remote_addr": "remote_addr",
                "remote_ip": "remote_ip",
                "user_agent": "user_agent"
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
                    .WithPath("/events")
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

        var response = await Client.Events.CreateEventAsync(
            new CreateEventRequestBody { EventType = EventType.FlagCheck }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
