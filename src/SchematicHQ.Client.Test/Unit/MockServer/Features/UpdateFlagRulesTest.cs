using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Features;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateFlagRulesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "rules": [
                {
                  "condition_groups": [
                    {
                      "conditions": [
                        {
                          "condition_type": "company",
                          "operator": "eq",
                          "resource_ids": [
                            "resource_ids"
                          ]
                        }
                      ]
                    }
                  ],
                  "conditions": [
                    {
                      "condition_type": "company",
                      "operator": "eq",
                      "resource_ids": [
                        "resource_ids"
                      ]
                    }
                  ],
                  "name": "name",
                  "priority": 1000000,
                  "value": true
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "flag": {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "default_value": true,
                  "description": "description",
                  "feature_id": "feature_id",
                  "flag_type": "boolean",
                  "id": "id",
                  "key": "key",
                  "maintainer_id": "maintainer_id",
                  "name": "name",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
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
                ]
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
                    .WithPath("/flags/flag_id/rules")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Features.UpdateFlagRulesAsync(
            "flag_id",
            new UpdateFlagRulesRequestBody
            {
                Rules = new List<CreateOrUpdateRuleRequestBody>()
                {
                    new CreateOrUpdateRuleRequestBody
                    {
                        ConditionGroups = new List<CreateOrUpdateConditionGroupRequestBody>()
                        {
                            new CreateOrUpdateConditionGroupRequestBody
                            {
                                Conditions = new List<CreateOrUpdateConditionRequestBody>()
                                {
                                    new CreateOrUpdateConditionRequestBody
                                    {
                                        ConditionType =
                                            CreateOrUpdateConditionRequestBodyConditionType.Company,
                                        Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                                        ResourceIds = new List<string>() { "resource_ids" },
                                    },
                                },
                            },
                        },
                        Conditions = new List<CreateOrUpdateConditionRequestBody>()
                        {
                            new CreateOrUpdateConditionRequestBody
                            {
                                ConditionType =
                                    CreateOrUpdateConditionRequestBodyConditionType.Company,
                                Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                                ResourceIds = new List<string>() { "resource_ids" },
                            },
                        },
                        Name = "name",
                        Priority = 1000000,
                        Value = true,
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
