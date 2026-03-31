using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plans;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreatePlanTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "description": "description",
              "name": "name",
              "plan_type": "plan"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "active_version": {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "description": "description",
                  "environment_id": "environment_id",
                  "icon": "icon",
                  "id": "id",
                  "name": "name",
                  "original_plan_id": "original_plan_id",
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
                  "price_decimal": "price_decimal",
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
                  "original_plan_id": "original_plan_id",
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
                  "price_decimal": "price_decimal",
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
                  "price_decimal": "price_decimal",
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
                  "price_decimal": "price_decimal",
                  "provider_type": "schematic",
                  "scheme": "per_unit"
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
                    .WithPath("/plans")
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

        var response = await Client.Plans.CreatePlanAsync(
            new CreatePlanRequestBody
            {
                Description = "description",
                Name = "name",
                PlanType = PlanType.Plan,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
