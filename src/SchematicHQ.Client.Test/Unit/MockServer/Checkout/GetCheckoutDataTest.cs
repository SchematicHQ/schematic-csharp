using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Checkout;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetCheckoutDataTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "company_id": "company_id"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "active_add_ons": [
                  {
                    "charge_type": "free",
                    "company_count": 1000000,
                    "controlled_by": "orb",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
                    "description": "description",
                    "features": [
                      {
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
                      }
                    ],
                    "icon": "amber",
                    "id": "id",
                    "is_default": true,
                    "is_free": true,
                    "is_trialable": true,
                    "name": "name",
                    "plan_type": "plan",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "versions": [
                      {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "description": "description",
                        "environment_id": "environment_id",
                        "icon": "amber",
                        "id": "id",
                        "name": "name",
                        "plan_type": "plan",
                        "status": "published",
                        "updated_at": "2024-01-15T09:30:00.000Z",
                        "version": 1000000
                      }
                    ]
                  }
                ],
                "active_plan": {
                  "active_version": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "environment_id": "environment_id",
                    "icon": "amber",
                    "id": "id",
                    "name": "name",
                    "plan_type": "plan",
                    "status": "published",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "version": 1000000
                  },
                  "billing_linked_resource": {
                    "billing_provider": "orb",
                    "external_resource_id": "external_resource_id",
                    "originator": "orb"
                  },
                  "billing_product": {
                    "account_id": "account_id",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "is_active": true,
                    "name": "name",
                    "price": 1.1,
                    "prices": [
                      {
                        "currency": "currency",
                        "external_price_id": "external_price_id",
                        "id": "id",
                        "interval": "day",
                        "price": 1000000,
                        "provider_type": "orb",
                        "scheme": "per_unit"
                      }
                    ],
                    "product_id": "product_id",
                    "provider_type": "orb",
                    "quantity": 1.1,
                    "subscription_count": 1000000,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "charge_type": "free",
                  "company_count": 1000000,
                  "controlled_by": "orb",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency_prices": [
                    {
                      "currency": "currency"
                    }
                  ],
                  "description": "description",
                  "draft_version": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "environment_id": "environment_id",
                    "icon": "amber",
                    "id": "id",
                    "name": "name",
                    "plan_type": "plan",
                    "status": "published",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "version": 1000000
                  },
                  "features": [
                    {
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
                    }
                  ],
                  "icon": "amber",
                  "id": "id",
                  "included_credit_grants": [
                    {
                      "auto_topup_enabled": true,
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "credit_amount": 1000000,
                      "credit_id": "credit_id",
                      "credit_name": "credit_name",
                      "id": "id",
                      "plan_id": "plan_id",
                      "plan_name": "plan_name",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    }
                  ],
                  "is_default": true,
                  "is_free": true,
                  "is_trialable": true,
                  "monthly_price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "orb",
                    "scheme": "per_unit"
                  },
                  "name": "name",
                  "one_time_price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "orb",
                    "scheme": "per_unit"
                  },
                  "plan_type": "plan",
                  "trial_days": 1000000,
                  "updated_at": "2024-01-15T09:30:00.000Z",
                  "versions": [
                    {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "description": "description",
                      "environment_id": "environment_id",
                      "icon": "amber",
                      "id": "id",
                      "name": "name",
                      "plan_type": "plan",
                      "status": "published",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "version": 1000000
                    }
                  ],
                  "yearly_price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "orb",
                    "scheme": "per_unit"
                  }
                },
                "active_usage_based_entitlements": [
                  {
                    "feature_id": "feature_id",
                    "value_type": "boolean"
                  }
                ],
                "available_credit_bundles": [
                  {
                    "bundle_type": "fixed",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "credit_id": "credit_id",
                    "credit_name": "credit_name",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
                    "expiry_type": "duration",
                    "expiry_unit": "billing_periods",
                    "has_grants": true,
                    "id": "id",
                    "name": "name",
                    "status": "active",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  }
                ],
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
                "feature_usage": {
                  "features": [
                    {
                      "access": true,
                      "allocation_type": "boolean",
                      "entitlement_id": "entitlement_id",
                      "entitlement_type": "company_override"
                    }
                  ]
                },
                "selected_credit_bundles": [
                  {
                    "quantity": 1000000,
                    "total": 1000000
                  }
                ],
                "selected_plan": {
                  "active_version": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "environment_id": "environment_id",
                    "icon": "amber",
                    "id": "id",
                    "name": "name",
                    "plan_type": "plan",
                    "status": "published",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "version": 1000000
                  },
                  "billing_linked_resource": {
                    "billing_provider": "orb",
                    "external_resource_id": "external_resource_id",
                    "originator": "orb"
                  },
                  "billing_product": {
                    "account_id": "account_id",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "is_active": true,
                    "name": "name",
                    "price": 1.1,
                    "prices": [
                      {
                        "currency": "currency",
                        "external_price_id": "external_price_id",
                        "id": "id",
                        "interval": "day",
                        "price": 1000000,
                        "provider_type": "orb",
                        "scheme": "per_unit"
                      }
                    ],
                    "product_id": "product_id",
                    "provider_type": "orb",
                    "quantity": 1.1,
                    "subscription_count": 1000000,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "charge_type": "free",
                  "company_count": 1000000,
                  "controlled_by": "orb",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency_prices": [
                    {
                      "currency": "currency"
                    }
                  ],
                  "description": "description",
                  "draft_version": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "environment_id": "environment_id",
                    "icon": "amber",
                    "id": "id",
                    "name": "name",
                    "plan_type": "plan",
                    "status": "published",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "version": 1000000
                  },
                  "features": [
                    {
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
                    }
                  ],
                  "icon": "amber",
                  "id": "id",
                  "included_credit_grants": [
                    {
                      "auto_topup_enabled": true,
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "credit_amount": 1000000,
                      "credit_id": "credit_id",
                      "credit_name": "credit_name",
                      "id": "id",
                      "plan_id": "plan_id",
                      "plan_name": "plan_name",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    }
                  ],
                  "is_default": true,
                  "is_free": true,
                  "is_trialable": true,
                  "monthly_price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "orb",
                    "scheme": "per_unit"
                  },
                  "name": "name",
                  "one_time_price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "orb",
                    "scheme": "per_unit"
                  },
                  "plan_type": "plan",
                  "trial_days": 1000000,
                  "updated_at": "2024-01-15T09:30:00.000Z",
                  "versions": [
                    {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "description": "description",
                      "environment_id": "environment_id",
                      "icon": "amber",
                      "id": "id",
                      "name": "name",
                      "plan_type": "plan",
                      "status": "published",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "version": 1000000
                    }
                  ],
                  "yearly_price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "orb",
                    "scheme": "per_unit"
                  }
                },
                "selected_usage_based_entitlements": [
                  {
                    "feature_id": "feature_id",
                    "value_type": "boolean"
                  }
                ],
                "subscription": {
                  "cancel_at": "2024-01-15T09:30:00.000Z",
                  "cancel_at_period_end": true,
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
                  "expired_at": "2024-01-15T09:30:00.000Z",
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
                    "provider_type": "orb",
                    "subtotal": 1000000,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "payment_method": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "customer_external_id": "customer_external_id",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "id": "id",
                    "payment_method_type": "payment_method_type",
                    "provider_type": "orb",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
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
                  "status": "status",
                  "subscription_external_id": "subscription_external_id",
                  "total_price": 1000000,
                  "trial_end": "2024-01-15T09:30:00.000Z"
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
                    .WithPath("/checkout-internal/data")
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

        var response = await Client.Checkout.GetCheckoutDataAsync(
            new CheckoutDataRequestBody { CompanyId = "company_id" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
