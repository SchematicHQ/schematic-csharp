using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plans;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class RetryCustomPlanBillingTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "customer_email": "customer_email",
              "pay_in_advance": [
                {
                  "price_id": "price_id",
                  "quantity": 1000000
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "activation_strategy": "on_payment",
                "company_id": "company_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "days_until_due": 1000000,
                "id": "id",
                "paid_at": "2024-01-15T09:30:00.000Z",
                "plan_id": "plan_id",
                "published_at": "2024-01-15T09:30:00.000Z",
                "status": "active",
                "stripe_invoice_url": "stripe_invoice_url",
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
                    .WithPath("/custom-plan-billings/custom_plan_billing_id/retry")
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

        var response = await Client.Plans.RetryCustomPlanBillingAsync(
            "custom_plan_billing_id",
            new RetryCustomPlanBillingRequestBody
            {
                CustomerEmail = "customer_email",
                PayInAdvance = new List<UpdatePayInAdvanceRequestBody>()
                {
                    new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
