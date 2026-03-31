using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accesstokens;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class IssueTemporaryAccessTokenTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "lookup": {
                "key": "value"
              },
              "resource_type": "company"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "api_key_id": "api_key_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "environment_id": "environment_id",
                "expired_at": "2024-01-15T09:30:00.000Z",
                "id": "id",
                "resource_type": "company",
                "token": "token",
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
                    .WithPath("/temporary-access-tokens")
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

        var response = await Client.Accesstokens.IssueTemporaryAccessTokenAsync(
            new IssueTemporaryAccessTokenRequestBody
            {
                Lookup = new Dictionary<string, string>() { { "key", "value" } },
                ResourceType = "company",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
