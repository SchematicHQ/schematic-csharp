using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertBillingProductTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "external_id": "external_id",
              "price": 1.1
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "account_id": "account_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "environment_id": "environment_id",
                "external_id": "external_id",
                "is_active": true,
                "name": "name",
                "price": 1.1,
                "price_decimal": "price_decimal",
                "product_id": "product_id",
                "provider_type": "schematic",
                "quantity": 1.1,
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
                    .WithPath("/billing/product/upsert")
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

        var response = await Client.Billing.UpsertBillingProductAsync(
            new CreateBillingProductRequestBody { ExternalId = "external_id", Price = 1.1 }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
