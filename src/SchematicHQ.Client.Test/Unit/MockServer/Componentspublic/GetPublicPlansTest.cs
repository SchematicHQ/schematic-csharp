using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Componentspublic;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetPublicPlansTest : BaseMockServerTest
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
                    "company_count": 1000000,
                    "compatible_plan_ids": [
                      "compatible_plan_ids"
                    ],
                    "controlled_by": "controlled_by",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
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
                    "icon": "amber",
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
                "active_plans": [
                  {
                    "charge_type": "free",
                    "company_count": 1000000,
                    "compatible_plan_ids": [
                      "compatible_plan_ids"
                    ],
                    "controlled_by": "controlled_by",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency_prices": [
                      {
                        "currency": "currency"
                      }
                    ],
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
                    "icon": "amber",
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
                "display_settings": {
                  "show_as_monthly_prices": true,
                  "show_credits": true,
                  "show_feature_description": true,
                  "show_hard_limit": true,
                  "show_period_toggle": true,
                  "show_zero_price_as_free": true
                },
                "show_as_monthly_prices": true,
                "show_credits": true,
                "show_period_toggle": true,
                "show_zero_price_as_free": true
              },
              "params": {
                "key": "value"
              }
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/public/plans").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Componentspublic.GetPublicPlansAsync();
        JsonAssert.AreEqual(response, mockResponse);
    }
}
