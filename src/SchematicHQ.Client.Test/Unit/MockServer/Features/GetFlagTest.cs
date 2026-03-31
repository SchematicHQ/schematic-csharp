using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Features;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetFlagTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "created_at": "2024-01-15T09:30:00.000Z",
                "default_value": true,
                "description": "description",
                "feature": {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "description": "description",
                  "event_subtype": "event_subtype",
                  "feature_type": "boolean",
                  "icon": "icon",
                  "id": "id",
                  "lifecycle_phase": "add_on",
                  "maintainer_id": "maintainer_id",
                  "name": "name",
                  "plural_name": "plural_name",
                  "singular_name": "singular_name",
                  "trait_id": "trait_id",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
                "feature_id": "feature_id",
                "flag_type": "boolean",
                "id": "id",
                "key": "key",
                "last_checked_at": "2024-01-15T09:30:00.000Z",
                "maintainer_id": "maintainer_id",
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
              },
              "params": {
                "key": "value"
              }
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/flags/flag_id").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Features.GetFlagAsync("flag_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
