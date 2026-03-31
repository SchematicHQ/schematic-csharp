using global::System.Globalization;
using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Entitlements;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetFeatureUsageTimeSeriesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "feature_id": "feature_id",
                "feature_type": "boolean",
                "limits": [
                  {
                    "effective_at": "2024-01-15T09:30:00.000Z",
                    "is_soft_limit": true,
                    "limit_source": "company_override"
                  }
                ],
                "period_type": "period_type",
                "usage_points": [
                  {
                    "timestamp": "2024-01-15T09:30:00.000Z",
                    "usage": 1000000
                  }
                ]
              },
              "params": {
                "company_id": "company_id",
                "end_time": "2024-01-15T09:30:00.000Z",
                "feature_id": "feature_id",
                "granularity": "daily",
                "start_time": "2024-01-15T09:30:00.000Z"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/feature-usage-timeseries")
                    .WithParam("company_id", "company_id")
                    .WithParam("end_time", "2024-01-15T09:30:00.000Z")
                    .WithParam("feature_id", "feature_id")
                    .WithParam("granularity", "daily")
                    .WithParam("start_time", "2024-01-15T09:30:00.000Z")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Entitlements.GetFeatureUsageTimeSeriesAsync(
            new GetFeatureUsageTimeSeriesRequest
            {
                CompanyId = "company_id",
                EndTime = DateTime.Parse(
                    "2024-01-15T09:30:00.000Z",
                    null,
                    DateTimeStyles.AdjustToUniversal
                ),
                FeatureId = "feature_id",
                Granularity = TimeSeriesGranularity.Daily,
                StartTime = DateTime.Parse(
                    "2024-01-15T09:30:00.000Z",
                    null,
                    DateTimeStyles.AdjustToUniversal
                ),
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
