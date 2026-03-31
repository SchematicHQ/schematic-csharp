using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Entitlements;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class DuplicatePlanEntitlementsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "source_plan_id": "source_plan_id",
              "target_plan_id": "target_plan_id"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "data": [
                  {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
                    "environment_id": "environment_id",
                    "feature_id": "feature_id",
                    "id": "id",
                    "plan_id": "plan_id",
                    "rule_id": "rule_id",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "value_type": "boolean"
                  }
                ],
                "skipped": [
                  {
                    "entitlement_id": "entitlement_id",
                    "errors": [
                      {
                        "message": "message"
                      }
                    ],
                    "feature_id": "feature_id",
                    "feature_name": "feature_name"
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
                    .WithPath("/plan-entitlements/duplicate")
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

        var response = await Client.Entitlements.DuplicatePlanEntitlementsAsync(
            new DuplicatePlanEntitlementsRequestBody
            {
                SourcePlanId = "source_plan_id",
                TargetPlanId = "target_plan_id",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
