using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Entitlements;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListPlanEntitlementsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "billing_linked_resource": {
                    "billing_provider": "orb",
                    "external_resource_id": "external_resource_id",
                    "originator": "orb"
                  },
                  "billing_threshold": 1000000,
                  "consumption_rate": 1.1,
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency_prices": [
                    {
                      "currency": "currency"
                    }
                  ],
                  "environment_id": "environment_id",
                  "feature": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "feature_type": "boolean",
                    "icon": "icon",
                    "id": "id",
                    "name": "name",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "feature_id": "feature_id",
                  "id": "id",
                  "metered_monthly_price": {
                    "billing_scheme": "per_unit",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency": "currency",
                    "id": "id",
                    "interval": "day",
                    "is_active": true,
                    "package_size": 1000000,
                    "price": 1000000,
                    "price_external_id": "price_external_id",
                    "price_id": "price_id",
                    "price_tier": [
                      {}
                    ],
                    "product_external_id": "product_external_id",
                    "product_id": "product_id",
                    "product_name": "product_name",
                    "provider_type": "orb",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "usage_type": "licensed"
                  },
                  "metered_yearly_price": {
                    "billing_scheme": "per_unit",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency": "currency",
                    "id": "id",
                    "interval": "day",
                    "is_active": true,
                    "package_size": 1000000,
                    "price": 1000000,
                    "price_external_id": "price_external_id",
                    "price_id": "price_id",
                    "price_tier": [
                      {}
                    ],
                    "product_external_id": "product_external_id",
                    "product_id": "product_id",
                    "product_name": "product_name",
                    "provider_type": "orb",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "usage_type": "licensed"
                  },
                  "metric_period": "metric_period",
                  "metric_period_month_reset": "metric_period_month_reset",
                  "plan": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "icon": "amber",
                    "id": "id",
                    "name": "name",
                    "plan_type": "plan",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "plan_id": "plan_id",
                  "price_behavior": "credit_burndown",
                  "rule_id": "rule_id",
                  "rule_id_usage_exceeded": "rule_id_usage_exceeded",
                  "soft_limit": 1000000,
                  "updated_at": "2024-01-15T09:30:00.000Z",
                  "usage_based_product": {
                    "account_id": "account_id",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "is_active": true,
                    "name": "name",
                    "price": 1.1,
                    "product_id": "product_id",
                    "provider_type": "orb",
                    "quantity": 1.1,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "value_bool": true,
                  "value_credit": {
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
                  "value_numeric": 1000000,
                  "value_trait": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "display_name": "display_name",
                    "entity_type": "company",
                    "hierarchy": [
                      "hierarchy"
                    ],
                    "id": "id",
                    "trait_type": "boolean",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "value_trait_id": "value_trait_id",
                  "value_type": "boolean"
                }
              ],
              "params": {
                "feature_id": "feature_id",
                "feature_ids": [
                  "feature_ids"
                ],
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "plan_id": "plan_id",
                "plan_ids": [
                  "plan_ids"
                ],
                "plan_version_id": "plan_version_id",
                "plan_version_ids": [
                  "plan_version_ids"
                ],
                "q": "q",
                "with_metered_products": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plan-entitlements")
                    .WithParam("feature_id", "feature_id")
                    .WithParam("plan_id", "plan_id")
                    .WithParam("plan_version_id", "plan_version_id")
                    .WithParam("q", "q")
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

        var response = await Client.Entitlements.ListPlanEntitlementsAsync(
            new ListPlanEntitlementsRequest
            {
                FeatureId = "feature_id",
                PlanId = "plan_id",
                PlanVersionId = "plan_version_id",
                Q = "q",
                WithMeteredProducts = true,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
