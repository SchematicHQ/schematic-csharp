using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListCreditEventLedgerTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "amount": 1.1,
                  "auto_topup_log_id": "auto_topup_log_id",
                  "billing_credit_bundle_id": "billing_credit_bundle_id",
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
                  "credit_name": "credit_name",
                  "environment_id": "environment_id",
                  "event_at": "2024-01-15T09:30:00.000Z",
                  "event_id": "event_id",
                  "event_type": "grant",
                  "expiry_type": "duration",
                  "expiry_unit": "billing_periods",
                  "expiry_unit_count": 1000000,
                  "feature": {
                    "id": "id",
                    "name": "name"
                  },
                  "feature_id": "feature_id",
                  "from_grant_id": "from_grant_id",
                  "grant_expires_at": "2024-01-15T09:30:00.000Z",
                  "grant_id": "grant_id",
                  "grant_quantity": 1000000,
                  "grant_quantity_remaining": 1.1,
                  "grant_reason": "adjustment",
                  "grant_valid_from": "2024-01-15T09:30:00.000Z",
                  "plan_id": "plan_id",
                  "quantity_consumed": 1.1,
                  "quantity_remaining_at_zero_out": 1.1,
                  "source_id": 1000000,
                  "to_grant_id": "to_grant_id",
                  "usage_event_id": "usage_event_id",
                  "zeroed_out_reason": "expired"
                }
              ],
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
                    .WithPath("/v2/billing/credits/ledger")
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

        var response = await Client.Credits.ListCreditEventLedgerAsync(
            new ListCreditEventLedgerRequest
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
