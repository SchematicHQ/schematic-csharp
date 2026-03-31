using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListUsersTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "company_memberships": [
                    {
                      "company_id": "company_id",
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "id": "id",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "user_id": "user_id"
                    }
                  ],
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "entity_traits": [
                    {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "definition_id": "definition_id",
                      "environment_id": "environment_id",
                      "id": "id",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "value": "value"
                    }
                  ],
                  "environment_id": "environment_id",
                  "id": "id",
                  "keys": [
                    {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "definition_id": "definition_id",
                      "entity_id": "entity_id",
                      "entity_type": "company",
                      "environment_id": "environment_id",
                      "id": "id",
                      "key": "key",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "value": "value"
                    }
                  ],
                  "last_seen_at": "2024-01-15T09:30:00.000Z",
                  "name": "name",
                  "traits": {
                    "key": "value"
                  },
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "company_id": "company_id",
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "plan_id": "plan_id",
                "q": "q"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/users")
                    .WithParam("company_id", "company_id")
                    .WithParam("plan_id", "plan_id")
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

        var response = await Client.Companies.ListUsersAsync(
            new ListUsersRequest
            {
                CompanyId = "company_id",
                PlanId = "plan_id",
                Q = "q",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
