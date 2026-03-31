using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountCreditEventLedgerTest : BaseMockServerTest
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
                "event_type": "grant",
                "feature_id": "feature_id",
                "limit": 1000000,
                "offset": 1000000,
                "start_time": "start_time"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v2/billing/credits/ledger/count")
                    .WithParam("billing_credit_id", "billing_credit_id")
                    .WithParam("company_id", "company_id")
                    .WithParam("end_time", "end_time")
                    .WithParam("event_type", "grant")
                    .WithParam("feature_id", "feature_id")
                    .WithParam("start_time", "start_time")
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

        var response = await Client.Credits.CountCreditEventLedgerAsync(
            new CountCreditEventLedgerRequest
            {
                BillingCreditId = "billing_credit_id",
                CompanyId = "company_id",
                EndTime = "end_time",
                EventType = CreditEventType.Grant,
                FeatureId = "feature_id",
                StartTime = "start_time",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
