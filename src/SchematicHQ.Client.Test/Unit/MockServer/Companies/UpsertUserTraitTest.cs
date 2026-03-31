using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertUserTraitTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "keys": {
                "key": "value"
              },
              "trait": "trait"
            }
            """;

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
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/user-traits")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Companies.UpsertUserTraitAsync(
            new UpsertTraitRequestBody
            {
                Keys = new Dictionary<string, string>() { { "key", "value" } },
                Trait = "trait",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
