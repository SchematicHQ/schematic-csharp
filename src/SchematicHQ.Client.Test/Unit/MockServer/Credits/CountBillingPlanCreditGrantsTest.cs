using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountBillingPlanCreditGrantsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "count": 1
              },
              "params": {
                "credit_id": "credit_id",
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "plan_id": "plan_id",
                "plan_ids": [
                  "plan_ids"
                ],
                "plan_version_id": "plan_version_id"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/credits/plan-grants/count")
                    .WithParam("credit_id", "credit_id")
                    .WithParam("plan_id", "plan_id")
                    .WithParam("plan_version_id", "plan_version_id")
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

        var response = await Client.Credits.CountBillingPlanCreditGrantsAsync(
            new CountBillingPlanCreditGrantsRequest
            {
                CreditId = "credit_id",
                Ids = new List<string>() { "ids" },
                PlanId = "plan_id",
                PlanIds = new List<string>() { "plan_ids" },
                PlanVersionId = "plan_version_id",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
