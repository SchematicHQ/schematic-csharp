using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Events;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListEventsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "api_key": "api_key",
                  "body": {
                    "key": "value"
                  },
                  "body_preview": "body_preview",
                  "captured_at": "2024-01-15T09:30:00.000Z",
                  "company": {
                    "id": "id",
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
                    "id": "id",
                    "name": "name"
                  },
                  "user_id": "user_id"
                }
              ],
              "params": {
                "company_id": "company_id",
                "event_subtype": "event_subtype",
                "event_types": [
                  "flag_check"
                ],
                "flag_id": "flag_id",
                "limit": 1000000,
                "offset": 1000000,
                "user_id": "user_id"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/events")
                    .WithParam("company_id", "company_id")
                    .WithParam("event_subtype", "event_subtype")
                    .WithParam("flag_id", "flag_id")
                    .WithParam("user_id", "user_id")
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

        var response = await Client.Events.ListEventsAsync(
            new ListEventsRequest
            {
                CompanyId = "company_id",
                EventSubtype = "event_subtype",
                EventTypes = [new List<EventType>() { EventType.FlagCheck }],
                FlagId = "flag_id",
                UserId = "user_id",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
