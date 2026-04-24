using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateApiKeyTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {}
            """;

        const string mockResponse = """
            {
              "data": {
                "created_at": "2024-01-15T09:30:00.000Z",
                "description": "description",
                "environment": {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "environment_type": "development",
                  "id": "id",
                  "name": "name",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
                "environment_id": "environment_id",
                "id": "id",
                "last_used_at": "2024-01-15T09:30:00.000Z",
                "name": "name",
                "readonly": true,
                "scopes": [
                  "admin"
                ],
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
                    .WithPath("/api-keys/api_key_id")
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

        var response = await Client.Accounts.UpdateApiKeyAsync(
            "api_key_id",
            new UpdateApiKeyRequestBody()
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
