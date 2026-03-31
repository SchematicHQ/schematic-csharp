using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetUserTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
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
              },
              "params": {
                "key": "value"
              }
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/users/user_id").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Companies.GetUserAsync("user_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
