using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreateEnvironmentTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "environment_type": "development",
              "name": "name"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "api_keys": [
                  {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "id": "id",
                    "name": "name",
                    "readonly": true,
                    "scopes": [
                      "admin"
                    ],
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  }
                ],
                "created_at": "2024-01-15T09:30:00.000Z",
                "environment_type": "development",
                "id": "id",
                "name": "name",
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
                    .WithPath("/environments")
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

        var response = await Client.Accounts.CreateEnvironmentAsync(
            new CreateEnvironmentRequestBody
            {
                EnvironmentType = EnvironmentType.Development,
                Name = "name",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
