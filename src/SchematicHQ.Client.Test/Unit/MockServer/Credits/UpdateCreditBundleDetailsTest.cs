using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateCreditBundleDetailsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "bundle_name": "bundle_name",
              "price_per_unit": 1000000
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "billing_invoice_id": "billing_invoice_id",
                "bundle_type": "fixed",
                "created_at": "2024-01-15T09:30:00.000Z",
                "credit_description": "credit_description",
                "credit_icon": "credit_icon",
                "credit_id": "credit_id",
                "credit_name": "credit_name",
                "currency_prices": [
                  {
                    "currency": "currency"
                  }
                ],
                "expiry_type": "duration",
                "expiry_unit": "billing_periods",
                "expiry_unit_count": 1000000,
                "has_grants": true,
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
                "quantity": 1000000,
                "singular_name": "singular_name",
                "status": "active",
                "unit_price": {
                  "currency": "currency",
                  "external_price_id": "external_price_id",
                  "id": "id",
                  "interval": "day",
                  "price": 1000000,
                  "price_decimal": "price_decimal",
                  "provider_type": "orb",
                  "scheme": "per_unit"
                },
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
                    .WithPath("/billing/credits/bundles/bundle_id")
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

        var response = await Client.Credits.UpdateCreditBundleDetailsAsync(
            "bundle_id",
            new UpdateCreditBundleDetailsRequestBody
            {
                BundleName = "bundle_name",
                PricePerUnit = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
