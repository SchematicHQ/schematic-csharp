using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetCompanyTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
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
                  "application_id": "application_id",
                  "cancel_at": 1000000,
                  "cancel_at_period_end": true,
                  "company_id": "company_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency": "currency",
                  "customer_external_id": "customer_external_id",
                  "default_payment_method_id": "default_payment_method_id",
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
                  "expired_at": "2024-01-15T09:30:00.000Z",
                  "id": "id",
                  "interval": "interval",
                  "latest_invoice": {
                    "amount_due": 1000000,
                    "amount_paid": 1000000,
                    "amount_remaining": 1000000,
                    "collection_method": "collection_method",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency": "currency",
                    "customer_external_id": "customer_external_id",
                    "environment_id": "environment_id",
                    "id": "id",
                    "provider_type": "schematic",
                    "subtotal": 1000000,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "metadata": {
                    "key": "value"
                  },
                  "payment_method": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "customer_external_id": "customer_external_id",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "id": "id",
                    "payment_method_type": "payment_method_type",
                    "provider_type": "schematic",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
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
                  "total_price": 1000000,
                  "trial_end": 1000000,
                  "trial_end_setting": "cancel"
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
                "default_payment_method": {
                  "account_last4": "account_last4",
                  "account_name": "account_name",
                  "bank_name": "bank_name",
                  "billing_email": "billing_email",
                  "billing_name": "billing_name",
                  "card_brand": "card_brand",
                  "card_exp_month": 1000000,
                  "card_exp_year": 1000000,
                  "card_last4": "card_last4",
                  "company_id": "company_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "customer_external_id": "customer_external_id",
                  "environment_id": "environment_id",
                  "external_id": "external_id",
                  "id": "id",
                  "payment_method_type": "payment_method_type",
                  "provider_type": "schematic",
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
                    "provider_type": "schematic",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  }
                ],
                "plan": {
                  "added_on": "2024-01-15T09:30:00.000Z",
                  "billing_product_external_id": "billing_product_external_id",
                  "billing_product_id": "billing_product_id",
                  "description": "description",
                  "id": "id",
                  "image_url": "image_url",
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
                  "name": "name",
                  "plan_period": "plan_period",
                  "plan_price": 1000000,
                  "plan_version_id": "plan_version_id"
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
                  "scheduled_interval": "scheduled_interval",
                  "scheduled_price": 1000000,
                  "to_plan_id": "to_plan_id",
                  "to_plan_name": "to_plan_name"
                },
                "traits": {
                  "key": "value"
                },
                "updated_at": "2024-01-15T09:30:00.000Z",
                "user_count": 1000000
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
                    .WithPath("/companies/company_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Companies.GetCompanyAsync("company_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
