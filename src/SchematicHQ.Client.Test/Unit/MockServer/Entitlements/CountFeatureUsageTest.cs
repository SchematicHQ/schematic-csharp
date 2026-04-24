using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Entitlements;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountFeatureUsageTest : BaseMockServerTest
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
                "company_keys": {
                  "key": "value"
                },
                "feature_ids": [
                  "feature_ids"
                ],
                "include_usage_aggregation": true,
                "limit": 1000000,
                "offset": 1000000,
                "q": "q",
                "without_negative_entitlements": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/feature-usage/count")
                    .WithParam("company_id", "company_id")
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

        var response = await Client.Entitlements.CountFeatureUsageAsync(
            new CountFeatureUsageRequest
            {
                CompanyId = "company_id",
                FeatureIds = new List<string>() { "feature_ids" },
                IncludeUsageAggregation = true,
                Q = "q",
                WithoutNegativeEntitlements = true,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
