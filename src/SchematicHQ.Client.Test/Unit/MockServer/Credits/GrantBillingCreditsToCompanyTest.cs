using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GrantBillingCreditsToCompanyTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "company_id": "company_id",
              "credit_id": "credit_id",
              "quantity": 1000000,
              "reason": "adjustment"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "company_id": "company_id",
                "company_name": "company_name",
                "created_at": "2024-01-15T09:30:00.000Z",
                "credit_icon": "credit_icon",
                "credit_id": "credit_id",
                "credit_name": "credit_name",
                "currency": "currency",
                "expires_at": "2024-01-15T09:30:00.000Z",
                "grant_reason": "adjustment",
                "id": "id",
                "plan_id": "plan_id",
                "plan_name": "plan_name",
                "price": {
                  "currency": "currency",
                  "external_price_id": "external_price_id",
                  "id": "id",
                  "interval": "day",
                  "price": 1000000,
                  "price_decimal": "price_decimal",
                  "provider_type": "orb",
                  "scheme": "per_unit"
                },
                "quantity": 1000000,
                "quantity_remaining": 1.1,
                "quantity_used": 1.1,
                "renewal_enabled": true,
                "renewal_period": "daily",
                "source_label": "source_label",
                "transfers": [
                  {
                    "amount": 1.1,
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "direction": "direction",
                    "id": "id",
                    "reason": "reason",
                    "related_grant_id": "related_grant_id"
                  }
                ],
                "updated_at": "2024-01-15T09:30:00.000Z",
                "valid_from": "2024-01-15T09:30:00.000Z",
                "zeroed_out_date": "2024-01-15T09:30:00.000Z",
                "zeroed_out_reason": "expired"
              },
              "params": {
                "key": "value"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/credits/grants/company")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Credits.GrantBillingCreditsToCompanyAsync(
            new CreateCompanyCreditGrant
            {
                CompanyId = "company_id",
                CreditId = "credit_id",
                Quantity = 1000000,
                Reason = BillingCreditGrantReason.Adjustment,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
