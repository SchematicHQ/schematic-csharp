using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plangroups;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetPlanGroupTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "add_ons": [
                  {
                    "charge_type": "free",
                    "company_count": 1000000,
                    "compatible_plan_ids": [
                      "compatible_plan_ids"
                    ],
                    "controlled_by": "orb",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
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
                                        "condition_type": "base_plan",
                                        "created_at": "2024-01-15T09:30:00.000Z",
                                        "environment_id": "environment_id",
                                        "id": "id",
                                        "operator": "eq",
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
                                    "condition_type": "base_plan",
                                    "created_at": "2024-01-15T09:30:00.000Z",
                                    "environment_id": "environment_id",
                                    "id": "id",
                                    "operator": "eq",
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
                    "is_custom": true,
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
                "custom_plan_config": {
                  "cta_text": "cta_text",
                  "cta_web_site": "cta_web_site",
                  "price_text": "price_text"
                },
                "custom_plan_id": "custom_plan_id",
                "default_plan": {
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
                  "company_id": "company_id",
                  "company_name": "company_name",
                  "compatible_plan_ids": [
                    "compatible_plan_ids"
                  ],
                  "controlled_by": "orb",
                  "copied_from_plan_id": "copied_from_plan_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency_prices": [
                    {
                      "currency": "currency"
                    }
                  ],
                  "custom_plan_config": {
                    "cta_text": "cta_text",
                    "cta_web_site": "cta_web_site",
                    "price_text": "price_text"
                  },
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
                                      "condition_type": "base_plan",
                                      "created_at": "2024-01-15T09:30:00.000Z",
                                      "environment_id": "environment_id",
                                      "id": "id",
                                      "operator": "eq",
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
                                  "condition_type": "base_plan",
                                  "created_at": "2024-01-15T09:30:00.000Z",
                                  "environment_id": "environment_id",
                                  "id": "id",
                                  "operator": "eq",
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
                  "is_custom": true,
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
                "fallback_plan": {
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
                  "company_id": "company_id",
                  "company_name": "company_name",
                  "compatible_plan_ids": [
                    "compatible_plan_ids"
                  ],
                  "controlled_by": "orb",
                  "copied_from_plan_id": "copied_from_plan_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency_prices": [
                    {
                      "currency": "currency"
                    }
                  ],
                  "custom_plan_config": {
                    "cta_text": "cta_text",
                    "cta_web_site": "cta_web_site",
                    "price_text": "price_text"
                  },
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
                                      "condition_type": "base_plan",
                                      "created_at": "2024-01-15T09:30:00.000Z",
                                      "environment_id": "environment_id",
                                      "id": "id",
                                      "operator": "eq",
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
                                  "condition_type": "base_plan",
                                  "created_at": "2024-01-15T09:30:00.000Z",
                                  "environment_id": "environment_id",
                                  "id": "id",
                                  "operator": "eq",
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
                  "is_custom": true,
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
                "fallback_plan_id": "fallback_plan_id",
                "id": "id",
                "initial_plan": {
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
                  "company_id": "company_id",
                  "company_name": "company_name",
                  "compatible_plan_ids": [
                    "compatible_plan_ids"
                  ],
                  "controlled_by": "orb",
                  "copied_from_plan_id": "copied_from_plan_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency_prices": [
                    {
                      "currency": "currency"
                    }
                  ],
                  "custom_plan_config": {
                    "cta_text": "cta_text",
                    "cta_web_site": "cta_web_site",
                    "price_text": "price_text"
                  },
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
                                      "condition_type": "base_plan",
                                      "created_at": "2024-01-15T09:30:00.000Z",
                                      "environment_id": "environment_id",
                                      "id": "id",
                                      "operator": "eq",
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
                                  "condition_type": "base_plan",
                                  "created_at": "2024-01-15T09:30:00.000Z",
                                  "environment_id": "environment_id",
                                  "id": "id",
                                  "operator": "eq",
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
                  "is_custom": true,
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
                "initial_plan_id": "initial_plan_id",
                "initial_plan_price": {
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
                "initial_plan_price_id": "initial_plan_price_id",
                "ordered_add_on_list": [
                  {
                    "plan_id": "plan_id"
                  }
                ],
                "ordered_bundle_list": [
                  {
                    "bundleId": "bundleId"
                  }
                ],
                "ordered_plan_list": [
                  {
                    "plan_id": "plan_id"
                  }
                ],
                "plans": [
                  {
                    "charge_type": "free",
                    "company_count": 1000000,
                    "compatible_plan_ids": [
                      "compatible_plan_ids"
                    ],
                    "controlled_by": "orb",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
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
                                        "condition_type": "base_plan",
                                        "created_at": "2024-01-15T09:30:00.000Z",
                                        "environment_id": "environment_id",
                                        "id": "id",
                                        "operator": "eq",
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
                                    "condition_type": "base_plan",
                                    "created_at": "2024-01-15T09:30:00.000Z",
                                    "environment_id": "environment_id",
                                    "id": "id",
                                    "operator": "eq",
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
                    "is_custom": true,
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
                "trial_expiry_plan": {
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
                  "company_id": "company_id",
                  "company_name": "company_name",
                  "compatible_plan_ids": [
                    "compatible_plan_ids"
                  ],
                  "controlled_by": "orb",
                  "copied_from_plan_id": "copied_from_plan_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency_prices": [
                    {
                      "currency": "currency"
                    }
                  ],
                  "custom_plan_config": {
                    "cta_text": "cta_text",
                    "cta_web_site": "cta_web_site",
                    "price_text": "price_text"
                  },
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
                                      "condition_type": "base_plan",
                                      "created_at": "2024-01-15T09:30:00.000Z",
                                      "environment_id": "environment_id",
                                      "id": "id",
                                      "operator": "eq",
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
                                  "condition_type": "base_plan",
                                  "created_at": "2024-01-15T09:30:00.000Z",
                                  "environment_id": "environment_id",
                                  "id": "id",
                                  "operator": "eq",
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
                  "is_custom": true,
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
                "trial_expiry_plan_id": "trial_expiry_plan_id",
                "trial_expiry_plan_price": {
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
                "trial_expiry_plan_price_id": "trial_expiry_plan_price_id",
                "trial_payment_method_required": true
              },
              "params": {
                "include_company_counts": true
              }
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/plan-groups").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Plangroups.GetPlanGroupAsync(
            new GetPlanGroupRequest { IncludeCompanyCounts = true }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
