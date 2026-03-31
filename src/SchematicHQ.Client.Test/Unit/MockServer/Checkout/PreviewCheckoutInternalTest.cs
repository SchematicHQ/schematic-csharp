using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Checkout;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class PreviewCheckoutInternalTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "add_on_ids": [
                {
                  "add_on_id": "add_on_id",
                  "price_id": "price_id"
                }
              ],
              "company_id": "company_id",
              "credit_bundles": [
                {
                  "bundle_id": "bundle_id",
                  "quantity": 1000000
                }
              ],
              "new_plan_id": "new_plan_id",
              "new_price_id": "new_price_id",
              "pay_in_advance": [
                {
                  "price_id": "price_id",
                  "quantity": 1000000
                }
              ],
              "skip_trial": true
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "amount_off": 1000000,
                "due_now": 1000000,
                "finance": {
                  "amount_off": 1000000,
                  "due_now": 1000000,
                  "new_charges": 1000000,
                  "percent_off": 1.1,
                  "period_start": "2024-01-15T09:30:00.000Z",
                  "promo_code_applied": true,
                  "proration": 1000000,
                  "tax_amount": 1000000,
                  "tax_display_name": "tax_display_name",
                  "tax_require_billing_details": true,
                  "total_per_billing_period": 1000000,
                  "trial_end": "2024-01-15T09:30:00.000Z",
                  "upcoming_invoice_line_items": [
                    {
                      "amount": 1000000,
                      "description": "description",
                      "price_id": "price_id",
                      "proration": true,
                      "quantity": 1000000
                    }
                  ]
                },
                "is_scheduled_downgrade": true,
                "new_charges": 1000000,
                "payment_method_required": true,
                "percent_off": 1.1,
                "period_start": "2024-01-15T09:30:00.000Z",
                "promo_code_applied": true,
                "proration": 1000000,
                "scheduled_change_time": "2024-01-15T09:30:00.000Z",
                "usage_violations": [
                  {
                    "access": true,
                    "allocation_type": "boolean",
                    "entitlement_id": "entitlement_id",
                    "entitlement_type": "company_override"
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
                    .WithPath("/checkout-internal/preview")
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

        var response = await Client.Checkout.PreviewCheckoutInternalAsync(
            new ChangeSubscriptionInternalRequestBody
            {
                AddOnIds = new List<UpdateAddOnRequestBody>()
                {
                    new UpdateAddOnRequestBody { AddOnId = "add_on_id", PriceId = "price_id" },
                },
                CompanyId = "company_id",
                CreditBundles = new List<UpdateCreditBundleRequestBody>()
                {
                    new UpdateCreditBundleRequestBody
                    {
                        BundleId = "bundle_id",
                        Quantity = 1000000,
                    },
                },
                NewPlanId = "new_plan_id",
                NewPriceId = "new_price_id",
                PayInAdvance = new List<UpdatePayInAdvanceRequestBody>()
                {
                    new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
                },
                SkipTrial = true,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
