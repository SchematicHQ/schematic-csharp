using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListInvoicesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
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
                  "provider_type": "schematic",
                  "status": "draft",
                  "subscription_external_id": "subscription_external_id",
                  "subtotal": 1000000,
                  "updated_at": "2024-01-15T09:30:00.000Z",
                  "url": "url"
                }
              ],
              "params": {
                "company_id": "company_id",
                "customer_external_id": "customer_external_id",
                "limit": 1000000,
                "offset": 1000000,
                "subscription_external_id": "subscription_external_id"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/invoices")
                    .WithParam("company_id", "company_id")
                    .WithParam("customer_external_id", "customer_external_id")
                    .WithParam("subscription_external_id", "subscription_external_id")
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

        var response = await Client.Billing.ListInvoicesAsync(
            new ListInvoicesRequest
            {
                CompanyId = "company_id",
                CustomerExternalId = "customer_external_id",
                SubscriptionExternalId = "subscription_external_id",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
