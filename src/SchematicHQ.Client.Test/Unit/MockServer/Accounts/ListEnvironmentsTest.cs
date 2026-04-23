using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListEnvironmentsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "environment_type": "development",
                  "id": "id",
                  "name": "name",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/environments")
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

        var response = await Client.Accounts.ListEnvironmentsAsync(
            new ListEnvironmentsRequest
            {
                Ids = [new List<string>() { "ids" }],
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
