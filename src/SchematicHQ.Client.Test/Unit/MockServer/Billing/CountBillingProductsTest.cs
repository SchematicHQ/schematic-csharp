using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountBillingProductsTest : BaseMockServerTest
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
                "ids": [
                  "ids"
                ],
                "is_active": true,
                "limit": 1000000,
                "name": "name",
                "offset": 1000000,
                "price_usage_type": "licensed",
                "provider_type": "orb",
                "q": "q",
                "with_one_time_charges": true,
                "with_prices_only": true,
                "with_zero_price": true,
                "without_linked_to_plan": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/products/count")
                    .WithParam("name", "name")
                    .WithParam("price_usage_type", "licensed")
                    .WithParam("provider_type", "orb")
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

        var response = await Client.Billing.CountBillingProductsAsync(
            new CountBillingProductsRequest
            {
                Ids = new List<string>() { "ids" },
                IsActive = true,
                Name = "name",
                PriceUsageType = BillingPriceUsageType.Licensed,
                ProviderType = BillingProviderType.Orb,
                Q = "q",
                WithOneTimeCharges = true,
                WithPricesOnly = true,
                WithZeroPrice = true,
                WithoutLinkedToPlan = true,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
