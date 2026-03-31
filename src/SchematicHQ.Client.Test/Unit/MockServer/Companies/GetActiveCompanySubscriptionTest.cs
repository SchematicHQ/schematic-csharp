using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetActiveCompanySubscriptionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "cancel_at": "2024-01-15T09:30:00.000Z",
                  "cancel_at_period_end": true,
                  "currency": "currency",
                  "customer_external_id": "customer_external_id",
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
                  "interval": "interval",
                  "latest_invoice": {
                    "amount_due": 1000000,
                    "amount_paid": 1000000,
                    "amount_remaining": 1000000,
                    "collection_method": "collection_method",
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "currency": "currency",
                    "customer_external_id": "customer_external_id",
                    "environment_id": "environment_id",
                    "id": "id",
                    "provider_type": "schematic",
                    "subtotal": 1000000,
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "payment_method": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "customer_external_id": "customer_external_id",
                    "environment_id": "environment_id",
                    "external_id": "external_id",
                    "id": "id",
                    "payment_method_type": "payment_method_type",
                    "provider_type": "schematic",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
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
                      "provider_type": "schematic",
                      "quantity": 1.1,
                      "subscription_id": "subscription_id",
                      "updated_at": "2024-01-15T09:30:00.000Z",
                      "usage_type": "licensed"
                    }
                  ],
                  "status": "status",
                  "subscription_external_id": "subscription_external_id",
                  "total_price": 1000000,
                  "trial_end": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "company_id": "company_id",
                "company_ids": [
                  "company_ids"
                ],
                "limit": 1000000,
                "offset": 1000000
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/company-subscriptions")
                    .WithParam("company_id", "company_id")
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

        var response = await Client.Companies.GetActiveCompanySubscriptionAsync(
            new GetActiveCompanySubscriptionRequest
            {
                CompanyId = "company_id",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
