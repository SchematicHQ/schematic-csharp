using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertBillingPriceTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "billing_scheme": "per_unit",
              "currency": "currency",
              "external_account_id": "external_account_id",
              "interval": "interval",
              "is_active": true,
              "price": 1000000,
              "price_external_id": "price_external_id",
              "price_tiers": [
                {
                  "price_external_id": "price_external_id"
                }
              ],
              "product_external_id": "product_external_id",
              "usage_type": "licensed"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "currency": "currency",
                "external_price_id": "external_price_id",
                "id": "id",
                "interval": "day",
                "nickname": "nickname",
                "price": 1000000,
                "price_decimal": "price_decimal",
                "provider_type": "orb",
                "scheme": "per_unit"
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
                    .WithPath("/billing/price/upsert")
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

        var response = await Client.Billing.UpsertBillingPriceAsync(
            new CreateBillingPriceRequestBody
            {
                BillingScheme = BillingPriceScheme.PerUnit,
                Currency = "currency",
                ExternalAccountId = "external_account_id",
                Interval = "interval",
                IsActive = true,
                Price = 1000000,
                PriceExternalId = "price_external_id",
                PriceTiers = new List<CreateBillingPriceTierRequestBody>()
                {
                    new CreateBillingPriceTierRequestBody { PriceExternalId = "price_external_id" },
                },
                ProductExternalId = "product_external_id",
                UsageType = BillingPriceUsageType.Licensed,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
