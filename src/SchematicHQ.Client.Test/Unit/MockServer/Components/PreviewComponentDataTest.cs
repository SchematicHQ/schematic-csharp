using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Components;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class PreviewComponentDataTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "active_add_ons": [
                  {
                    "charge_type": "free",
                    "company_can_trial": true,
                    "company_count": 1000000,
                    "compatible_plan_ids": [
                      "compatible_plan_ids"
                    ],
                    "controlled_by": "schematic",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
                    "current": true,
                    "custom": true,
                    "description": "description",
                    "entitlements": [
                      {
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
                      }
                    ],
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
                    "icon": "icon",
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
                    "is_custom": true,
                    "is_default": true,
                    "is_free": true,
                    "is_trialable": true,
                    "name": "name",
                    "plan_type": "plan",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "usage_violations": [
                      {
                        "access": true,
                        "allocation_type": "boolean",
                        "entitlement_id": "entitlement_id",
                        "entitlement_type": "company_override"
                      }
                    ],
                    "valid": true,
                    "versions": [
                      {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "description": "description",
                        "environment_id": "environment_id",
                        "icon": "icon",
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
                "active_plans": [
                  {
                    "charge_type": "free",
                    "company_can_trial": true,
                    "company_count": 1000000,
                    "compatible_plan_ids": [
                      "compatible_plan_ids"
                    ],
                    "controlled_by": "schematic",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
                    "current": true,
                    "custom": true,
                    "description": "description",
                    "entitlements": [
                      {
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
                      }
                    ],
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
                    "icon": "icon",
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
                    "is_custom": true,
                    "is_default": true,
                    "is_free": true,
                    "is_trialable": true,
                    "name": "name",
                    "plan_type": "plan",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "usage_violations": [
                      {
                        "access": true,
                        "allocation_type": "boolean",
                        "entitlement_id": "entitlement_id",
                        "entitlement_type": "company_override"
                      }
                    ],
                    "valid": true,
                    "versions": [
                      {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "description": "description",
                        "environment_id": "environment_id",
                        "icon": "icon",
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
                "active_usage_based_entitlements": [
                  {
                    "feature_id": "feature_id",
                    "value_type": "boolean"
                  }
                ],
                "add_on_compatibilities": [
                  {
                    "compatible_plan_ids": [
                      "compatible_plan_ids"
                    ],
                    "source_plan_id": "source_plan_id"
                  }
                ],
                "capabilities": {
                  "badge_visibility": true,
                  "checkout": true
                },
                "checkout_settings": {
                  "collect_address": true,
                  "collect_email": true,
                  "collect_phone": true,
                  "tax_collection_enabled": true
                },
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
                "component": {
                  "ast": {
                    "key": 1.1
                  },
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "id": "id",
                  "name": "name",
                  "state": "draft",
                  "type": "billing",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
                "credit_bundles": [
                  {
                    "bundle_type": "fixed",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "credit_id": "credit_id",
                    "credit_name": "credit_name",
                    "expiry_type": "duration",
                    "expiry_unit": "billing_periods",
                    "has_grants": true,
                    "id": "id",
                    "name": "name",
                    "status": "active",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  }
                ],
                "credit_grants": [
                  {
                    "billing_credit_id": "billing_credit_id",
                    "company_id": "company_id",
                    "company_name": "company_name",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "credit_description": "credit_description",
                    "credit_name": "credit_name",
                    "grant_reason": "billing_credit_auto_topup",
                    "id": "id",
                    "quantity": 1000000,
                    "quantity_remaining": 1.1,
                    "quantity_used": 1.1,
                    "renewal_enabled": true,
                    "source_label": "source_label",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  }
                ],
                "default_plan": {
                  "active_version": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "environment_id": "environment_id",
                    "icon": "icon",
                    "id": "id",
                    "name": "name",
                    "plan_type": "plan",
                    "status": "published",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "version": 1000000
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
                        "provider_type": "schematic",
                        "scheme": "per_unit"
                      }
                    ],
                    "product_id": "product_id",
                    "provider_type": "schematic",
                    "quantity": 1.1,
                    "subscription_count": 1000000,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "charge_type": "free",
                  "company_count": 1000000,
                  "controlled_by": "schematic",
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
                    "icon": "icon",
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
                  "icon": "icon",
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
                    "provider_type": "schematic",
                    "scheme": "per_unit"
                  },
                  "name": "name",
                  "one_time_price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "schematic",
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
                      "icon": "icon",
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
                    "provider_type": "schematic",
                    "scheme": "per_unit"
                  }
                },
                "display_settings": {
                  "show_as_monthly_prices": true,
                  "show_credits": true,
                  "show_feature_description": true,
                  "show_hard_limit": true,
                  "show_period_toggle": true,
                  "show_zero_price_as_free": true
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
                "invoices": [
                  {
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
                  }
                ],
                "post_trial_plan": {
                  "active_version": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "description": "description",
                    "environment_id": "environment_id",
                    "icon": "icon",
                    "id": "id",
                    "name": "name",
                    "plan_type": "plan",
                    "status": "published",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "version": 1000000
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
                        "provider_type": "schematic",
                        "scheme": "per_unit"
                      }
                    ],
                    "product_id": "product_id",
                    "provider_type": "schematic",
                    "quantity": 1.1,
                    "subscription_count": 1000000,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "charge_type": "free",
                  "company_count": 1000000,
                  "controlled_by": "schematic",
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
                    "icon": "icon",
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
                  "icon": "icon",
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
                    "provider_type": "schematic",
                    "scheme": "per_unit"
                  },
                  "name": "name",
                  "one_time_price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "schematic",
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
                      "icon": "icon",
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
                    "provider_type": "schematic",
                    "scheme": "per_unit"
                  }
                },
                "prevent_self_service_downgrade": true,
                "prevent_self_service_downgrade_button_text": "prevent_self_service_downgrade_button_text",
                "prevent_self_service_downgrade_url": "prevent_self_service_downgrade_url",
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
                "show_as_monthly_prices": true,
                "show_credits": true,
                "show_period_toggle": true,
                "show_zero_price_as_free": true,
                "stripe_embed": {
                  "account_id": "account_id",
                  "publishable_key": "publishable_key",
                  "schematic_publishable_key": "schematic_publishable_key",
                  "setup_intent_client_secret": "setup_intent_client_secret"
                },
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
                    "provider_type": "schematic",
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
                    "provider_type": "schematic",
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
                      "provider_type": "schematic",
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
                },
                "trial_payment_method_required": true,
                "upcoming_invoice": {
                  "amount_due": 1000000,
                  "amount_paid": 1000000,
                  "amount_remaining": 1000000,
                  "collection_method": "collection_method",
                  "company_id": "company_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency": "currency",
                  "customer_external_id": "customer_external_id",
                  "due_date": "2024-01-15T09:30:00.000Z",
                  "environment_id": "environment_id",
                  "external_id": "external_id",
                  "id": "id",
                  "payment_method_external_id": "payment_method_external_id",
                  "provider_type": "schematic",
                  "status": "draft",
                  "subscription_external_id": "subscription_external_id",
                  "subtotal": 1000000,
                  "updated_at": "2024-01-15T09:30:00.000Z",
                  "url": "url"
                }
              },
              "params": {
                "company_id": "company_id",
                "component_id": "component_id"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/components/preview-data")
                    .WithParam("company_id", "company_id")
                    .WithParam("component_id", "component_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Components.PreviewComponentDataAsync(
            new PreviewComponentDataRequest
            {
                CompanyId = "company_id",
                ComponentId = "component_id",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
