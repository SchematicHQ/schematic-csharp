using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetPlanChangeTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "action": "checkout",
                "actor_type": "api_key",
                "add_ons_added": [
                  {
                    "deleted": true,
                    "description": "description",
                    "icon": "icon",
                    "id": "id",
                    "name": "name"
                  }
                ],
                "add_ons_removed": [
                  {
                    "deleted": true,
                    "description": "description",
                    "icon": "icon",
                    "id": "id",
                    "name": "name"
                  }
                ],
                "api_key": {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "description": "description",
                  "environment": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_type": "development",
                    "id": "id",
                    "name": "name",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "environment_id": "environment_id",
                  "id": "id",
                  "last_used_at": "2024-01-15T09:30:00.000Z",
                  "name": "name",
                  "readonly": true,
                  "scopes": [
                    "admin"
                  ],
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
                "audit_log": {
                  "actor_type": "api_key",
                  "api_key_id": "api_key_id",
                  "ended_at": "2024-01-15T09:30:00.000Z",
                  "environment": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_type": "development",
                    "id": "id",
                    "name": "name",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "environment_id": "environment_id",
                  "id": "id",
                  "method": "method",
                  "resource_id": 1000000,
                  "resource_id_string": "resource_id_string",
                  "resource_name": "resource_name",
                  "resource_type": "resource_type",
                  "resp_code": 1000000,
                  "secondary_resource": "secondary_resource",
                  "started_at": "2024-01-15T09:30:00.000Z",
                  "url": "url",
                  "user_name": "user_name"
                },
                "base_plan": {
                  "deleted": true,
                  "description": "description",
                  "icon": "icon",
                  "id": "id",
                  "name": "name"
                },
                "base_plan_action": "fallback",
                "base_plan_version": {
                  "id": "id",
                  "name": "name",
                  "version": 1000000
                },
                "company": {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "environment_id": "environment_id",
                  "id": "id",
                  "last_seen_at": "2024-01-15T09:30:00.000Z",
                  "logo_url": "logo_url",
                  "name": "name",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
                "company_id": "company_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "environment_id": "environment_id",
                "id": "id",
                "previous_base_plan": {
                  "deleted": true,
                  "description": "description",
                  "icon": "icon",
                  "id": "id",
                  "name": "name"
                },
                "previous_base_plan_version": {
                  "id": "id",
                  "name": "name",
                  "version": 1000000
                },
                "request_id": "request_id",
                "subscription_change_action": "adjustment",
                "traits_updated": [
                  {
                    "feature_id": "feature_id",
                    "hierarchy": [
                      "hierarchy"
                    ],
                    "reason": "reason",
                    "trait_id": "trait_id",
                    "trait_name": "trait_name",
                    "trait_type": "boolean",
                    "value": "value"
                  }
                ],
                "updated_at": "2024-01-15T09:30:00.000Z",
                "user_id": "user_id",
                "user_name": "user_name"
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
                    .WithPath("/plan-changes/plan_change_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Companies.GetPlanChangeAsync("plan_change_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
