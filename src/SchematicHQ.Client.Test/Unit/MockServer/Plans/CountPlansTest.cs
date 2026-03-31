using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plans;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountPlansTest : BaseMockServerTest
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
                "company_id": "company_id",
                "for_fallback_plan": true,
                "for_initial_plan": true,
                "for_trial_expiry_plan": true,
                "has_product_id": true,
                "ids": [
                  "ids"
                ],
                "include_draft_versions": true,
                "limit": 1000000,
                "offset": 1000000,
                "plan_type": "plan",
                "q": "q",
                "without_entitlement_for": "without_entitlement_for",
                "without_paid_product_id": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plans/count")
                    .WithParam("company_id", "company_id")
                    .WithParam("plan_type", "plan")
                    .WithParam("q", "q")
                    .WithParam("without_entitlement_for", "without_entitlement_for")
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

        var response = await Client.Plans.CountPlansAsync(
            new CountPlansRequest
            {
                CompanyId = "company_id",
                ForFallbackPlan = true,
                ForInitialPlan = true,
                ForTrialExpiryPlan = true,
                HasProductId = true,
                IncludeDraftVersions = true,
                PlanType = PlanType.Plan,
                Q = "q",
                WithoutEntitlementFor = "without_entitlement_for",
                WithoutPaidProductId = true,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
