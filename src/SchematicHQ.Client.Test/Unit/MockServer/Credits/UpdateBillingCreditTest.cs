using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateBillingCreditTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "description": "description",
              "name": "name"
            }
            """;

        const string mockResponse = """
            {
              "data": {
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
                  "price_decimal": "price_decimal",
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
                  "price_decimal": "price_decimal",
                  "product_id": "product_id",
                  "provider_type": "orb",
                  "quantity": 1.1,
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
                "singular_name": "singular_name",
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
                    .WithPath("/billing/credits/credit_id")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Credits.UpdateBillingCreditAsync(
            "credit_id",
            new UpdateBillingCreditRequestBody { Description = "description", Name = "name" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
