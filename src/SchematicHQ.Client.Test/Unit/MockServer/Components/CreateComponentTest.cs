using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Components;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreateComponentTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "entity_type": "billing",
              "name": "name"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "ast": {
                  "key": 1.1
                },
                "created_at": "2024-01-15T09:30:00.000Z",
                "id": "id",
                "name": "name",
                "state": "draft",
                "type": "billing",
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
                    .WithPath("/components")
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

        var response = await Client.Components.CreateComponentAsync(
            new CreateComponentRequestBody
            {
                EntityType = ComponentEntityType.Billing,
                Name = "name",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
