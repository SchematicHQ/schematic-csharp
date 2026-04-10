using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListBillingCreditsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "burn_strategy": "expiration_priority",
                  "cost_editable": true,
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency_prices": [
                    {
                      "currency": "currency"
                    }
                  ],
                  "default_expiry_unit": "billing_periods",
                  "default_expiry_unit_count": 1000000,
                  "default_rollover_policy": "expire",
                  "description": "description",
                  "icon": "icon",
                  "id": "id",
                  "name": "name",
                  "plural_name": "plural_name",
                  "price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "orb",
                    "scheme": "per_unit"
                  },
                  "product": {
                    "account_id": "account_id",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "is_active": true,
                    "name": "name",
                    "price": 1.1,
                    "product_id": "product_id",
                    "provider_type": "orb",
                    "quantity": 1.1,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "singular_name": "singular_name",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "name": "name",
                "offset": 1000000
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/credits")
                    .WithParam("name", "name")
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

        var response = await Client.Credits.ListBillingCreditsAsync(
            new ListBillingCreditsRequest
            {
                Name = "name",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
