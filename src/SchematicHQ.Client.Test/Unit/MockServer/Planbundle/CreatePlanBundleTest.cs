using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Planbundle;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreatePlanBundleTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "entitlements": [
                {
                  "action": "create"
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "billing_product": {
                  "account_id": "account_id",
                  "billing_product_id": "billing_product_id",
                  "charge_type": "free",
                  "controlled_by": "orb",
                  "environment_id": "environment_id",
                  "is_trialable": true,
                  "monthly_price_id": "monthly_price_id",
                  "one_time_price_id": "one_time_price_id",
                  "plan_id": "plan_id",
                  "trial_days": 1000000,
                  "yearly_price_id": "yearly_price_id"
                },
                "credit_grants": [
                  {
                    "auto_topup_enabled": true,
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "credit_amount": 1000000,
                    "credit_id": "credit_id",
                    "credit_name": "credit_name",
                    "id": "id",
                    "plan_id": "plan_id",
                    "plan_name": "plan_name",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  }
                ],
                "entitlements": [
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
                "plan": {
                  "company_id": "company_id",
                  "copied_from_plan_id": "copied_from_plan_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "description": "description",
                  "icon": "amber",
                  "id": "id",
                  "name": "name",
                  "plan_type": "plan",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
                "traits": [
                  {
                    "account_id": "account_id",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_id": "environment_id",
                    "id": "id",
                    "plan_id": "plan_id",
                    "plan_type": "plan_type",
                    "trait_id": "trait_id",
                    "trait_value": "trait_value",
                    "updated_at": "2024-01-15T09:30:00.000Z"
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
                    .WithPath("/plan-bundles")
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

        var response = await Client.Planbundle.CreatePlanBundleAsync(
            new CreatePlanBundleRequestBody
            {
                Entitlements = new List<PlanBundleEntitlementRequestBody>()
                {
                    new PlanBundleEntitlementRequestBody { Action = PlanBundleAction.Create },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
