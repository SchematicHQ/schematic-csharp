using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetWhoAmITest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "account_id": "account_id",
                "account_name": "account_name",
                "actor_type": "api_key",
                "api_key_id": "api_key_id",
                "environment_id": "environment_id",
                "environments": [
                  {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_type": "development",
                    "id": "id",
                    "name": "name",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  }
                ],
                "stripe_user_id": "stripe_user_id",
                "user_id": "user_id",
                "user_name": "user_name"
              },
              "params": {
                "key": "value"
              }
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/whoami").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Accounts.GetWhoAmIAsync();
        JsonAssert.AreEqual(response, mockResponse);
    }
}
