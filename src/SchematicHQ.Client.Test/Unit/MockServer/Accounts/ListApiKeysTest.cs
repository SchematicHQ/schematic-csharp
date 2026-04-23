using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListApiKeysTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
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
                }
              ],
              "params": {
                "environment_id": "environment_id",
                "limit": 1000000,
                "offset": 1000000,
                "require_environment": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/api-keys")
                    .WithParam("environment_id", "environment_id")
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

        var response = await Client.Accounts.ListApiKeysAsync(
            new ListApiKeysRequest
            {
                EnvironmentId = "environment_id",
                RequireEnvironment = true,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
