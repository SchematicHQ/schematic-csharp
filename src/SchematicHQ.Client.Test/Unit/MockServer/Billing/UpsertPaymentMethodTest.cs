using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertPaymentMethodTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "customer_external_id": "customer_external_id",
              "external_id": "external_id",
              "payment_method_type": "payment_method_type"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "account_last4": "account_last4",
                "account_name": "account_name",
                "bank_name": "bank_name",
                "billing_email": "billing_email",
                "billing_name": "billing_name",
                "card_brand": "card_brand",
                "card_exp_month": 1000000,
                "card_exp_year": 1000000,
                "card_last4": "card_last4",
                "company_id": "company_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "customer_external_id": "customer_external_id",
                "environment_id": "environment_id",
                "external_id": "external_id",
                "id": "id",
                "payment_method_type": "payment_method_type",
                "provider_type": "orb",
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
                    .WithPath("/billing/payment-methods")
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

        var response = await Client.Billing.UpsertPaymentMethodAsync(
            new CreatePaymentMethodRequestBody
            {
                CustomerExternalId = "customer_external_id",
                ExternalId = "external_id",
                PaymentMethodType = "payment_method_type",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
