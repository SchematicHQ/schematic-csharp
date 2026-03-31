using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Components;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetComponentTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
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
                    .WithPath("/components/component_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Components.GetComponentAsync("component_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
