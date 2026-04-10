using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Entitlements;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetCompanyOverrideTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
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
                  "billing_credit_balances": {
                    "key": 1.1
                  },
                  "billing_subscription": {
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
                        "provider_type": "orb",
                        "quantity": 1.1,
                        "subscription_id": "subscription_id",
                        "updated_at": "2024-01-15T09:30:00.000Z",
                        "usage_type": "licensed"
                      }
                    ],
                    "provider_type": "orb",
                    "status": "status",
                    "subscription_external_id": "subscription_external_id",
                    "total_price": 1000000
                  },
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
                          "provider_type": "orb",
                          "quantity": 1.1,
                          "subscription_id": "subscription_id",
                          "updated_at": "2024-01-15T09:30:00.000Z",
                          "usage_type": "licensed"
                        }
                      ],
                      "provider_type": "orb",
                      "status": "status",
                      "subscription_external_id": "subscription_external_id",
                      "total_price": 1000000
                    }
                  ],
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "default_payment_method": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "customer_external_id": "customer_external_id",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "id": "id",
                    "payment_method_type": "payment_method_type",
                    "provider_type": "orb",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
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
                  "last_seen_at": "2024-01-15T09:30:00.000Z",
                  "logo_url": "logo_url",
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
                      "provider_type": "orb",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    }
                  ],
                  "plan": {
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
                  },
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
                  "scheduled_downgrade": {
                    "currency": "currency",
                    "effective_after": "2024-01-15T09:30:00.000Z",
                    "from_plan_id": "from_plan_id",
                    "from_plan_name": "from_plan_name",
                    "from_subscription_price": 1000000,
                    "id": "id",
                    "interval": "interval",
                    "to_plan_id": "to_plan_id",
                    "to_plan_name": "to_plan_name"
                  },
                  "traits": {
                    "key": "value"
                  },
                  "updated_at": "2024-01-15T09:30:00.000Z",
                  "user_count": 1000000
                },
                "company_id": "company_id",
                "consumption_rate": 1.1,
                "created_at": "2024-01-15T09:30:00.000Z",
                "environment_id": "environment_id",
                "expiration_date": "2024-01-15T09:30:00.000Z",
                "feature": {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "description": "description",
                  "event_subtype": "event_subtype",
                  "feature_type": "boolean",
                  "icon": "icon",
                  "id": "id",
                  "lifecycle_phase": "add_on",
                  "maintainer_id": "maintainer_id",
                  "name": "name",
                  "plural_name": "plural_name",
                  "singular_name": "singular_name",
                  "trait_id": "trait_id",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
                "feature_id": "feature_id",
                "id": "id",
                "metric_period": "metric_period",
                "metric_period_month_reset": "metric_period_month_reset",
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
                "rule_id": "rule_id",
                "rule_id_usage_exceeded": "rule_id_usage_exceeded",
                "updated_at": "2024-01-15T09:30:00.000Z",
                "value_bool": true,
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
                    .WithPath("/company-overrides/company_override_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Entitlements.GetCompanyOverrideAsync("company_override_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
