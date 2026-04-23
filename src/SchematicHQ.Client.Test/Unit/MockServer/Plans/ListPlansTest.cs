using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plans;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListPlansTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
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
                  "controlled_by": "orb",
                  "copied_from_plan_id": "copied_from_plan_id",
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
                }
              ],
              "params": {
                "company_id": "company_id",
                "exclude_company_scoped": true,
                "for_fallback_plan": true,
                "for_initial_plan": true,
                "for_trial_expiry_plan": true,
                "has_product_id": true,
                "ids": [
                  "ids"
                ],
                "include_draft_versions": true,
                "limit": 1000000,
                "offset": 1000000,
                "plan_type": "plan",
                "q": "q",
                "scoped_to_company_id": "scoped_to_company_id",
                "without_entitlement_for": "without_entitlement_for",
                "without_paid_product_id": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plans")
                    .WithParam("company_id", "company_id")
                    .WithParam("plan_type", "plan")
                    .WithParam("q", "q")
                    .WithParam("scoped_to_company_id", "scoped_to_company_id")
                    .WithParam("without_entitlement_for", "without_entitlement_for")
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

        var response = await Client.Plans.ListPlansAsync(
            new ListPlansRequest
            {
                CompanyId = "company_id",
                ExcludeCompanyScoped = true,
                ForFallbackPlan = true,
                ForInitialPlan = true,
                ForTrialExpiryPlan = true,
                HasProductId = true,
                Ids = [new List<string>() { "ids" }],
                IncludeDraftVersions = true,
                PlanType = PlanType.Plan,
                Q = "q",
                ScopedToCompanyId = "scoped_to_company_id",
                WithoutEntitlementFor = "without_entitlement_for",
                WithoutPaidProductId = true,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
