using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertBillingMeterTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "display_name": "display_name",
              "event_name": "event_name",
              "event_payload_key": "event_payload_key",
              "external_id": "external_id"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "dispaly_name": "dispaly_name",
                "event_name": "event_name",
                "event_payload_key": "event_payload_key",
                "external_price_id": "external_price_id",
                "id": "id",
                "provider_type": "orb"
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
                    .WithPath("/billing/meter/upsert")
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

        var response = await Client.Billing.UpsertBillingMeterAsync(
            new CreateMeterRequestBody
            {
                DisplayName = "display_name",
                EventName = "event_name",
                EventPayloadKey = "event_payload_key",
                ExternalId = "external_id",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
