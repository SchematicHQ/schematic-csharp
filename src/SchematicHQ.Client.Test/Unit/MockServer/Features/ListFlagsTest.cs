using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Features;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListFlagsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
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
                  "maintainer": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "id": "id",
                    "permissions": {
                      "key": [
                        "billing_credits_edit"
                      ]
                    },
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "maintainer_account_member_id": "maintainer_account_member_id",
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
              "params": {
                "feature_id": "feature_id",
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "q": "q"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/flags")
                    .WithParam("feature_id", "feature_id")
                    .WithParam("q", "q")
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

        var response = await Client.Features.ListFlagsAsync(
            new ListFlagsRequest
            {
                FeatureId = "feature_id",
                Ids = [new List<string>() { "ids" }],
                Q = "q",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
