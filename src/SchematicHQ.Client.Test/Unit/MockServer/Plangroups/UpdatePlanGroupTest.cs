using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plangroups;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdatePlanGroupTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "add_on_ids": [
                "add_on_ids"
              ],
              "checkout_collect_address": true,
              "checkout_collect_email": true,
              "checkout_collect_phone": true,
              "enable_tax_collection": true,
              "ordered_add_ons": [
                {
                  "plan_id": "plan_id"
                }
              ],
              "ordered_bundle_list": [
                {
                  "bundleId": "bundleId"
                }
              ],
              "ordered_plans": [
                {
                  "plan_id": "plan_id"
                }
              ],
              "prevent_downgrades_when_over_limit": true,
              "prevent_self_service_downgrade": true,
              "proration_behavior": "create_prorations",
              "show_as_monthly_prices": true,
              "show_credits": true,
              "show_feature_description": true,
              "show_hard_limit": true,
              "show_period_toggle": true,
              "show_zero_price_as_free": true,
              "sync_customer_billing_details": true
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "add_on_compatibilities": [
                  {
                    "compatible_plan_ids": [
                      "compatible_plan_ids"
                    ],
                    "source_plan_id": "source_plan_id"
                  }
                ],
                "add_on_ids": [
                  "add_on_ids"
                ],
                "checkout_settings": {
                  "collect_address": true,
                  "collect_email": true,
                  "collect_phone": true
                },
                "component_settings": {
                  "show_as_monthly_prices": true,
                  "show_credits": true,
                  "show_feature_description": true,
                  "show_hard_limit": true,
                  "show_period_toggle": true,
                  "show_zero_price_as_free": true
                },
                "fallback_plan_id": "fallback_plan_id",
                "id": "id",
                "initial_plan_id": "initial_plan_id",
                "initial_plan_price_id": "initial_plan_price_id",
                "ordered_add_on_ids": [
                  {
                    "plan_id": "plan_id"
                  }
                ],
                "plan_ids": [
                  {
                    "plan_id": "plan_id"
                  }
                ],
                "prevent_downgrades_when_over_limit": true,
                "prevent_self_service_downgrade": true,
                "prevent_self_service_downgrade_button_text": "prevent_self_service_downgrade_button_text",
                "prevent_self_service_downgrade_url": "prevent_self_service_downgrade_url",
                "proration_behavior": "proration_behavior",
                "scheduled_downgrade_behavior": "scheduled_downgrade_behavior",
                "scheduled_downgrade_prevent_when_over_limit": true,
                "show_as_monthly_prices": true,
                "show_credits": true,
                "show_period_toggle": true,
                "show_zero_price_as_free": true,
                "sync_customer_billing_details": true,
                "tax_collection_enabled": true,
                "trial_days": 1000000,
                "trial_expiry_plan_id": "trial_expiry_plan_id",
                "trial_expiry_plan_price_id": "trial_expiry_plan_price_id",
                "trial_payment_method_required": true
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
                    .WithPath("/plan-groups/plan_group_id")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Plangroups.UpdatePlanGroupAsync(
            "plan_group_id",
            new UpdatePlanGroupRequestBody
            {
                AddOnIds = new List<string>() { "add_on_ids" },
                CheckoutCollectAddress = true,
                CheckoutCollectEmail = true,
                CheckoutCollectPhone = true,
                EnableTaxCollection = true,
                OrderedAddOns = new List<OrderedPlansInGroup>()
                {
                    new OrderedPlansInGroup { PlanId = "plan_id" },
                },
                OrderedBundleList = new List<PlanGroupBundleOrder>()
                {
                    new PlanGroupBundleOrder { BundleId = "bundleId" },
                },
                OrderedPlans = new List<OrderedPlansInGroup>()
                {
                    new OrderedPlansInGroup { PlanId = "plan_id" },
                },
                PreventDowngradesWhenOverLimit = true,
                PreventSelfServiceDowngrade = true,
                ProrationBehavior = ProrationBehavior.CreateProrations,
                ShowAsMonthlyPrices = true,
                ShowCredits = true,
                ShowFeatureDescription = true,
                ShowHardLimit = true,
                ShowPeriodToggle = true,
                ShowZeroPriceAsFree = true,
                SyncCustomerBillingDetails = true,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
