using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListBillingProductsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "account_id": "account_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "environment_id": "environment_id",
                  "external_id": "external_id",
                  "is_active": true,
                  "name": "name",
                  "price": 1.1,
                  "price_decimal": "price_decimal",
                  "prices": [
                    {
                      "currency": "currency",
                      "external_price_id": "external_price_id",
                      "id": "id",
                      "interval": "day",
                      "price": 1000000,
                      "provider_type": "orb",
                      "scheme": "per_unit"
                    }
                  ],
                  "product_id": "product_id",
                  "provider_type": "orb",
                  "quantity": 1.1,
                  "subscription_count": 1000000,
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
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
                    .WithPath("/billing/products")
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

        var response = await Client.Billing.ListBillingProductsAsync(
            new ListBillingProductsRequest
            {
                Ids = [new List<string>() { "ids" }],
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
