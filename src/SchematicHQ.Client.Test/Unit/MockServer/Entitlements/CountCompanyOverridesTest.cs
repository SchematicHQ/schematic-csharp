using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Entitlements;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountCompanyOverridesTest : BaseMockServerTest
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
                "company_ids": [
                  "company_ids"
                ],
                "feature_id": "feature_id",
                "feature_ids": [
                  "feature_ids"
                ],
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "q": "q",
                "without_expired": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/company-overrides/count")
                    .WithParam("company_id", "company_id")
                    .WithParam("feature_id", "feature_id")
                    .WithParam("q", "q")
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

        var response = await Client.Entitlements.CountCompanyOverridesAsync(
            new CountCompanyOverridesRequest
            {
                CompanyId = "company_id",
                FeatureId = "feature_id",
                WithoutExpired = true,
                Q = "q",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
