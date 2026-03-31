using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertBillingCouponTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "amount_off": 1000000,
              "duration": "duration",
              "duration_in_months": 1000000,
              "external_id": "external_id",
              "max_redemptions": 1000000,
              "name": "name",
              "percent_off": 1.1,
              "times_redeemed": 1000000
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "account_id": "account_id",
                "amount_off": 1000000,
                "currency": "currency",
                "duration": "duration",
                "duration_in_months": 1000000,
                "environment_id": "environment_id",
                "external_id": "external_id",
                "id": "id",
                "is_active": true,
                "max_redemptions": 1000000,
                "metadata": {
                  "key": "value"
                },
                "name": "name",
                "percent_off": 1.1,
                "provider_type": "schematic",
                "times_redeemed": 1000000,
                "valid_from": "2024-01-15T09:30:00.000Z",
                "valid_until": "2024-01-15T09:30:00.000Z"
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
                    .WithPath("/billing/coupons")
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

        var response = await Client.Billing.UpsertBillingCouponAsync(
            new CreateCouponRequestBody
            {
                AmountOff = 1000000,
                Duration = "duration",
                DurationInMonths = 1000000,
                ExternalId = "external_id",
                MaxRedemptions = 1000000,
                Name = "name",
                PercentOff = 1.1,
                TimesRedeemed = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
