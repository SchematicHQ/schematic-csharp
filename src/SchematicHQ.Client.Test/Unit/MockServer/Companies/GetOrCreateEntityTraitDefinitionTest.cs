using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetOrCreateEntityTraitDefinitionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "entity_type": "company",
              "hierarchy": [
                "hierarchy"
              ],
              "trait_type": "boolean"
            }
            """;

        const string mockResponse = """
            {
              "data": {
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
              "params": {
                "key": "value"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/entity-trait-definitions")
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

        var response = await Client.Companies.GetOrCreateEntityTraitDefinitionAsync(
            new CreateEntityTraitDefinitionRequestBody
            {
                EntityType = EntityType.Company,
                Hierarchy = new List<string>() { "hierarchy" },
                TraitType = TraitType.Boolean,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
