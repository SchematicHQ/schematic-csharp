using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Entitlements;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetFeatureUsageByCompanyTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "features": [
                  {
                    "access": true,
                    "allocation": 1000000,
                    "allocation_type": "boolean",
                    "company_override": {
                      "company": {
                        "add_ons": [
                          {
                            "id": "id",
                            "included_credit_grants": [],
                            "name": "name"
                          },
                          {
                            "id": "id",
                            "included_credit_grants": [],
                            "name": "name"
                          }
                        ],
                        "billing_credit_balances": {
                          "billing_credit_balances": 1.1
                        },
                        "billing_subscription": {
                          "cancel_at_period_end": true,
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "currency": "currency",
                          "customer_external_id": "customer_external_id",
                          "discounts": [],
                          "id": "id",
                          "interval": "interval",
                          "period_end": 1000000,
                          "period_start": 1000000,
                          "products": [],
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
                            "discounts": [],
                            "id": "id",
                            "interval": "interval",
                            "period_end": 1000000,
                            "period_start": 1000000,
                            "products": [],
                            "provider_type": "orb",
                            "status": "status",
                            "subscription_external_id": "subscription_external_id",
                            "total_price": 1000000
                          },
                          {
                            "cancel_at_period_end": true,
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "currency": "currency",
                            "customer_external_id": "customer_external_id",
                            "discounts": [],
                            "id": "id",
                            "interval": "interval",
                            "period_end": 1000000,
                            "period_start": 1000000,
                            "products": [],
                            "provider_type": "orb",
                            "status": "status",
                            "subscription_external_id": "subscription_external_id",
                            "total_price": 1000000
                          }
                        ],
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "custom_plan_billings": [
                          {
                            "activation_strategy": "on_payment",
                            "company_id": "company_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "days_until_due": 1000000,
                            "id": "id",
                            "plan_id": "plan_id",
                            "status": "active",
                            "updated_at": "2024-01-15T09:30:00.000Z"
                          },
                          {
                            "activation_strategy": "on_payment",
                            "company_id": "company_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "days_until_due": 1000000,
                            "id": "id",
                            "plan_id": "plan_id",
                            "status": "active",
                            "updated_at": "2024-01-15T09:30:00.000Z"
                          }
                        ],
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
                          },
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
                          },
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
                          },
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
                            "month_reset": "billing_cycle",
                            "period": "all_time",
                            "value": 1000000
                          },
                          {
                            "account_id": "account_id",
                            "captured_at_max": "2024-01-15T09:30:00.000Z",
                            "captured_at_min": "2024-01-15T09:30:00.000Z",
                            "company_id": "company_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "month_reset": "billing_cycle",
                            "period": "all_time",
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
                          },
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
                          "included_credit_grants": [],
                          "name": "name"
                        },
                        "plans": [
                          {
                            "id": "id",
                            "name": "name"
                          },
                          {
                            "id": "id",
                            "name": "name"
                          }
                        ],
                        "rules": [
                          {
                            "account_id": "account_id",
                            "condition_groups": [],
                            "conditions": [],
                            "environment_id": "environment_id",
                            "id": "id",
                            "name": "name",
                            "priority": 1000000,
                            "rule_type": "company_override",
                            "value": true
                          },
                          {
                            "account_id": "account_id",
                            "condition_groups": [],
                            "conditions": [],
                            "environment_id": "environment_id",
                            "id": "id",
                            "name": "name",
                            "priority": 1000000,
                            "rule_type": "company_override",
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
                          "traits": {
                            "key": "value"
                          }
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
                        "maintainer_account_member_id": "maintainer_account_member_id",
                        "name": "name",
                        "plural_name": "plural_name",
                        "singular_name": "singular_name",
                        "trait_id": "trait_id",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "feature_id": "feature_id",
                      "id": "id",
                      "metric_period": "all_time",
                      "metric_period_month_reset": "billing_cycle",
                      "notes": [
                        {
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "external_user_id": "external_user_id",
                          "external_user_name": "external_user_name",
                          "id": "id",
                          "note": "note",
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
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
                          "hierarchy",
                          "hierarchy"
                        ],
                        "id": "id",
                        "trait_type": "boolean",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "value_trait_id": "value_trait_id",
                      "value_type": "boolean"
                    },
                    "credit_consumption_rate": 1.1,
                    "credit_grant_counts": {
                      "credit_grant_counts": 1.1
                    },
                    "credit_grant_details": [
                      {
                        "credit_type_icon": "credit_type_icon",
                        "expires_at": "2024-01-15T09:30:00.000Z",
                        "grant_reason": "adjustment",
                        "quantity": 1.1
                      },
                      {
                        "credit_type_icon": "credit_type_icon",
                        "expires_at": "2024-01-15T09:30:00.000Z",
                        "grant_reason": "adjustment",
                        "quantity": 1.1
                      }
                    ],
                    "credit_grant_reason": "adjustment",
                    "credit_remaining": 1.1,
                    "credit_total": 1.1,
                    "credit_type_icon": "credit_type_icon",
                    "credit_usage_aggregation": {
                      "usage_this_billing_period": 1.1,
                      "usage_this_calendar_month": 1.1,
                      "usage_this_week": 1.1,
                      "usage_today": 1.1
                    },
                    "credit_used": 1.1,
                    "effective_limit": 1000000,
                    "effective_price": 1.1,
                    "entitlement_expiration_date": "2024-01-15T09:30:00.000Z",
                    "entitlement_id": "entitlement_id",
                    "entitlement_source": "entitlement_source",
                    "entitlement_type": "company_override",
                    "feature": {
                      "billing_linked_resource": {
                        "billing_provider": "orb",
                        "external_resource_id": "external_resource_id",
                        "originator": "orb"
                      },
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "description": "description",
                      "event_subtype": "event_subtype",
                      "event_summary": {
                        "company_count": 1000000,
                        "environment_id": "environment_id",
                        "event_count": 1000000,
                        "event_subtype": "event_subtype",
                        "last_seen_at": "2024-01-15T09:30:00.000Z",
                        "user_count": 1000000
                      },
                      "feature_type": "boolean",
                      "flags": [
                        {
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "default_value": true,
                          "description": "description",
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
                          "flag_type": "boolean",
                          "id": "id",
                          "key": "key",
                          "last_checked_at": "2024-01-15T09:30:00.000Z",
                          "maintainer_account_member_id": "maintainer_account_member_id",
                          "name": "name",
                          "rules": [
                            {
                              "condition_groups": [],
                              "conditions": [],
                              "created_at": "2024-01-15T09:30:00.000Z",
                              "environment_id": "environment_id",
                              "id": "id",
                              "name": "name",
                              "priority": 1000000,
                              "rule_type": "company_override",
                              "updated_at": "2024-01-15T09:30:00.000Z",
                              "value": true
                            },
                            {
                              "condition_groups": [],
                              "conditions": [],
                              "created_at": "2024-01-15T09:30:00.000Z",
                              "environment_id": "environment_id",
                              "id": "id",
                              "name": "name",
                              "priority": 1000000,
                              "rule_type": "company_override",
                              "updated_at": "2024-01-15T09:30:00.000Z",
                              "value": true
                            }
                          ],
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
                        {
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "default_value": true,
                          "description": "description",
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
                          "flag_type": "boolean",
                          "id": "id",
                          "key": "key",
                          "last_checked_at": "2024-01-15T09:30:00.000Z",
                          "maintainer_account_member_id": "maintainer_account_member_id",
                          "name": "name",
                          "rules": [
                            {
                              "condition_groups": [],
                              "conditions": [],
                              "created_at": "2024-01-15T09:30:00.000Z",
                              "environment_id": "environment_id",
                              "id": "id",
                              "name": "name",
                              "priority": 1000000,
                              "rule_type": "company_override",
                              "updated_at": "2024-01-15T09:30:00.000Z",
                              "value": true
                            },
                            {
                              "condition_groups": [],
                              "conditions": [],
                              "created_at": "2024-01-15T09:30:00.000Z",
                              "environment_id": "environment_id",
                              "id": "id",
                              "name": "name",
                              "priority": 1000000,
                              "rule_type": "company_override",
                              "updated_at": "2024-01-15T09:30:00.000Z",
                              "value": true
                            }
                          ],
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        }
                      ],
                      "icon": "icon",
                      "id": "id",
                      "lifecycle_phase": "add_on",
                      "maintainer_account_member_id": "maintainer_account_member_id",
                      "name": "name",
                      "plans": [
                        {
                          "description": "description",
                          "id": "id",
                          "image_url": "image_url",
                          "name": "name"
                        },
                        {
                          "description": "description",
                          "id": "id",
                          "image_url": "image_url",
                          "name": "name"
                        }
                      ],
                      "plural_name": "plural_name",
                      "singular_name": "singular_name",
                      "trait": {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "display_name": "display_name",
                        "entity_type": "company",
                        "hierarchy": [
                          "hierarchy",
                          "hierarchy"
                        ],
                        "id": "id",
                        "trait_type": "boolean",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "trait_id": "trait_id",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
                    "has_valid_allocation": true,
                    "is_unlimited": true,
                    "metric_reset_at": "2024-01-15T09:30:00.000Z",
                    "month_reset": "billing_cycle",
                    "monthly_usage_based_price": {
                      "billing_scheme": "per_unit",
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "currency": "currency",
                      "id": "id",
                      "interval": "day",
                      "is_active": true,
                      "meter_event_name": "meter_event_name",
                      "meter_event_payload_key": "meter_event_payload_key",
                      "meter_id": "meter_id",
                      "nickname": "nickname",
                      "package_size": 1000000,
                      "price": 1000000,
                      "price_decimal": "price_decimal",
                      "price_external_id": "price_external_id",
                      "price_id": "price_id",
                      "price_tier": [
                        {
                          "flat_amount": 1000000,
                          "per_unit_price": 1000000,
                          "per_unit_price_decimal": "per_unit_price_decimal",
                          "up_to": 1000000
                        },
                        {
                          "flat_amount": 1000000,
                          "per_unit_price": 1000000,
                          "per_unit_price_decimal": "per_unit_price_decimal",
                          "up_to": 1000000
                        }
                      ],
                      "product_external_id": "product_external_id",
                      "product_id": "product_id",
                      "product_name": "product_name",
                      "provider_type": "orb",
                      "tiers_mode": "graduated",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "usage_type": "licensed"
                    },
                    "overuse": 1000000,
                    "percent_used": 1.1,
                    "period": "all_time",
                    "plan": {
                      "audience_type": "audience_type",
                      "company_id": "company_id",
                      "copied_from_plan_id": "copied_from_plan_id",
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "description": "description",
                      "icon": "amber",
                      "id": "id",
                      "name": "name",
                      "plan_type": "plan",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
                    "plan_entitlement": {
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
                          "currency": "currency",
                          "monthly_price": {
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
                            "price_tier": [],
                            "product_external_id": "product_external_id",
                            "product_id": "product_id",
                            "product_name": "product_name",
                            "provider_type": "orb",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          },
                          "yearly_price": {
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
                            "price_tier": [],
                            "product_external_id": "product_external_id",
                            "product_id": "product_id",
                            "product_name": "product_name",
                            "provider_type": "orb",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          }
                        },
                        {
                          "currency": "currency",
                          "monthly_price": {
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
                            "price_tier": [],
                            "product_external_id": "product_external_id",
                            "product_id": "product_id",
                            "product_name": "product_name",
                            "provider_type": "orb",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          },
                          "yearly_price": {
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
                            "price_tier": [],
                            "product_external_id": "product_external_id",
                            "product_id": "product_id",
                            "product_name": "product_name",
                            "provider_type": "orb",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          }
                        }
                      ],
                      "environment_id": "environment_id",
                      "feature": {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "description": "description",
                        "event_subtype": "event_subtype",
                        "feature_type": "boolean",
                        "icon": "icon",
                        "id": "id",
                        "lifecycle_phase": "add_on",
                        "maintainer_account_member_id": "maintainer_account_member_id",
                        "name": "name",
                        "plural_name": "plural_name",
                        "singular_name": "singular_name",
                        "trait_id": "trait_id",
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
                        "meter_event_name": "meter_event_name",
                        "meter_event_payload_key": "meter_event_payload_key",
                        "meter_id": "meter_id",
                        "nickname": "nickname",
                        "package_size": 1000000,
                        "price": 1000000,
                        "price_decimal": "price_decimal",
                        "price_external_id": "price_external_id",
                        "price_id": "price_id",
                        "price_tier": [
                          {},
                          {}
                        ],
                        "product_external_id": "product_external_id",
                        "product_id": "product_id",
                        "product_name": "product_name",
                        "provider_type": "orb",
                        "tiers_mode": "graduated",
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
                        "meter_event_name": "meter_event_name",
                        "meter_event_payload_key": "meter_event_payload_key",
                        "meter_id": "meter_id",
                        "nickname": "nickname",
                        "package_size": 1000000,
                        "price": 1000000,
                        "price_decimal": "price_decimal",
                        "price_external_id": "price_external_id",
                        "price_id": "price_id",
                        "price_tier": [
                          {},
                          {}
                        ],
                        "product_external_id": "product_external_id",
                        "product_id": "product_id",
                        "product_name": "product_name",
                        "provider_type": "orb",
                        "tiers_mode": "graduated",
                        "updated_at": "2024-01-15T09:30:00.000Z",
                        "usage_type": "licensed"
                      },
                      "metric_period": "all_time",
                      "metric_period_month_reset": "billing_cycle",
                      "plan": {
                        "audience_type": "audience_type",
                        "company_id": "company_id",
                        "copied_from_plan_id": "copied_from_plan_id",
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
                        "currency": "currency",
                        "environment_id": "environment_id",
                        "external_id": "external_id",
                        "is_active": true,
                        "name": "name",
                        "price": 1.1,
                        "price_decimal": "price_decimal",
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
                          },
                          {
                            "currency": "currency"
                          }
                        ],
                        "default_expiry_unit": "billing_periods",
                        "default_expiry_unit_count": 1000000,
                        "default_rollover_policy": "expire",
                        "description": "description",
                        "icon": "icon",
                        "id": "id",
                        "name": "name",
                        "plural_name": "plural_name",
                        "price": {
                          "currency": "currency",
                          "external_price_id": "external_price_id",
                          "id": "id",
                          "interval": "day",
                          "price": 1000000,
                          "provider_type": "orb",
                          "scheme": "per_unit"
                        },
                        "product": {
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
                        "singular_name": "singular_name",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "value_numeric": 1000000,
                      "value_trait": {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "display_name": "display_name",
                        "entity_type": "company",
                        "hierarchy": [
                          "hierarchy",
                          "hierarchy"
                        ],
                        "id": "id",
                        "trait_type": "boolean",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "value_trait_id": "value_trait_id",
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
                      "meter_event_name": "meter_event_name",
                      "meter_event_payload_key": "meter_event_payload_key",
                      "meter_id": "meter_id",
                      "nickname": "nickname",
                      "package_size": 1000000,
                      "price": 1000000,
                      "price_decimal": "price_decimal",
                      "price_external_id": "price_external_id",
                      "price_id": "price_id",
                      "price_tier": [
                        {
                          "flat_amount": 1000000,
                          "per_unit_price": 1000000,
                          "per_unit_price_decimal": "per_unit_price_decimal",
                          "up_to": 1000000
                        },
                        {
                          "flat_amount": 1000000,
                          "per_unit_price": 1000000,
                          "per_unit_price_decimal": "per_unit_price_decimal",
                          "up_to": 1000000
                        }
                      ],
                      "product_external_id": "product_external_id",
                      "product_id": "product_id",
                      "product_name": "product_name",
                      "provider_type": "orb",
                      "tiers_mode": "graduated",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "usage_type": "licensed"
                    }
                  },
                  {
                    "access": true,
                    "allocation": 1000000,
                    "allocation_type": "boolean",
                    "company_override": {
                      "company": {
                        "add_ons": [
                          {
                            "id": "id",
                            "included_credit_grants": [],
                            "name": "name"
                          },
                          {
                            "id": "id",
                            "included_credit_grants": [],
                            "name": "name"
                          }
                        ],
                        "billing_credit_balances": {
                          "billing_credit_balances": 1.1
                        },
                        "billing_subscription": {
                          "cancel_at_period_end": true,
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "currency": "currency",
                          "customer_external_id": "customer_external_id",
                          "discounts": [],
                          "id": "id",
                          "interval": "interval",
                          "period_end": 1000000,
                          "period_start": 1000000,
                          "products": [],
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
                            "discounts": [],
                            "id": "id",
                            "interval": "interval",
                            "period_end": 1000000,
                            "period_start": 1000000,
                            "products": [],
                            "provider_type": "orb",
                            "status": "status",
                            "subscription_external_id": "subscription_external_id",
                            "total_price": 1000000
                          },
                          {
                            "cancel_at_period_end": true,
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "currency": "currency",
                            "customer_external_id": "customer_external_id",
                            "discounts": [],
                            "id": "id",
                            "interval": "interval",
                            "period_end": 1000000,
                            "period_start": 1000000,
                            "products": [],
                            "provider_type": "orb",
                            "status": "status",
                            "subscription_external_id": "subscription_external_id",
                            "total_price": 1000000
                          }
                        ],
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "custom_plan_billings": [
                          {
                            "activation_strategy": "on_payment",
                            "company_id": "company_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "days_until_due": 1000000,
                            "id": "id",
                            "plan_id": "plan_id",
                            "status": "active",
                            "updated_at": "2024-01-15T09:30:00.000Z"
                          },
                          {
                            "activation_strategy": "on_payment",
                            "company_id": "company_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "days_until_due": 1000000,
                            "id": "id",
                            "plan_id": "plan_id",
                            "status": "active",
                            "updated_at": "2024-01-15T09:30:00.000Z"
                          }
                        ],
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
                          },
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
                          },
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
                          },
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
                            "month_reset": "billing_cycle",
                            "period": "all_time",
                            "value": 1000000
                          },
                          {
                            "account_id": "account_id",
                            "captured_at_max": "2024-01-15T09:30:00.000Z",
                            "captured_at_min": "2024-01-15T09:30:00.000Z",
                            "company_id": "company_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "month_reset": "billing_cycle",
                            "period": "all_time",
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
                          },
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
                          "included_credit_grants": [],
                          "name": "name"
                        },
                        "plans": [
                          {
                            "id": "id",
                            "name": "name"
                          },
                          {
                            "id": "id",
                            "name": "name"
                          }
                        ],
                        "rules": [
                          {
                            "account_id": "account_id",
                            "condition_groups": [],
                            "conditions": [],
                            "environment_id": "environment_id",
                            "id": "id",
                            "name": "name",
                            "priority": 1000000,
                            "rule_type": "company_override",
                            "value": true
                          },
                          {
                            "account_id": "account_id",
                            "condition_groups": [],
                            "conditions": [],
                            "environment_id": "environment_id",
                            "id": "id",
                            "name": "name",
                            "priority": 1000000,
                            "rule_type": "company_override",
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
                          "traits": {
                            "key": "value"
                          }
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
                        "maintainer_account_member_id": "maintainer_account_member_id",
                        "name": "name",
                        "plural_name": "plural_name",
                        "singular_name": "singular_name",
                        "trait_id": "trait_id",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "feature_id": "feature_id",
                      "id": "id",
                      "metric_period": "all_time",
                      "metric_period_month_reset": "billing_cycle",
                      "notes": [
                        {
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "external_user_id": "external_user_id",
                          "external_user_name": "external_user_name",
                          "id": "id",
                          "note": "note",
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
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
                          "hierarchy",
                          "hierarchy"
                        ],
                        "id": "id",
                        "trait_type": "boolean",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "value_trait_id": "value_trait_id",
                      "value_type": "boolean"
                    },
                    "credit_consumption_rate": 1.1,
                    "credit_grant_counts": {
                      "credit_grant_counts": 1.1
                    },
                    "credit_grant_details": [
                      {
                        "credit_type_icon": "credit_type_icon",
                        "expires_at": "2024-01-15T09:30:00.000Z",
                        "grant_reason": "adjustment",
                        "quantity": 1.1
                      },
                      {
                        "credit_type_icon": "credit_type_icon",
                        "expires_at": "2024-01-15T09:30:00.000Z",
                        "grant_reason": "adjustment",
                        "quantity": 1.1
                      }
                    ],
                    "credit_grant_reason": "adjustment",
                    "credit_remaining": 1.1,
                    "credit_total": 1.1,
                    "credit_type_icon": "credit_type_icon",
                    "credit_usage_aggregation": {
                      "usage_this_billing_period": 1.1,
                      "usage_this_calendar_month": 1.1,
                      "usage_this_week": 1.1,
                      "usage_today": 1.1
                    },
                    "credit_used": 1.1,
                    "effective_limit": 1000000,
                    "effective_price": 1.1,
                    "entitlement_expiration_date": "2024-01-15T09:30:00.000Z",
                    "entitlement_id": "entitlement_id",
                    "entitlement_source": "entitlement_source",
                    "entitlement_type": "company_override",
                    "feature": {
                      "billing_linked_resource": {
                        "billing_provider": "orb",
                        "external_resource_id": "external_resource_id",
                        "originator": "orb"
                      },
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "description": "description",
                      "event_subtype": "event_subtype",
                      "event_summary": {
                        "company_count": 1000000,
                        "environment_id": "environment_id",
                        "event_count": 1000000,
                        "event_subtype": "event_subtype",
                        "last_seen_at": "2024-01-15T09:30:00.000Z",
                        "user_count": 1000000
                      },
                      "feature_type": "boolean",
                      "flags": [
                        {
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "default_value": true,
                          "description": "description",
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
                          "flag_type": "boolean",
                          "id": "id",
                          "key": "key",
                          "last_checked_at": "2024-01-15T09:30:00.000Z",
                          "maintainer_account_member_id": "maintainer_account_member_id",
                          "name": "name",
                          "rules": [
                            {
                              "condition_groups": [],
                              "conditions": [],
                              "created_at": "2024-01-15T09:30:00.000Z",
                              "environment_id": "environment_id",
                              "id": "id",
                              "name": "name",
                              "priority": 1000000,
                              "rule_type": "company_override",
                              "updated_at": "2024-01-15T09:30:00.000Z",
                              "value": true
                            },
                            {
                              "condition_groups": [],
                              "conditions": [],
                              "created_at": "2024-01-15T09:30:00.000Z",
                              "environment_id": "environment_id",
                              "id": "id",
                              "name": "name",
                              "priority": 1000000,
                              "rule_type": "company_override",
                              "updated_at": "2024-01-15T09:30:00.000Z",
                              "value": true
                            }
                          ],
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
                        {
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "default_value": true,
                          "description": "description",
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
                          "flag_type": "boolean",
                          "id": "id",
                          "key": "key",
                          "last_checked_at": "2024-01-15T09:30:00.000Z",
                          "maintainer_account_member_id": "maintainer_account_member_id",
                          "name": "name",
                          "rules": [
                            {
                              "condition_groups": [],
                              "conditions": [],
                              "created_at": "2024-01-15T09:30:00.000Z",
                              "environment_id": "environment_id",
                              "id": "id",
                              "name": "name",
                              "priority": 1000000,
                              "rule_type": "company_override",
                              "updated_at": "2024-01-15T09:30:00.000Z",
                              "value": true
                            },
                            {
                              "condition_groups": [],
                              "conditions": [],
                              "created_at": "2024-01-15T09:30:00.000Z",
                              "environment_id": "environment_id",
                              "id": "id",
                              "name": "name",
                              "priority": 1000000,
                              "rule_type": "company_override",
                              "updated_at": "2024-01-15T09:30:00.000Z",
                              "value": true
                            }
                          ],
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        }
                      ],
                      "icon": "icon",
                      "id": "id",
                      "lifecycle_phase": "add_on",
                      "maintainer_account_member_id": "maintainer_account_member_id",
                      "name": "name",
                      "plans": [
                        {
                          "description": "description",
                          "id": "id",
                          "image_url": "image_url",
                          "name": "name"
                        },
                        {
                          "description": "description",
                          "id": "id",
                          "image_url": "image_url",
                          "name": "name"
                        }
                      ],
                      "plural_name": "plural_name",
                      "singular_name": "singular_name",
                      "trait": {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "display_name": "display_name",
                        "entity_type": "company",
                        "hierarchy": [
                          "hierarchy",
                          "hierarchy"
                        ],
                        "id": "id",
                        "trait_type": "boolean",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "trait_id": "trait_id",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
                    "has_valid_allocation": true,
                    "is_unlimited": true,
                    "metric_reset_at": "2024-01-15T09:30:00.000Z",
                    "month_reset": "billing_cycle",
                    "monthly_usage_based_price": {
                      "billing_scheme": "per_unit",
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "currency": "currency",
                      "id": "id",
                      "interval": "day",
                      "is_active": true,
                      "meter_event_name": "meter_event_name",
                      "meter_event_payload_key": "meter_event_payload_key",
                      "meter_id": "meter_id",
                      "nickname": "nickname",
                      "package_size": 1000000,
                      "price": 1000000,
                      "price_decimal": "price_decimal",
                      "price_external_id": "price_external_id",
                      "price_id": "price_id",
                      "price_tier": [
                        {
                          "flat_amount": 1000000,
                          "per_unit_price": 1000000,
                          "per_unit_price_decimal": "per_unit_price_decimal",
                          "up_to": 1000000
                        },
                        {
                          "flat_amount": 1000000,
                          "per_unit_price": 1000000,
                          "per_unit_price_decimal": "per_unit_price_decimal",
                          "up_to": 1000000
                        }
                      ],
                      "product_external_id": "product_external_id",
                      "product_id": "product_id",
                      "product_name": "product_name",
                      "provider_type": "orb",
                      "tiers_mode": "graduated",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "usage_type": "licensed"
                    },
                    "overuse": 1000000,
                    "percent_used": 1.1,
                    "period": "all_time",
                    "plan": {
                      "audience_type": "audience_type",
                      "company_id": "company_id",
                      "copied_from_plan_id": "copied_from_plan_id",
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "description": "description",
                      "icon": "amber",
                      "id": "id",
                      "name": "name",
                      "plan_type": "plan",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
                    "plan_entitlement": {
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
                          "currency": "currency",
                          "monthly_price": {
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
                            "price_tier": [],
                            "product_external_id": "product_external_id",
                            "product_id": "product_id",
                            "product_name": "product_name",
                            "provider_type": "orb",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          },
                          "yearly_price": {
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
                            "price_tier": [],
                            "product_external_id": "product_external_id",
                            "product_id": "product_id",
                            "product_name": "product_name",
                            "provider_type": "orb",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          }
                        },
                        {
                          "currency": "currency",
                          "monthly_price": {
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
                            "price_tier": [],
                            "product_external_id": "product_external_id",
                            "product_id": "product_id",
                            "product_name": "product_name",
                            "provider_type": "orb",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          },
                          "yearly_price": {
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
                            "price_tier": [],
                            "product_external_id": "product_external_id",
                            "product_id": "product_id",
                            "product_name": "product_name",
                            "provider_type": "orb",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          }
                        }
                      ],
                      "environment_id": "environment_id",
                      "feature": {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "description": "description",
                        "event_subtype": "event_subtype",
                        "feature_type": "boolean",
                        "icon": "icon",
                        "id": "id",
                        "lifecycle_phase": "add_on",
                        "maintainer_account_member_id": "maintainer_account_member_id",
                        "name": "name",
                        "plural_name": "plural_name",
                        "singular_name": "singular_name",
                        "trait_id": "trait_id",
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
                        "meter_event_name": "meter_event_name",
                        "meter_event_payload_key": "meter_event_payload_key",
                        "meter_id": "meter_id",
                        "nickname": "nickname",
                        "package_size": 1000000,
                        "price": 1000000,
                        "price_decimal": "price_decimal",
                        "price_external_id": "price_external_id",
                        "price_id": "price_id",
                        "price_tier": [
                          {},
                          {}
                        ],
                        "product_external_id": "product_external_id",
                        "product_id": "product_id",
                        "product_name": "product_name",
                        "provider_type": "orb",
                        "tiers_mode": "graduated",
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
                        "meter_event_name": "meter_event_name",
                        "meter_event_payload_key": "meter_event_payload_key",
                        "meter_id": "meter_id",
                        "nickname": "nickname",
                        "package_size": 1000000,
                        "price": 1000000,
                        "price_decimal": "price_decimal",
                        "price_external_id": "price_external_id",
                        "price_id": "price_id",
                        "price_tier": [
                          {},
                          {}
                        ],
                        "product_external_id": "product_external_id",
                        "product_id": "product_id",
                        "product_name": "product_name",
                        "provider_type": "orb",
                        "tiers_mode": "graduated",
                        "updated_at": "2024-01-15T09:30:00.000Z",
                        "usage_type": "licensed"
                      },
                      "metric_period": "all_time",
                      "metric_period_month_reset": "billing_cycle",
                      "plan": {
                        "audience_type": "audience_type",
                        "company_id": "company_id",
                        "copied_from_plan_id": "copied_from_plan_id",
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
                        "currency": "currency",
                        "environment_id": "environment_id",
                        "external_id": "external_id",
                        "is_active": true,
                        "name": "name",
                        "price": 1.1,
                        "price_decimal": "price_decimal",
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
                          },
                          {
                            "currency": "currency"
                          }
                        ],
                        "default_expiry_unit": "billing_periods",
                        "default_expiry_unit_count": 1000000,
                        "default_rollover_policy": "expire",
                        "description": "description",
                        "icon": "icon",
                        "id": "id",
                        "name": "name",
                        "plural_name": "plural_name",
                        "price": {
                          "currency": "currency",
                          "external_price_id": "external_price_id",
                          "id": "id",
                          "interval": "day",
                          "price": 1000000,
                          "provider_type": "orb",
                          "scheme": "per_unit"
                        },
                        "product": {
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
                        "singular_name": "singular_name",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "value_numeric": 1000000,
                      "value_trait": {
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "display_name": "display_name",
                        "entity_type": "company",
                        "hierarchy": [
                          "hierarchy",
                          "hierarchy"
                        ],
                        "id": "id",
                        "trait_type": "boolean",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "value_trait_id": "value_trait_id",
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
                      "meter_event_name": "meter_event_name",
                      "meter_event_payload_key": "meter_event_payload_key",
                      "meter_id": "meter_id",
                      "nickname": "nickname",
                      "package_size": 1000000,
                      "price": 1000000,
                      "price_decimal": "price_decimal",
                      "price_external_id": "price_external_id",
                      "price_id": "price_id",
                      "price_tier": [
                        {
                          "flat_amount": 1000000,
                          "per_unit_price": 1000000,
                          "per_unit_price_decimal": "per_unit_price_decimal",
                          "up_to": 1000000
                        },
                        {
                          "flat_amount": 1000000,
                          "per_unit_price": 1000000,
                          "per_unit_price_decimal": "per_unit_price_decimal",
                          "up_to": 1000000
                        }
                      ],
                      "product_external_id": "product_external_id",
                      "product_id": "product_id",
                      "product_name": "product_name",
                      "provider_type": "orb",
                      "tiers_mode": "graduated",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "usage_type": "licensed"
                    }
                  }
                ]
              },
              "params": {
                "keys": {
                  "keys": "keys"
                }
              }
            }
            """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/usage-by-company").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Entitlements.GetFeatureUsageByCompanyAsync(
            new GetFeatureUsageByCompanyRequest
            {
                Keys = new Dictionary<string, string>() { { "keys", "keys" } },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
