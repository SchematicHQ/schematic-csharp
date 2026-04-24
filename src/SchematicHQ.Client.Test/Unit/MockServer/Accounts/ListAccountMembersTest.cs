using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListAccountMembersTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "email": "email",
                  "id": "id",
                  "image_url": "image_url",
                  "name": "name",
                  "permissions": {
                    "key": [
                      "billing_credits_edit"
                    ]
                  },
                  "role": "admin",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "q": "q"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/account-members")
                    .WithParam("q", "q")
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

        var response = await Client.Accounts.ListAccountMembersAsync(
            new ListAccountMembersRequest
            {
                Ids = new List<string>() { "ids" },
                Q = "q",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
