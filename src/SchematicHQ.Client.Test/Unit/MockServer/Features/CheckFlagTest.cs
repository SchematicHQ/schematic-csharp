using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Features;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CheckFlagTest : BaseMockServerTest
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
                "company_id": "company_id",
                "entitlement": {
                  "allocation": 1000000,
                  "credit_id": "credit_id",
                  "credit_remaining": 1.1,
                  "credit_total": 1.1,
                  "credit_used": 1.1,
                  "event_name": "event_name",
                  "feature_id": "feature_id",
                  "feature_key": "feature_key",
                  "metric_period": "all_time",
                  "metric_reset_at": "2024-01-15T09:30:00.000Z",
                  "month_reset": "first_of_month",
                  "soft_limit": 1000000,
                  "usage": 1000000,
                  "value_type": "boolean"
                },
                "error": "error",
                "flag": "flag",
                "flag_id": "flag_id",
                "reason": "reason",
                "rule_id": "rule_id",
                "rule_type": "rule_type",
                "user_id": "user_id",
                "value": true
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
                    .WithPath("/flags/key/check")
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

        var response = await Client.Features.CheckFlagAsync("key", new CheckFlagRequestBody());
        JsonAssert.AreEqual(response, mockResponse);
    }
}
