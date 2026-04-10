using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetAccountMemberTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
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
                    .WithPath("/account-members/account_member_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Accounts.GetAccountMemberAsync("account_member_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
