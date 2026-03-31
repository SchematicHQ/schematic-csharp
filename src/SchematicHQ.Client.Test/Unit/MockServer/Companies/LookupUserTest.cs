using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class LookupUserTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "company_memberships": [
                  {
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
                    "id": "id",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "user_id": "user_id"
                  },
                  {
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
                    "id": "id",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "user_id": "user_id"
                  }
                ],
                "created_at": "2024-01-15T09:30:00.000Z",
                "entity_traits": [
                  {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "definition": {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "display_name": "display_name",
                      "entity_type": "company",
                      "hierarchy": [
                        "hierarchy",
                        "hierarchy"
                      ],
                      "id": "id",
                      "trait_type": "boolean",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
                    "definition_id": "definition_id",
                    "environment_id": "environment_id",
                    "id": "id",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "value": "value"
                  },
                  {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "definition": {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "display_name": "display_name",
                      "entity_type": "company",
                      "hierarchy": [
                        "hierarchy",
                        "hierarchy"
                      ],
                      "id": "id",
                      "trait_type": "boolean",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
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
                    "definition": {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "entity_type": "company",
                      "id": "id",
                      "key": "key",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
                    "definition_id": "definition_id",
                    "entity_id": "entity_id",
                    "entity_type": "company",
                    "environment_id": "environment_id",
                    "id": "id",
                    "key": "key",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "value": "value"
                  },
                  {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "definition": {
                      "created_at": "2024-01-15T09:30:00.000Z",
                      "entity_type": "company",
                      "id": "id",
                      "key": "key",
                      "updated_at": "2024-01-15T09:30:00.000Z"
                    },
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
                  "traits": {
                    "key": "value"
                  }
                },
                "updated_at": "2024-01-15T09:30:00.000Z"
              },
              "params": {
                "keys": {
                  "keys": "keys"
                }
              }
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/users/lookup").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Companies.LookupUserAsync(
            new LookupUserRequest { Keys = new Dictionary<string, string>() { { "keys", "keys" } } }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
