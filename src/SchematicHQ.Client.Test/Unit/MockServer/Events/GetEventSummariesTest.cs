using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Events;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetEventSummariesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "company_count": 1000000,
                  "environment_id": "environment_id",
                  "event_count": 1000000,
                  "event_subtype": "event_subtype",
                  "last_seen_at": "2024-01-15T09:30:00.000Z",
                  "user_count": 1000000
                }
              ],
              "params": {
                "event_subtypes": [
                  "event_subtypes"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "q": "q"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/event-types")
                    .WithParam("q", "q")
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

        var response = await Client.Events.GetEventSummariesAsync(
            new GetEventSummariesRequest
            {
                Q = "q",
                EventSubtypes = new List<string>() { "event_subtypes" },
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
