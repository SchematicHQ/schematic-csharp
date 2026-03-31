using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreateBillingPlanCreditGrantTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "credit_amount": 1000000,
              "credit_id": "credit_id",
              "plan_id": "plan_id",
              "reset_cadence": "daily",
              "reset_start": "billing_period"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "auto_topup_amount": 1000000,
                "auto_topup_amount_type": "auto_topup_amount_type",
                "auto_topup_enabled": true,
                "auto_topup_expiry_type": "duration",
                "auto_topup_expiry_unit": "billing_periods",
                "auto_topup_expiry_unit_count": 1000000,
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
                  "default_expiry_unit_count": 1000000,
                  "default_rollover_policy": "expire",
                  "description": "description",
                  "icon": "icon",
                  "id": "id",
                  "name": "name",
                  "plural_name": "plural_name",
                  "price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "schematic",
                    "scheme": "per_unit"
                  },
                  "product": {
                    "account_id": "account_id",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "is_active": true,
                    "name": "name",
                    "price": 1.1,
                    "product_id": "product_id",
                    "provider_type": "schematic",
                    "quantity": 1.1,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "singular_name": "singular_name",
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
                  "description": "description",
                  "id": "id",
                  "image_url": "image_url",
                  "name": "name"
                },
                "plan_id": "plan_id",
                "plan_name": "plan_name",
                "plan_version_id": "plan_version_id",
                "reset_cadence": "daily",
                "reset_start": "billing_period",
                "reset_type": "no_reset",
                "updated_at": "2024-01-15T09:30:00.000Z"
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
                    .WithPath("/billing/credits/plan-grants")
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

        var response = await Client.Credits.CreateBillingPlanCreditGrantAsync(
            new CreateBillingPlanCreditGrantRequestBody
            {
                CreditAmount = 1000000,
                CreditId = "credit_id",
                PlanId = "plan_id",
                ResetCadence = BillingPlanCreditGrantResetCadence.Daily,
                ResetStart = BillingPlanCreditGrantResetStart.BillingPeriod,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
