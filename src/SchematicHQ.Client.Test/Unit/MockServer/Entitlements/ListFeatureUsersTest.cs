using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Entitlements;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListFeatureUsersTest : BaseMockServerTest
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
                  "company": {
                    "add_ons": [
                      {
                        "id": "id",
                        "included_credit_grants": [
                          {
                            "billing_credit_auto_topup_enabled": true,
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "credit_amount": 1000000,
                            "credit_description": "credit_description",
                            "credit_id": "credit_id",
                            "credit_name": "credit_name",
                            "id": "id",
                            "plan_id": "plan_id",
                            "reset_type": "no_reset",
                            "updated_at": "2024-01-15T09:30:00.000Z"
                          }
                        ],
                        "name": "name"
                      }
                    ],
                    "billing_subscriptions": [
                      {
                        "cancel_at_period_end": true,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "currency": "currency",
                        "customer_external_id": "customer_external_id",
                        "discounts": [
                          {
                            "coupon_id": "coupon_id",
                            "coupon_name": "coupon_name",
                            "discount_external_id": "discount_external_id",
                            "duration": "duration",
                            "is_active": true,
                            "started_at": "2024-01-15T09:30:00.000Z",
                            "subscription_external_id": "subscription_external_id"
                          }
                        ],
                        "id": "id",
                        "interval": "interval",
                        "period_end": 1000000,
                        "period_start": 1000000,
                        "products": [
                          {
                            "billing_scheme": "per_unit",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "currency": "currency",
                            "environment_id": "environment_id",
                            "external_id": "external_id",
                            "id": "id",
                            "interval": "interval",
                            "name": "name",
                            "package_size": 1000000,
                            "price": 1000000,
                            "price_external_id": "price_external_id",
                            "price_id": "price_id",
                            "price_tier": [
                              {}
                            ],
                            "provider_type": "schematic",
                            "quantity": 1.1,
                            "subscription_id": "subscription_id",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          }
                        ],
                        "provider_type": "schematic",
                        "status": "status",
                        "subscription_external_id": "subscription_external_id",
                        "total_price": 1000000
                      }
                    ],
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "entitlements": [
                      {
                        "feature_id": "feature_id",
                        "feature_key": "feature_key",
                        "value_type": "boolean"
                      }
                    ],
                    "entity_traits": [
                      {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "definition_id": "definition_id",
                        "environment_id": "environment_id",
                        "id": "id",
                        "updated_at": "2024-01-15T09:30:00.000Z",
                        "value": "value"
                      }
                    ],
                    "environment_id": "environment_id",
                    "id": "id",
                    "keys": [
                      {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "definition_id": "definition_id",
                        "entity_id": "entity_id",
                        "entity_type": "company",
                        "environment_id": "environment_id",
                        "id": "id",
                        "key": "key",
                        "updated_at": "2024-01-15T09:30:00.000Z",
                        "value": "value"
                      }
                    ],
                    "metrics": [
                      {
                        "account_id": "account_id",
                        "captured_at_max": "2024-01-15T09:30:00.000Z",
                        "captured_at_min": "2024-01-15T09:30:00.000Z",
                        "company_id": "company_id",
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "environment_id": "environment_id",
                        "event_subtype": "event_subtype",
                        "month_reset": "month_reset",
                        "period": "period",
                        "value": 1000000
                      }
                    ],
                    "name": "name",
                    "payment_methods": [
                      {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "customer_external_id": "customer_external_id",
                        "environment_id": "environment_id",
                        "external_id": "external_id",
                        "id": "id",
                        "payment_method_type": "payment_method_type",
                        "provider_type": "schematic",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      }
                    ],
                    "plans": [
                      {
                        "id": "id",
                        "name": "name"
                      }
                    ],
                    "rules": [
                      {
                        "account_id": "account_id",
                        "condition_groups": [
                          {
                            "conditions": [
                              {
                                "account_id": "account_id",
                                "condition_type": "base_plan",
                                "environment_id": "environment_id",
                                "id": "id",
                                "operator": "eq",
                                "resource_ids": [
                                  "resource_ids"
                                ],
                                "trait_value": "trait_value"
                              }
                            ]
                          }
                        ],
                        "conditions": [
                          {
                            "account_id": "account_id",
                            "condition_type": "base_plan",
                            "environment_id": "environment_id",
                            "id": "id",
                            "operator": "eq",
                            "resource_ids": [
                              "resource_ids"
                            ],
                            "trait_value": "trait_value"
                          }
                        ],
                        "environment_id": "environment_id",
                        "id": "id",
                        "name": "name",
                        "priority": 1000000,
                        "rule_type": "default",
                        "value": true
                      }
                    ],
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "user_count": 1000000
                  },
                  "entitlement_id": "entitlement_id",
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
                  "metric_reset_at": "2024-01-15T09:30:00.000Z",
                  "month_reset": "month_reset",
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
                  "usage": 1000000,
                  "user": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_id": "environment_id",
                    "id": "id",
                    "name": "name",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  }
                }
              ],
              "params": {
                "feature_id": "feature_id",
                "limit": 1000000,
                "offset": 1000000,
                "q": "q"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/feature-users")
                    .WithParam("feature_id", "feature_id")
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

        var response = await Client.Entitlements.ListFeatureUsersAsync(
            new ListFeatureUsersRequest
            {
                FeatureId = "feature_id",
                Q = "q",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
