using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Checkout;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateCustomerSubscriptionTrialEndTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {}
            """;

        const string mockResponse = """
            {
              "data": {
                "application_id": "application_id",
                "cancel_at": 1000000,
                "cancel_at_period_end": true,
                "company_id": "company_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "currency": "currency",
                "customer_external_id": "customer_external_id",
                "default_payment_method_id": "default_payment_method_id",
                "discounts": [
                  {
                    "coupon_id": "coupon_id",
                    "coupon_name": "coupon_name",
                    "discount_external_id": "discount_external_id",
                    "duration": "duration",
                    "is_active": true,
                    "started_at": "2024-01-15T09:30:00.000Z",
                    "subscription_external_id": "subscription_external_id"
                  }
                ],
                "expired_at": "2024-01-15T09:30:00.000Z",
                "id": "id",
                "interval": "interval",
                "latest_invoice": {
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
                "metadata": {
                  "key": "value"
                },
                "payment_method": {
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
                "period_end": 1000000,
                "period_start": 1000000,
                "products": [
                  {
                    "billing_scheme": "per_unit",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency": "currency",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "id": "id",
                    "interval": "interval",
                    "name": "name",
                    "package_size": 1000000,
                    "price": 1000000,
                    "price_external_id": "price_external_id",
                    "price_id": "price_id",
                    "price_tier": [
                      {}
                    ],
                    "provider_type": "orb",
                    "quantity": 1.1,
                    "subscription_id": "subscription_id",
                    "updated_at": "2024-01-15T09:30:00.000Z",
                    "usage_type": "licensed"
                  }
                ],
                "provider_type": "orb",
                "status": "status",
                "subscription_external_id": "subscription_external_id",
                "total_price": 1000000,
                "trial_end": 1000000,
                "trial_end_setting": "cancel"
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
                    .WithPath("/subscription/subscription_id/edit-trial-end")
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

        var response = await Client.Checkout.UpdateCustomerSubscriptionTrialEndAsync(
            "subscription_id",
            new UpdateTrialEndRequestBody()
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
