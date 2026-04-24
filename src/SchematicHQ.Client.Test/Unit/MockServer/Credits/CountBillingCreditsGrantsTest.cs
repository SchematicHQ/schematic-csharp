using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountBillingCreditsGrantsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "count": 1
              },
              "params": {
                "credit_id": "credit_id",
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
                    .WithPath("/billing/credits/grants/count")
                    .WithParam("credit_id", "credit_id")
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

        var response = await Client.Credits.CountBillingCreditsGrantsAsync(
            new CountBillingCreditsGrantsRequest
            {
                CreditId = "credit_id",
                Ids = new List<string>() { "ids" },
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
