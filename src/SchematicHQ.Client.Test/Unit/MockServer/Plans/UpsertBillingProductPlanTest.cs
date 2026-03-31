using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plans;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertBillingProductPlanTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "charge_type": "free",
              "is_trialable": true
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "account_id": "account_id",
                "billing_product_id": "billing_product_id",
                "charge_type": "free",
                "controlled_by": "schematic",
                "environment_id": "environment_id",
                "is_trialable": true,
                "monthly_price_id": "monthly_price_id",
                "one_time_price_id": "one_time_price_id",
                "plan_id": "plan_id",
                "trial_days": 1000000,
                "yearly_price_id": "yearly_price_id"
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
                    .WithPath("/plans/plan_id/billing_products")
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

        var response = await Client.Plans.UpsertBillingProductPlanAsync(
            "plan_id",
            new UpsertBillingProductRequestBody { ChargeType = ChargeType.Free, IsTrialable = true }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
