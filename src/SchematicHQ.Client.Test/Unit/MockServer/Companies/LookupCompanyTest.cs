using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class LookupCompanyTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "add_ons": [
                  {
                    "added_on": "2024-01-15T09:30:00.000Z",
                    "billing_product_external_id": "billing_product_external_id",
                    "billing_product_id": "billing_product_id",
                    "description": "description",
                    "id": "id",
                    "image_url": "image_url",
                    "included_credit_grants": [
                      {
                        "billing_credit_auto_topup_amount": 1000000,
                        "billing_credit_auto_topup_amount_type": "billing_credit_auto_topup_amount_type",
                        "billing_credit_auto_topup_enabled": true,
                        "billing_credit_auto_topup_expiry_type": "duration",
                        "billing_credit_auto_topup_expiry_unit": "billing_periods",
                        "billing_credit_auto_topup_expiry_unit_count": 1000000,
                        "billing_credit_auto_topup_threshold_percent": 1000000,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "credit": {
                          "account_id": "account_id",
                          "burn_strategy": "expiration_priority",
                          "cost_editable": true,
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "default_expiry_unit": "billing_periods",
                          "default_expiry_unit_count": 1000000,
                          "default_rollover_policy": "expire",
                          "description": "description",
                          "environment_id": "environment_id",
                          "icon": "icon",
                          "id": "id",
                          "name": "name",
                          "plural_name": "plural_name",
                          "price": {
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
                            "provider_type": "schematic",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          },
                          "price_per_unit": 1000000,
                          "price_per_unit_decimal": "price_per_unit_decimal",
                          "product": {
                            "account_id": "account_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "environment_id": "environment_id",
                            "external_id": "external_id",
                            "is_active": true,
                            "name": "name",
                            "price": 1.1,
                            "product_id": "product_id",
                            "provider_type": "schematic",
                            "quantity": 1.1,
                            "updated_at": "2024-01-15T09:30:00.000Z"
                          },
                          "singular_name": "singular_name",
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
                        "credit_amount": 1000000,
                        "credit_description": "credit_description",
                        "credit_icon": "credit_icon",
                        "credit_id": "credit_id",
                        "credit_name": "credit_name",
                        "expiry_type": "duration",
                        "expiry_unit": "billing_periods",
                        "expiry_unit_count": 1000000,
                        "id": "id",
                        "plan": {
                          "description": "description",
                          "id": "id",
                          "image_url": "image_url",
                          "name": "name"
                        },
                        "plan_id": "plan_id",
                        "plan_version_id": "plan_version_id",
                        "plural_name": "plural_name",
                        "reset_cadence": "daily",
                        "reset_start": "billing_period",
                        "reset_type": "no_reset",
                        "singular_name": "singular_name",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      {
                        "billing_credit_auto_topup_amount": 1000000,
                        "billing_credit_auto_topup_amount_type": "billing_credit_auto_topup_amount_type",
                        "billing_credit_auto_topup_enabled": true,
                        "billing_credit_auto_topup_expiry_type": "duration",
                        "billing_credit_auto_topup_expiry_unit": "billing_periods",
                        "billing_credit_auto_topup_expiry_unit_count": 1000000,
                        "billing_credit_auto_topup_threshold_percent": 1000000,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "credit": {
                          "account_id": "account_id",
                          "burn_strategy": "expiration_priority",
                          "cost_editable": true,
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "default_expiry_unit": "billing_periods",
                          "default_expiry_unit_count": 1000000,
                          "default_rollover_policy": "expire",
                          "description": "description",
                          "environment_id": "environment_id",
                          "icon": "icon",
                          "id": "id",
                          "name": "name",
                          "plural_name": "plural_name",
                          "price": {
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
                            "provider_type": "schematic",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          },
                          "price_per_unit": 1000000,
                          "price_per_unit_decimal": "price_per_unit_decimal",
                          "product": {
                            "account_id": "account_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "environment_id": "environment_id",
                            "external_id": "external_id",
                            "is_active": true,
                            "name": "name",
                            "price": 1.1,
                            "product_id": "product_id",
                            "provider_type": "schematic",
                            "quantity": 1.1,
                            "updated_at": "2024-01-15T09:30:00.000Z"
                          },
                          "singular_name": "singular_name",
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
                        "credit_amount": 1000000,
                        "credit_description": "credit_description",
                        "credit_icon": "credit_icon",
                        "credit_id": "credit_id",
                        "credit_name": "credit_name",
                        "expiry_type": "duration",
                        "expiry_unit": "billing_periods",
                        "expiry_unit_count": 1000000,
                        "id": "id",
                        "plan": {
                          "description": "description",
                          "id": "id",
                          "image_url": "image_url",
                          "name": "name"
                        },
                        "plan_id": "plan_id",
                        "plan_version_id": "plan_version_id",
                        "plural_name": "plural_name",
                        "reset_cadence": "daily",
                        "reset_start": "billing_period",
                        "reset_type": "no_reset",
                        "singular_name": "singular_name",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      }
                    ],
                    "name": "name",
                    "plan_period": "plan_period",
                    "plan_price": 1000000,
                    "plan_version_id": "plan_version_id"
                  },
                  {
                    "added_on": "2024-01-15T09:30:00.000Z",
                    "billing_product_external_id": "billing_product_external_id",
                    "billing_product_id": "billing_product_id",
                    "description": "description",
                    "id": "id",
                    "image_url": "image_url",
                    "included_credit_grants": [
                      {
                        "billing_credit_auto_topup_amount": 1000000,
                        "billing_credit_auto_topup_amount_type": "billing_credit_auto_topup_amount_type",
                        "billing_credit_auto_topup_enabled": true,
                        "billing_credit_auto_topup_expiry_type": "duration",
                        "billing_credit_auto_topup_expiry_unit": "billing_periods",
                        "billing_credit_auto_topup_expiry_unit_count": 1000000,
                        "billing_credit_auto_topup_threshold_percent": 1000000,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "credit": {
                          "account_id": "account_id",
                          "burn_strategy": "expiration_priority",
                          "cost_editable": true,
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "default_expiry_unit": "billing_periods",
                          "default_expiry_unit_count": 1000000,
                          "default_rollover_policy": "expire",
                          "description": "description",
                          "environment_id": "environment_id",
                          "icon": "icon",
                          "id": "id",
                          "name": "name",
                          "plural_name": "plural_name",
                          "price": {
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
                            "provider_type": "schematic",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          },
                          "price_per_unit": 1000000,
                          "price_per_unit_decimal": "price_per_unit_decimal",
                          "product": {
                            "account_id": "account_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "environment_id": "environment_id",
                            "external_id": "external_id",
                            "is_active": true,
                            "name": "name",
                            "price": 1.1,
                            "product_id": "product_id",
                            "provider_type": "schematic",
                            "quantity": 1.1,
                            "updated_at": "2024-01-15T09:30:00.000Z"
                          },
                          "singular_name": "singular_name",
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
                        "credit_amount": 1000000,
                        "credit_description": "credit_description",
                        "credit_icon": "credit_icon",
                        "credit_id": "credit_id",
                        "credit_name": "credit_name",
                        "expiry_type": "duration",
                        "expiry_unit": "billing_periods",
                        "expiry_unit_count": 1000000,
                        "id": "id",
                        "plan": {
                          "description": "description",
                          "id": "id",
                          "image_url": "image_url",
                          "name": "name"
                        },
                        "plan_id": "plan_id",
                        "plan_version_id": "plan_version_id",
                        "plural_name": "plural_name",
                        "reset_cadence": "daily",
                        "reset_start": "billing_period",
                        "reset_type": "no_reset",
                        "singular_name": "singular_name",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      {
                        "billing_credit_auto_topup_amount": 1000000,
                        "billing_credit_auto_topup_amount_type": "billing_credit_auto_topup_amount_type",
                        "billing_credit_auto_topup_enabled": true,
                        "billing_credit_auto_topup_expiry_type": "duration",
                        "billing_credit_auto_topup_expiry_unit": "billing_periods",
                        "billing_credit_auto_topup_expiry_unit_count": 1000000,
                        "billing_credit_auto_topup_threshold_percent": 1000000,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "credit": {
                          "account_id": "account_id",
                          "burn_strategy": "expiration_priority",
                          "cost_editable": true,
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "default_expiry_unit": "billing_periods",
                          "default_expiry_unit_count": 1000000,
                          "default_rollover_policy": "expire",
                          "description": "description",
                          "environment_id": "environment_id",
                          "icon": "icon",
                          "id": "id",
                          "name": "name",
                          "plural_name": "plural_name",
                          "price": {
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
                            "provider_type": "schematic",
                            "updated_at": "2024-01-15T09:30:00.000Z",
                            "usage_type": "licensed"
                          },
                          "price_per_unit": 1000000,
                          "price_per_unit_decimal": "price_per_unit_decimal",
                          "product": {
                            "account_id": "account_id",
                            "created_at": "2024-01-15T09:30:00.000Z",
                            "environment_id": "environment_id",
                            "external_id": "external_id",
                            "is_active": true,
                            "name": "name",
                            "price": 1.1,
                            "product_id": "product_id",
                            "provider_type": "schematic",
                            "quantity": 1.1,
                            "updated_at": "2024-01-15T09:30:00.000Z"
                          },
                          "singular_name": "singular_name",
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
                        "credit_amount": 1000000,
                        "credit_description": "credit_description",
                        "credit_icon": "credit_icon",
                        "credit_id": "credit_id",
                        "credit_name": "credit_name",
                        "expiry_type": "duration",
                        "expiry_unit": "billing_periods",
                        "expiry_unit_count": 1000000,
                        "id": "id",
                        "plan": {
                          "description": "description",
                          "id": "id",
                          "image_url": "image_url",
                          "name": "name"
                        },
                        "plan_id": "plan_id",
                        "plan_version_id": "plan_version_id",
                        "plural_name": "plural_name",
                        "reset_cadence": "daily",
                        "reset_start": "billing_period",
                        "reset_type": "no_reset",
                        "singular_name": "singular_name",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      }
                    ],
                    "name": "name",
                    "plan_period": "plan_period",
                    "plan_price": 1000000,
                    "plan_version_id": "plan_version_id"
                  }
                ],
                "billing_credit_balances": {
                  "billing_credit_balances": 1.1
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
                      "amount_off": 1000000,
                      "coupon_id": "coupon_id",
                      "coupon_name": "coupon_name",
                      "currency": "currency",
                      "customer_facing_code": "customer_facing_code",
                      "discount_external_id": "discount_external_id",
                      "duration": "duration",
                      "duration_in_months": 1000000,
                      "ended_at": "2024-01-15T09:30:00.000Z",
                      "is_active": true,
                      "percent_off": 1.1,
                      "promo_code_external_id": "promo_code_external_id",
                      "started_at": "2024-01-15T09:30:00.000Z",
                      "subscription_external_id": "subscription_external_id"
                    },
                    {
                      "amount_off": 1000000,
                      "coupon_id": "coupon_id",
                      "coupon_name": "coupon_name",
                      "currency": "currency",
                      "customer_facing_code": "customer_facing_code",
                      "discount_external_id": "discount_external_id",
                      "duration": "duration",
                      "duration_in_months": 1000000,
                      "ended_at": "2024-01-15T09:30:00.000Z",
                      "is_active": true,
                      "percent_off": 1.1,
                      "promo_code_external_id": "promo_code_external_id",
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
                  },
                  "metadata": {
                    "metadata": {
                      "key": "value"
                    }
                  },
                  "payment_method": {
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
                  "period_end": 1000000,
                  "period_start": 1000000,
                  "products": [
                    {
                      "billing_scheme": "per_unit",
                      "billing_threshold": 1000000,
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "currency": "currency",
                      "environment_id": "environment_id",
                      "external_id": "external_id",
                      "id": "id",
                      "interval": "interval",
                      "meter_id": "meter_id",
                      "name": "name",
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
                      "provider_type": "schematic",
                      "quantity": 1.1,
                      "subscription_id": "subscription_id",
                      "subscription_item_external_id": "subscription_item_external_id",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "usage_type": "licensed"
                    },
                    {
                      "billing_scheme": "per_unit",
                      "billing_threshold": 1000000,
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "currency": "currency",
                      "environment_id": "environment_id",
                      "external_id": "external_id",
                      "id": "id",
                      "interval": "interval",
                      "meter_id": "meter_id",
                      "name": "name",
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
                      "provider_type": "schematic",
                      "quantity": 1.1,
                      "subscription_id": "subscription_id",
                      "subscription_item_external_id": "subscription_item_external_id",
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
                        "amount_off": 1000000,
                        "coupon_id": "coupon_id",
                        "coupon_name": "coupon_name",
                        "currency": "currency",
                        "customer_facing_code": "customer_facing_code",
                        "discount_external_id": "discount_external_id",
                        "duration": "duration",
                        "duration_in_months": 1000000,
                        "ended_at": "2024-01-15T09:30:00.000Z",
                        "is_active": true,
                        "percent_off": 1.1,
                        "promo_code_external_id": "promo_code_external_id",
                        "started_at": "2024-01-15T09:30:00.000Z",
                        "subscription_external_id": "subscription_external_id"
                      },
                      {
                        "amount_off": 1000000,
                        "coupon_id": "coupon_id",
                        "coupon_name": "coupon_name",
                        "currency": "currency",
                        "customer_facing_code": "customer_facing_code",
                        "discount_external_id": "discount_external_id",
                        "duration": "duration",
                        "duration_in_months": 1000000,
                        "ended_at": "2024-01-15T09:30:00.000Z",
                        "is_active": true,
                        "percent_off": 1.1,
                        "promo_code_external_id": "promo_code_external_id",
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
                    },
                    "metadata": {
                      "metadata": {
                        "key": "value"
                      }
                    },
                    "payment_method": {
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
                    "period_end": 1000000,
                    "period_start": 1000000,
                    "products": [
                      {
                        "billing_scheme": "per_unit",
                        "billing_threshold": 1000000,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "currency": "currency",
                        "environment_id": "environment_id",
                        "external_id": "external_id",
                        "id": "id",
                        "interval": "interval",
                        "meter_id": "meter_id",
                        "name": "name",
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
                        "provider_type": "schematic",
                        "quantity": 1.1,
                        "subscription_id": "subscription_id",
                        "subscription_item_external_id": "subscription_item_external_id",
                        "updated_at": "2024-01-15T09:30:00.000Z",
                        "usage_type": "licensed"
                      },
                      {
                        "billing_scheme": "per_unit",
                        "billing_threshold": 1000000,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "currency": "currency",
                        "environment_id": "environment_id",
                        "external_id": "external_id",
                        "id": "id",
                        "interval": "interval",
                        "meter_id": "meter_id",
                        "name": "name",
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
                        "provider_type": "schematic",
                        "quantity": 1.1,
                        "subscription_id": "subscription_id",
                        "subscription_item_external_id": "subscription_item_external_id",
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
                  {
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
                        "amount_off": 1000000,
                        "coupon_id": "coupon_id",
                        "coupon_name": "coupon_name",
                        "currency": "currency",
                        "customer_facing_code": "customer_facing_code",
                        "discount_external_id": "discount_external_id",
                        "duration": "duration",
                        "duration_in_months": 1000000,
                        "ended_at": "2024-01-15T09:30:00.000Z",
                        "is_active": true,
                        "percent_off": 1.1,
                        "promo_code_external_id": "promo_code_external_id",
                        "started_at": "2024-01-15T09:30:00.000Z",
                        "subscription_external_id": "subscription_external_id"
                      },
                      {
                        "amount_off": 1000000,
                        "coupon_id": "coupon_id",
                        "coupon_name": "coupon_name",
                        "currency": "currency",
                        "customer_facing_code": "customer_facing_code",
                        "discount_external_id": "discount_external_id",
                        "duration": "duration",
                        "duration_in_months": 1000000,
                        "ended_at": "2024-01-15T09:30:00.000Z",
                        "is_active": true,
                        "percent_off": 1.1,
                        "promo_code_external_id": "promo_code_external_id",
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
                    },
                    "metadata": {
                      "metadata": {
                        "key": "value"
                      }
                    },
                    "payment_method": {
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
                    "period_end": 1000000,
                    "period_start": 1000000,
                    "products": [
                      {
                        "billing_scheme": "per_unit",
                        "billing_threshold": 1000000,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "currency": "currency",
                        "environment_id": "environment_id",
                        "external_id": "external_id",
                        "id": "id",
                        "interval": "interval",
                        "meter_id": "meter_id",
                        "name": "name",
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
                        "provider_type": "schematic",
                        "quantity": 1.1,
                        "subscription_id": "subscription_id",
                        "subscription_item_external_id": "subscription_item_external_id",
                        "updated_at": "2024-01-15T09:30:00.000Z",
                        "usage_type": "licensed"
                      },
                      {
                        "billing_scheme": "per_unit",
                        "billing_threshold": 1000000,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "currency": "currency",
                        "environment_id": "environment_id",
                        "external_id": "external_id",
                        "id": "id",
                        "interval": "interval",
                        "meter_id": "meter_id",
                        "name": "name",
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
                        "provider_type": "schematic",
                        "quantity": 1.1,
                        "subscription_id": "subscription_id",
                        "subscription_item_external_id": "subscription_item_external_id",
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
                    "allocation": 1000000,
                    "credit_id": "credit_id",
                    "credit_remaining": 1.1,
                    "credit_total": 1.1,
                    "credit_used": 1.1,
                    "event_name": "event_name",
                    "feature_id": "feature_id",
                    "feature_key": "feature_key",
                    "metric_period": "all_time",
                    "metric_reset_at": "2024-01-15T09:30:00.000Z",
                    "month_reset": "first_of_month",
                    "soft_limit": 1000000,
                    "usage": 1000000,
                    "value_type": "boolean"
                  },
                  {
                    "allocation": 1000000,
                    "credit_id": "credit_id",
                    "credit_remaining": 1.1,
                    "credit_total": 1.1,
                    "credit_used": 1.1,
                    "event_name": "event_name",
                    "feature_id": "feature_id",
                    "feature_key": "feature_key",
                    "metric_period": "all_time",
                    "metric_reset_at": "2024-01-15T09:30:00.000Z",
                    "month_reset": "first_of_month",
                    "soft_limit": 1000000,
                    "usage": 1000000,
                    "value_type": "boolean"
                  }
                ],
                "entity_traits": [
                  {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "definition": {
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
                    "definition_id": "definition_id",
                    "environment_id": "environment_id",
                    "id": "id",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "value": "value"
                  },
                  {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "definition": {
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
                    "definition": {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "entity_type": "company",
                      "id": "id",
                      "key": "key",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
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
                    "definition": {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "entity_type": "company",
                      "id": "id",
                      "key": "key",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
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
                    "valid_until": "2024-01-15T09:30:00.000Z",
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
                    "month_reset": "month_reset",
                    "period": "period",
                    "valid_until": "2024-01-15T09:30:00.000Z",
                    "value": 1000000
                  }
                ],
                "name": "name",
                "payment_methods": [
                  {
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
                  {
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
                      "billing_credit_auto_topup_amount": 1000000,
                      "billing_credit_auto_topup_amount_type": "billing_credit_auto_topup_amount_type",
                      "billing_credit_auto_topup_enabled": true,
                      "billing_credit_auto_topup_expiry_type": "duration",
                      "billing_credit_auto_topup_expiry_unit": "billing_periods",
                      "billing_credit_auto_topup_expiry_unit_count": 1000000,
                      "billing_credit_auto_topup_threshold_percent": 1000000,
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "credit": {
                        "account_id": "account_id",
                        "burn_strategy": "expiration_priority",
                        "cost_editable": true,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "default_expiry_unit": "billing_periods",
                        "default_expiry_unit_count": 1000000,
                        "default_rollover_policy": "expire",
                        "description": "description",
                        "environment_id": "environment_id",
                        "icon": "icon",
                        "id": "id",
                        "name": "name",
                        "plural_name": "plural_name",
                        "price": {
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
                          "provider_type": "schematic",
                          "updated_at": "2024-01-15T09:30:00.000Z",
                          "usage_type": "licensed"
                        },
                        "price_per_unit": 1000000,
                        "price_per_unit_decimal": "price_per_unit_decimal",
                        "product": {
                          "account_id": "account_id",
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "environment_id": "environment_id",
                          "external_id": "external_id",
                          "is_active": true,
                          "name": "name",
                          "price": 1.1,
                          "product_id": "product_id",
                          "provider_type": "schematic",
                          "quantity": 1.1,
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
                        "singular_name": "singular_name",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "credit_amount": 1000000,
                      "credit_description": "credit_description",
                      "credit_icon": "credit_icon",
                      "credit_id": "credit_id",
                      "credit_name": "credit_name",
                      "expiry_type": "duration",
                      "expiry_unit": "billing_periods",
                      "expiry_unit_count": 1000000,
                      "id": "id",
                      "plan": {
                        "description": "description",
                        "id": "id",
                        "image_url": "image_url",
                        "name": "name"
                      },
                      "plan_id": "plan_id",
                      "plan_version_id": "plan_version_id",
                      "plural_name": "plural_name",
                      "reset_cadence": "daily",
                      "reset_start": "billing_period",
                      "reset_type": "no_reset",
                      "singular_name": "singular_name",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
                    {
                      "billing_credit_auto_topup_amount": 1000000,
                      "billing_credit_auto_topup_amount_type": "billing_credit_auto_topup_amount_type",
                      "billing_credit_auto_topup_enabled": true,
                      "billing_credit_auto_topup_expiry_type": "duration",
                      "billing_credit_auto_topup_expiry_unit": "billing_periods",
                      "billing_credit_auto_topup_expiry_unit_count": 1000000,
                      "billing_credit_auto_topup_threshold_percent": 1000000,
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "credit": {
                        "account_id": "account_id",
                        "burn_strategy": "expiration_priority",
                        "cost_editable": true,
                        "created_at": "2024-01-15T09:30:00.000Z",
                        "default_expiry_unit": "billing_periods",
                        "default_expiry_unit_count": 1000000,
                        "default_rollover_policy": "expire",
                        "description": "description",
                        "environment_id": "environment_id",
                        "icon": "icon",
                        "id": "id",
                        "name": "name",
                        "plural_name": "plural_name",
                        "price": {
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
                          "provider_type": "schematic",
                          "updated_at": "2024-01-15T09:30:00.000Z",
                          "usage_type": "licensed"
                        },
                        "price_per_unit": 1000000,
                        "price_per_unit_decimal": "price_per_unit_decimal",
                        "product": {
                          "account_id": "account_id",
                          "created_at": "2024-01-15T09:30:00.000Z",
                          "environment_id": "environment_id",
                          "external_id": "external_id",
                          "is_active": true,
                          "name": "name",
                          "price": 1.1,
                          "product_id": "product_id",
                          "provider_type": "schematic",
                          "quantity": 1.1,
                          "updated_at": "2024-01-15T09:30:00.000Z"
                        },
                        "singular_name": "singular_name",
                        "updated_at": "2024-01-15T09:30:00.000Z"
                      },
                      "credit_amount": 1000000,
                      "credit_description": "credit_description",
                      "credit_icon": "credit_icon",
                      "credit_id": "credit_id",
                      "credit_name": "credit_name",
                      "expiry_type": "duration",
                      "expiry_unit": "billing_periods",
                      "expiry_unit_count": 1000000,
                      "id": "id",
                      "plan": {
                        "description": "description",
                        "id": "id",
                        "image_url": "image_url",
                        "name": "name"
                      },
                      "plan_id": "plan_id",
                      "plan_version_id": "plan_version_id",
                      "plural_name": "plural_name",
                      "reset_cadence": "daily",
                      "reset_start": "billing_period",
                      "reset_type": "no_reset",
                      "singular_name": "singular_name",
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
                "rules": [
                  {
                    "account_id": "account_id",
                    "condition_groups": [
                      {
                        "conditions": [
                          {
                            "account_id": "account_id",
                            "comparison_trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "condition_type": "base_plan",
                            "consumption_rate": 1.1,
                            "credit_id": "credit_id",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "id": "id",
                            "metric_period": "all_time",
                            "metric_period_month_reset": "first_of_month",
                            "metric_value": 1000000,
                            "operator": "eq",
                            "resource_ids": [
                              "resource_ids",
                              "resource_ids"
                            ],
                            "trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "trait_value": "trait_value"
                          },
                          {
                            "account_id": "account_id",
                            "comparison_trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "condition_type": "base_plan",
                            "consumption_rate": 1.1,
                            "credit_id": "credit_id",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "id": "id",
                            "metric_period": "all_time",
                            "metric_period_month_reset": "first_of_month",
                            "metric_value": 1000000,
                            "operator": "eq",
                            "resource_ids": [
                              "resource_ids",
                              "resource_ids"
                            ],
                            "trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "trait_value": "trait_value"
                          }
                        ]
                      },
                      {
                        "conditions": [
                          {
                            "account_id": "account_id",
                            "comparison_trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "condition_type": "base_plan",
                            "consumption_rate": 1.1,
                            "credit_id": "credit_id",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "id": "id",
                            "metric_period": "all_time",
                            "metric_period_month_reset": "first_of_month",
                            "metric_value": 1000000,
                            "operator": "eq",
                            "resource_ids": [
                              "resource_ids",
                              "resource_ids"
                            ],
                            "trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "trait_value": "trait_value"
                          },
                          {
                            "account_id": "account_id",
                            "comparison_trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "condition_type": "base_plan",
                            "consumption_rate": 1.1,
                            "credit_id": "credit_id",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "id": "id",
                            "metric_period": "all_time",
                            "metric_period_month_reset": "first_of_month",
                            "metric_value": 1000000,
                            "operator": "eq",
                            "resource_ids": [
                              "resource_ids",
                              "resource_ids"
                            ],
                            "trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "trait_value": "trait_value"
                          }
                        ]
                      }
                    ],
                    "conditions": [
                      {
                        "account_id": "account_id",
                        "comparison_trait_definition": {
                          "comparable_type": "bool",
                          "entity_type": "company",
                          "id": "id"
                        },
                        "condition_type": "base_plan",
                        "consumption_rate": 1.1,
                        "credit_id": "credit_id",
                        "environment_id": "environment_id",
                        "event_subtype": "event_subtype",
                        "id": "id",
                        "metric_period": "all_time",
                        "metric_period_month_reset": "first_of_month",
                        "metric_value": 1000000,
                        "operator": "eq",
                        "resource_ids": [
                          "resource_ids",
                          "resource_ids"
                        ],
                        "trait_definition": {
                          "comparable_type": "bool",
                          "entity_type": "company",
                          "id": "id"
                        },
                        "trait_value": "trait_value"
                      },
                      {
                        "account_id": "account_id",
                        "comparison_trait_definition": {
                          "comparable_type": "bool",
                          "entity_type": "company",
                          "id": "id"
                        },
                        "condition_type": "base_plan",
                        "consumption_rate": 1.1,
                        "credit_id": "credit_id",
                        "environment_id": "environment_id",
                        "event_subtype": "event_subtype",
                        "id": "id",
                        "metric_period": "all_time",
                        "metric_period_month_reset": "first_of_month",
                        "metric_value": 1000000,
                        "operator": "eq",
                        "resource_ids": [
                          "resource_ids",
                          "resource_ids"
                        ],
                        "trait_definition": {
                          "comparable_type": "bool",
                          "entity_type": "company",
                          "id": "id"
                        },
                        "trait_value": "trait_value"
                      }
                    ],
                    "environment_id": "environment_id",
                    "flag_id": "flag_id",
                    "id": "id",
                    "name": "name",
                    "priority": 1000000,
                    "rule_type": "default",
                    "value": true
                  },
                  {
                    "account_id": "account_id",
                    "condition_groups": [
                      {
                        "conditions": [
                          {
                            "account_id": "account_id",
                            "comparison_trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "condition_type": "base_plan",
                            "consumption_rate": 1.1,
                            "credit_id": "credit_id",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "id": "id",
                            "metric_period": "all_time",
                            "metric_period_month_reset": "first_of_month",
                            "metric_value": 1000000,
                            "operator": "eq",
                            "resource_ids": [
                              "resource_ids",
                              "resource_ids"
                            ],
                            "trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "trait_value": "trait_value"
                          },
                          {
                            "account_id": "account_id",
                            "comparison_trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "condition_type": "base_plan",
                            "consumption_rate": 1.1,
                            "credit_id": "credit_id",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "id": "id",
                            "metric_period": "all_time",
                            "metric_period_month_reset": "first_of_month",
                            "metric_value": 1000000,
                            "operator": "eq",
                            "resource_ids": [
                              "resource_ids",
                              "resource_ids"
                            ],
                            "trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "trait_value": "trait_value"
                          }
                        ]
                      },
                      {
                        "conditions": [
                          {
                            "account_id": "account_id",
                            "comparison_trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "condition_type": "base_plan",
                            "consumption_rate": 1.1,
                            "credit_id": "credit_id",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "id": "id",
                            "metric_period": "all_time",
                            "metric_period_month_reset": "first_of_month",
                            "metric_value": 1000000,
                            "operator": "eq",
                            "resource_ids": [
                              "resource_ids",
                              "resource_ids"
                            ],
                            "trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "trait_value": "trait_value"
                          },
                          {
                            "account_id": "account_id",
                            "comparison_trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "condition_type": "base_plan",
                            "consumption_rate": 1.1,
                            "credit_id": "credit_id",
                            "environment_id": "environment_id",
                            "event_subtype": "event_subtype",
                            "id": "id",
                            "metric_period": "all_time",
                            "metric_period_month_reset": "first_of_month",
                            "metric_value": 1000000,
                            "operator": "eq",
                            "resource_ids": [
                              "resource_ids",
                              "resource_ids"
                            ],
                            "trait_definition": {
                              "comparable_type": "bool",
                              "entity_type": "company",
                              "id": "id"
                            },
                            "trait_value": "trait_value"
                          }
                        ]
                      }
                    ],
                    "conditions": [
                      {
                        "account_id": "account_id",
                        "comparison_trait_definition": {
                          "comparable_type": "bool",
                          "entity_type": "company",
                          "id": "id"
                        },
                        "condition_type": "base_plan",
                        "consumption_rate": 1.1,
                        "credit_id": "credit_id",
                        "environment_id": "environment_id",
                        "event_subtype": "event_subtype",
                        "id": "id",
                        "metric_period": "all_time",
                        "metric_period_month_reset": "first_of_month",
                        "metric_value": 1000000,
                        "operator": "eq",
                        "resource_ids": [
                          "resource_ids",
                          "resource_ids"
                        ],
                        "trait_definition": {
                          "comparable_type": "bool",
                          "entity_type": "company",
                          "id": "id"
                        },
                        "trait_value": "trait_value"
                      },
                      {
                        "account_id": "account_id",
                        "comparison_trait_definition": {
                          "comparable_type": "bool",
                          "entity_type": "company",
                          "id": "id"
                        },
                        "condition_type": "base_plan",
                        "consumption_rate": 1.1,
                        "credit_id": "credit_id",
                        "environment_id": "environment_id",
                        "event_subtype": "event_subtype",
                        "id": "id",
                        "metric_period": "all_time",
                        "metric_period_month_reset": "first_of_month",
                        "metric_value": 1000000,
                        "operator": "eq",
                        "resource_ids": [
                          "resource_ids",
                          "resource_ids"
                        ],
                        "trait_definition": {
                          "comparable_type": "bool",
                          "entity_type": "company",
                          "id": "id"
                        },
                        "trait_value": "trait_value"
                      }
                    ],
                    "environment_id": "environment_id",
                    "flag_id": "flag_id",
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
                  "traits": {
                    "key": "value"
                  }
                },
                "updated_at": "2024-01-15T09:30:00.000Z",
                "user_count": 1000000
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
                WireMock.RequestBuilders.Request.Create().WithPath("/companies/lookup").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Companies.LookupCompanyAsync(
            new LookupCompanyRequest
            {
                Keys = new Dictionary<string, string>() { { "keys", "keys" } },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
