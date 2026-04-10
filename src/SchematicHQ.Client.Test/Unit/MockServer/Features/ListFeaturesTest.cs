using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Features;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListFeaturesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
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
                    "user_count": 1000000
                  },
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
                  "lifecycle_phase": "add_on",
                  "maintainer_id": "maintainer_id",
                  "name": "name",
                  "plans": [
                    {
                      "id": "id",
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
                      "hierarchy"
                    ],
                    "id": "id",
                    "trait_type": "boolean",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "trait_id": "trait_id",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "boolean_require_event": true,
                "feature_type": [
                  "boolean"
                ],
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "plan_version_id": "plan_version_id",
                "q": "q",
                "without_company_override_for": "without_company_override_for",
                "without_plan_entitlement_for": "without_plan_entitlement_for"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/features")
                    .WithParam("plan_version_id", "plan_version_id")
                    .WithParam("q", "q")
                    .WithParam("without_company_override_for", "without_company_override_for")
                    .WithParam("without_plan_entitlement_for", "without_plan_entitlement_for")
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

        var response = await Client.Features.ListFeaturesAsync(
            new ListFeaturesRequest
            {
                BooleanRequireEvent = true,
                PlanVersionId = "plan_version_id",
                Q = "q",
                WithoutCompanyOverrideFor = "without_company_override_for",
                WithoutPlanEntitlementFor = "without_plan_entitlement_for",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
