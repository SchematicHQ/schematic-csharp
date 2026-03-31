using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Checkout;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class PreviewManagePlanTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "add_on_selections": [
                {
                  "plan_id": "plan_id"
                }
              ],
              "company_id": "company_id",
              "credit_bundles": [
                {
                  "bundle_id": "bundle_id",
                  "quantity": 1000000
                }
              ],
              "pay_in_advance_entitlements": [
                {
                  "price_id": "price_id",
                  "quantity": 1000000
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "subscription_change_preview": {
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
                    "tax_require_billing_details": true,
                    "total_per_billing_period": 1000000,
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
                }
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
                    .WithPath("/manage-plan/preview")
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

        var response = await Client.Checkout.PreviewManagePlanAsync(
            new ManagePlanRequest
            {
                AddOnSelections = new List<PlanSelection>()
                {
                    new PlanSelection { PlanId = "plan_id" },
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
                PayInAdvanceEntitlements = new List<UpdatePayInAdvanceRequestBody>()
                {
                    new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
