using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetEnrichedCreditLedgerTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "billing_credit_auto_topup_grant_count": 1000000,
                  "billing_credit_id": "billing_credit_id",
                  "company": {
                    "id": "id",
                    "name": "name"
                  },
                  "company_id": "company_id",
                  "credit": {
                    "id": "id",
                    "name": "name"
                  },
                  "expired_grant_count": 1000000,
                  "feature": {
                    "id": "id",
                    "name": "name"
                  },
                  "feature_id": "feature_id",
                  "first_transaction_at": "2024-01-15T09:30:00.000Z",
                  "free_grant_count": 1000000,
                  "grant_count": 1000000,
                  "last_transaction_at": "2024-01-15T09:30:00.000Z",
                  "manually_zeroed_count": 1000000,
                  "net_change": 1.1,
                  "plan_grant_count": 1000000,
                  "purchased_grant_count": 1000000,
                  "time_bucket": "2024-01-15T09:30:00.000Z",
                  "total_consumed": 1.1,
                  "total_granted": 1.1,
                  "transaction_count": 1000000,
                  "usage_count": 1000000,
                  "zeroed_out_count": 1000000
                }
              ],
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
                    .WithPath("/billing/credits/ledger")
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

        var response = await Client.Credits.GetEnrichedCreditLedgerAsync(
            new GetEnrichedCreditLedgerRequest
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
