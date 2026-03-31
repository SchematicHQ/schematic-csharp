using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Entitlements;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListFeatureUsageTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "access": true,
                  "allocation": 1000000,
                  "allocation_type": "boolean",
                  "company_override": {
                    "company_id": "company_id",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_id": "environment_id",
                    "feature_id": "feature_id",
                    "id": "id",
                    "notes": [
                      {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "external_user_id": "external_user_id",
                        "external_user_name": "external_user_name",
                        "id": "id",
                        "note": "note",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      }
                    ],
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "value_type": "boolean"
                  },
                  "credit_consumption_rate": 1.1,
                  "credit_grant_counts": {
                    "key": 1.1
                  },
                  "credit_grant_details": [
                    {
                      "grant_reason": "billing_credit_auto_topup",
                      "quantity": 1.1
                    }
                  ],
                  "credit_grant_reason": "billing_credit_auto_topup",
                  "credit_remaining": 1.1,
                  "credit_type_icon": "credit_type_icon",
                  "credit_used": 1.1,
                  "effective_limit": 1000000,
                  "effective_price": 1.1,
                  "entitlement_expiration_date": "2024-01-15T09:30:00.000Z",
                  "entitlement_id": "entitlement_id",
                  "entitlement_source": "entitlement_source",
                  "entitlement_type": "company_override",
                  "feature": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "feature_type": "boolean",
                    "flags": [
                      {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "default_value": true,
                        "description": "description",
                        "flag_type": "boolean",
                        "id": "id",
                        "key": "key",
                        "name": "name",
                        "rules": [
                          {
                            "condition_groups": [
                              {
                                "conditions": [
                                  {
                                    "condition_type": "condition_type",
                                    "created_at": "2024-01-15T09:30:00.000Z",
                                    "environment_id": "environment_id",
                                    "id": "id",
                                    "operator": "operator",
                                    "resource_ids": [
                                      "resource_ids"
                                    ],
                                    "resources": [
                                      {
                                        "id": "id",
                                        "name": "name"
                                      }
                                    ],
                                    "rule_id": "rule_id",
                                    "trait_value": "trait_value",
                                    "updated_at": "2024-01-15T09:30:00.000Z"
                                  }
                                ],
                                "created_at": "2024-01-15T09:30:00.000Z",
                                "environment_id": "environment_id",
                                "id": "id",
                                "rule_id": "rule_id",
                                "updated_at": "2024-01-15T09:30:00.000Z"
                              }
                            ],
                            "conditions": [
                              {
                                "condition_type": "condition_type",
                                "created_at": "2024-01-15T09:30:00.000Z",
                                "environment_id": "environment_id",
                                "id": "id",
                                "operator": "operator",
                                "resource_ids": [
                                  "resource_ids"
                                ],
                                "resources": [
                                  {
                                    "id": "id",
                                    "name": "name"
                                  }
                                ],
                                "rule_id": "rule_id",
                                "trait_value": "trait_value",
                                "updated_at": "2024-01-15T09:30:00.000Z"
                              }
                            ],
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "environment_id": "environment_id",
                            "id": "id",
                            "name": "name",
                            "priority": 1000000,
                            "rule_type": "rule_type",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "value": true
                          }
                        ],
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      }
                    ],
                    "icon": "icon",
                    "id": "id",
                    "name": "name",
                    "plans": [
                      {
                        "id": "id",
                        "name": "name"
                      }
                    ],
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "has_valid_allocation": true,
                  "is_unlimited": true,
                  "metric_reset_at": "2024-01-15T09:30:00.000Z",
                  "month_reset": "month_reset",
                  "monthly_usage_based_price": {
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
                    "provider_type": "schematic",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "usage_type": "licensed"
                  },
                  "overuse": 1000000,
                  "percent_used": 1.1,
                  "period": "period",
                  "plan": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "icon": "icon",
                    "id": "id",
                    "name": "name",
                    "plan_type": "plan",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "plan_entitlement": {
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
                  },
                  "price_behavior": "credit_burndown",
                  "soft_limit": 1000000,
                  "usage": 1000000,
                  "yearly_usage_based_price": {
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
                    "provider_type": "schematic",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "usage_type": "licensed"
                  }
                }
              ],
              "params": {
                "company_id": "company_id",
                "company_keys": {
                  "key": "value"
                },
                "feature_ids": [
                  "feature_ids"
                ],
                "include_usage_aggregation": true,
                "limit": 1000000,
                "offset": 1000000,
                "q": "q",
                "without_negative_entitlements": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/feature-usage")
                    .WithParam("company_id", "company_id")
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

        var response = await Client.Entitlements.ListFeatureUsageAsync(
            new ListFeatureUsageRequest
            {
                CompanyId = "company_id",
                IncludeUsageAggregation = true,
                Q = "q",
                WithoutNegativeEntitlements = true,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
