using global::System.Globalization;
using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertBillingSubscriptionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "cancel_at_period_end": true,
              "currency": "currency",
              "customer_external_id": "customer_external_id",
              "discounts": [
                {
                  "coupon_external_id": "coupon_external_id",
                  "external_id": "external_id",
                  "is_active": true,
                  "started_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "expired_at": "2024-01-15T09:30:00.000Z",
              "product_external_ids": [
                {
                  "currency": "currency",
                  "interval": "interval",
                  "price": 1000000,
                  "price_external_id": "price_external_id",
                  "product_external_id": "product_external_id",
                  "quantity": 1000000,
                  "usage_type": "licensed"
                }
              ],
              "subscription_external_id": "subscription_external_id",
              "total_price": 1000000
            }
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
                "expired_at": "2024-01-15T09:30:00.000Z",
                "id": "id",
                "interval": "interval",
                "metadata": {
                  "key": "value"
                },
                "period_end": 1000000,
                "period_start": 1000000,
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
                    .WithPath("/billing/subscription/upsert")
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

        var response = await Client.Billing.UpsertBillingSubscriptionAsync(
            new CreateBillingSubscriptionRequestBody
            {
                CancelAtPeriodEnd = true,
                Currency = "currency",
                CustomerExternalId = "customer_external_id",
                Discounts = new List<BillingSubscriptionDiscount>()
                {
                    new BillingSubscriptionDiscount
                    {
                        CouponExternalId = "coupon_external_id",
                        ExternalId = "external_id",
                        IsActive = true,
                        StartedAt = DateTime.Parse(
                            "2024-01-15T09:30:00.000Z",
                            null,
                            DateTimeStyles.AdjustToUniversal
                        ),
                    },
                },
                ExpiredAt = DateTime.Parse(
                    "2024-01-15T09:30:00.000Z",
                    null,
                    DateTimeStyles.AdjustToUniversal
                ),
                ProductExternalIds = new List<BillingProductPricing>()
                {
                    new BillingProductPricing
                    {
                        Currency = "currency",
                        Interval = "interval",
                        Price = 1000000,
                        PriceExternalId = "price_external_id",
                        ProductExternalId = "product_external_id",
                        Quantity = 1000000,
                        UsageType = BillingPriceUsageType.Licensed,
                    },
                },
                SubscriptionExternalId = "subscription_external_id",
                TotalPrice = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
