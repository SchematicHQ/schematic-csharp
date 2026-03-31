using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountCreditLedgerTest : BaseMockServerTest
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
                "billing_credit_id": "billing_credit_id",
                "company_id": "company_id",
                "end_time": "end_time",
                "feature_id": "feature_id",
                "limit": 1000000,
                "offset": 1000000,
                "period": "daily",
                "start_time": "start_time"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/credits/ledger/count")
                    .WithParam("company_id", "company_id")
                    .WithParam("billing_credit_id", "billing_credit_id")
                    .WithParam("feature_id", "feature_id")
                    .WithParam("period", "daily")
                    .WithParam("start_time", "start_time")
                    .WithParam("end_time", "end_time")
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

        var response = await Client.Credits.CountCreditLedgerAsync(
            new CountCreditLedgerRequest
            {
                CompanyId = "company_id",
                BillingCreditId = "billing_credit_id",
                FeatureId = "feature_id",
                Period = CreditLedgerPeriod.Daily,
                StartTime = "start_time",
                EndTime = "end_time",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
