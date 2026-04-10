using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Features;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountFeaturesTest : BaseMockServerTest
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
                "boolean_require_event": true,
                "feature_type": [
                  "boolean"
                ],
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "plan_version_id": "plan_version_id",
                "q": "q",
                "without_company_override_for": "without_company_override_for",
                "without_plan_entitlement_for": "without_plan_entitlement_for"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/features/count")
                    .WithParam("plan_version_id", "plan_version_id")
                    .WithParam("q", "q")
                    .WithParam("without_company_override_for", "without_company_override_for")
                    .WithParam("without_plan_entitlement_for", "without_plan_entitlement_for")
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

        var response = await Client.Features.CountFeaturesAsync(
            new CountFeaturesRequest
            {
                BooleanRequireEvent = true,
                PlanVersionId = "plan_version_id",
                Q = "q",
                WithoutCompanyOverrideFor = "without_company_override_for",
                WithoutPlanEntitlementFor = "without_plan_entitlement_for",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
