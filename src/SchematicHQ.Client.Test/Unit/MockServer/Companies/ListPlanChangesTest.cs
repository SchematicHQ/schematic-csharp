using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListPlanChangesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
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
                    "id": "id",
                    "name": "name",
                    "readonly": true,
                    "scopes": [
                      "admin"
                    ],
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "audit_log": {
                    "actor_type": "api_key",
                    "id": "id",
                    "method": "method",
                    "started_at": "2024-01-15T09:30:00.000Z",
                    "url": "url"
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
                }
              ],
              "params": {
                "action": "checkout",
                "base_plan_action": "fallback",
                "company_id": "company_id",
                "company_ids": [
                  "company_ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "plan_ids": [
                  "plan_ids"
                ]
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plan-changes")
                    .WithParam("action", "checkout")
                    .WithParam("base_plan_action", "fallback")
                    .WithParam("company_id", "company_id")
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

        var response = await Client.Companies.ListPlanChangesAsync(
            new ListPlanChangesRequest
            {
                Action = PlanChangeAction.Checkout,
                BasePlanAction = PlanChangeBasePlanAction.Fallback,
                CompanyId = "company_id",
                CompanyIds = [new List<string>() { "company_ids" }],
                PlanIds = [new List<string>() { "plan_ids" }],
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
