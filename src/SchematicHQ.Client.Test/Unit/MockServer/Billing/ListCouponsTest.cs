using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListCouponsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "account_id": "account_id",
                  "amount_off": 1000000,
                  "currency": "currency",
                  "duration": "duration",
                  "duration_in_months": 1000000,
                  "environment_id": "environment_id",
                  "external_id": "external_id",
                  "id": "id",
                  "is_active": true,
                  "max_redemptions": 1000000,
                  "metadata": {
                    "key": "value"
                  },
                  "name": "name",
                  "percent_off": 1.1,
                  "provider_type": "orb",
                  "times_redeemed": 1000000,
                  "valid_from": "2024-01-15T09:30:00.000Z",
                  "valid_until": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "is_active": true,
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
                    .WithPath("/billing/coupons")
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

        var response = await Client.Billing.ListCouponsAsync(
            new ListCouponsRequest
            {
                IsActive = true,
                Q = "q",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
