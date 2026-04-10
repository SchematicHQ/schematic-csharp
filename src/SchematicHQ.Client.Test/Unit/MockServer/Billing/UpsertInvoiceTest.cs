using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertInvoiceTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "amount_due": 1000000,
              "amount_paid": 1000000,
              "amount_remaining": 1000000,
              "collection_method": "collection_method",
              "currency": "currency",
              "customer_external_id": "customer_external_id",
              "subtotal": 1000000
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "amount_due": 1000000,
                "amount_paid": 1000000,
                "amount_remaining": 1000000,
                "collection_method": "collection_method",
                "company_id": "company_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "currency": "currency",
                "customer_external_id": "customer_external_id",
                "due_date": "2024-01-15T09:30:00.000Z",
                "environment_id": "environment_id",
                "external_id": "external_id",
                "id": "id",
                "payment_method_external_id": "payment_method_external_id",
                "provider_type": "orb",
                "status": "draft",
                "subscription_external_id": "subscription_external_id",
                "subtotal": 1000000,
                "updated_at": "2024-01-15T09:30:00.000Z",
                "url": "url"
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
                    .WithPath("/billing/invoices")
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

        var response = await Client.Billing.UpsertInvoiceAsync(
            new CreateInvoiceRequestBody
            {
                AmountDue = 1000000,
                AmountPaid = 1000000,
                AmountRemaining = 1000000,
                CollectionMethod = "collection_method",
                Currency = "currency",
                CustomerExternalId = "customer_external_id",
                Subtotal = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
