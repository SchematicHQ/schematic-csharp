using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListBillingPricesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "billing_scheme": "per_unit",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "currency": "currency",
                  "id": "id",
                  "interval": "day",
                  "is_active": true,
                  "meter_event_name": "meter_event_name",
                  "meter_event_payload_key": "meter_event_payload_key",
                  "meter_id": "meter_id",
                  "nickname": "nickname",
                  "package_size": 1000000,
                  "price": 1000000,
                  "price_decimal": "price_decimal",
                  "price_external_id": "price_external_id",
                  "price_id": "price_id",
                  "price_tier": [
                    {}
                  ],
                  "product_external_id": "product_external_id",
                  "product_id": "product_id",
                  "product_name": "product_name",
                  "provider_type": "orb",
                  "tiers_mode": "graduated",
                  "updated_at": "2024-01-15T09:30:00.000Z",
                  "usage_type": "licensed"
                }
              ],
              "params": {
                "currency": "currency",
                "for_initial_plan": true,
                "for_trial_expiry_plan": true,
                "ids": [
                  "ids"
                ],
                "interval": "interval",
                "is_active": true,
                "limit": 1000000,
                "offset": 1000000,
                "price": 1000000,
                "product_id": "product_id",
                "product_ids": [
                  "product_ids"
                ],
                "provider_type": "orb",
                "q": "q",
                "tiers_mode": "graduated",
                "usage_type": "licensed",
                "with_meter": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/price")
                    .WithParam("currency", "currency")
                    .WithParam("interval", "interval")
                    .WithParam("price", "1000000")
                    .WithParam("product_id", "product_id")
                    .WithParam("provider_type", "orb")
                    .WithParam("q", "q")
                    .WithParam("tiers_mode", "graduated")
                    .WithParam("usage_type", "licensed")
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

        var response = await Client.Billing.ListBillingPricesAsync(
            new ListBillingPricesRequest
            {
                Currency = "currency",
                ForInitialPlan = true,
                ForTrialExpiryPlan = true,
                Ids = [new List<string>() { "ids" }],
                Interval = "interval",
                IsActive = true,
                Price = 1000000,
                ProductId = "product_id",
                ProductIds = [new List<string>() { "product_ids" }],
                ProviderType = BillingProviderType.Orb,
                Q = "q",
                TiersMode = BillingTiersMode.Graduated,
                UsageType = BillingPriceUsageType.Licensed,
                WithMeter = true,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
