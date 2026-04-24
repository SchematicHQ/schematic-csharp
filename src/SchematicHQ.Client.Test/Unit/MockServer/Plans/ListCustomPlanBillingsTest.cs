using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plans;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListCustomPlanBillingsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
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
                }
              ],
              "params": {
                "company_id": "company_id",
                "limit": 1000000,
                "offset": 1000000,
                "plan_id": "plan_id",
                "status": "active",
                "statuses": [
                  "active"
                ]
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/custom-plan-billings")
                    .WithParam("company_id", "company_id")
                    .WithParam("plan_id", "plan_id")
                    .WithParam("status", "active")
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

        var response = await Client.Plans.ListCustomPlanBillingsAsync(
            new ListCustomPlanBillingsRequest
            {
                CompanyId = "company_id",
                PlanId = "plan_id",
                Status = CustomPlanBillingStatus.Active,
                Statuses = new List<CustomPlanBillingStatus>() { CustomPlanBillingStatus.Active },
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
