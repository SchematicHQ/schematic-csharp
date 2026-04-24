using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListBillingPlanCreditGrantsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "auto_topup_amount": 1000000,
                  "auto_topup_amount_type": "auto_topup_amount_type",
                  "auto_topup_enabled": true,
                  "auto_topup_expiry_type": "duration",
                  "auto_topup_expiry_unit": "billing_periods",
                  "auto_topup_expiry_unit_count": 1000000,
                  "auto_topup_threshold_credits": 1000000,
                  "auto_topup_threshold_percent": 1000000,
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "credit": {
                    "burn_strategy": "expiration_priority",
                    "cost_editable": true,
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
                    "default_expiry_unit": "billing_periods",
                    "default_rollover_policy": "expire",
                    "description": "description",
                    "id": "id",
                    "name": "name",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "credit_amount": 1000000,
                  "credit_id": "credit_id",
                  "credit_name": "credit_name",
                  "expiry_type": "duration",
                  "expiry_unit": "billing_periods",
                  "expiry_unit_count": 1000000,
                  "id": "id",
                  "plan": {
                    "id": "id",
                    "name": "name"
                  },
                  "plan_id": "plan_id",
                  "plan_name": "plan_name",
                  "plan_version_id": "plan_version_id",
                  "reset_cadence": "daily",
                  "reset_start": "billing_period",
                  "reset_type": "no_reset",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "credit_id": "credit_id",
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "plan_id": "plan_id",
                "plan_ids": [
                  "plan_ids"
                ],
                "plan_version_id": "plan_version_id"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/credits/plan-grants")
                    .WithParam("credit_id", "credit_id")
                    .WithParam("plan_id", "plan_id")
                    .WithParam("plan_version_id", "plan_version_id")
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

        var response = await Client.Credits.ListBillingPlanCreditGrantsAsync(
            new ListBillingPlanCreditGrantsRequest
            {
                CreditId = "credit_id",
                Ids = new List<string>() { "ids" },
                PlanId = "plan_id",
                PlanIds = new List<string>() { "plan_ids" },
                PlanVersionId = "plan_version_id",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
