using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateEntityTraitDefinitionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
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
                    .WithPath("/entity-trait-definitions/entity_trait_definition_id")
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

        var response = await Client.Companies.UpdateEntityTraitDefinitionAsync(
            "entity_trait_definition_id",
            new UpdateEntityTraitDefinitionRequestBody { TraitType = TraitType.Boolean }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
