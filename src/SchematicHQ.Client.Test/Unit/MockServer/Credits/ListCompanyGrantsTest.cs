using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Credits;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListCompanyGrantsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "company_id": "company_id",
                  "company_name": "company_name",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "credit_icon": "credit_icon",
                  "credit_id": "credit_id",
                  "credit_name": "credit_name",
                  "currency": "currency",
                  "expires_at": "2024-01-15T09:30:00.000Z",
                  "grant_reason": "billing_credit_auto_topup",
                  "id": "id",
                  "plan_id": "plan_id",
                  "plan_name": "plan_name",
                  "price": {
                    "currency": "currency",
                    "external_price_id": "external_price_id",
                    "id": "id",
                    "interval": "day",
                    "price": 1000000,
                    "provider_type": "schematic",
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
                }
              ],
              "params": {
                "company_id": "company_id",
                "dir": "asc",
                "limit": 1000000,
                "offset": 1000000,
                "order": "created_at"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/credits/grants/company/list")
                    .WithParam("company_id", "company_id")
                    .WithParam("order", "created_at")
                    .WithParam("dir", "asc")
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

        var response = await Client.Credits.ListCompanyGrantsAsync(
            new ListCompanyGrantsRequest
            {
                CompanyId = "company_id",
                Order = CreditGrantSortOrder.CreatedAt,
                Dir = SortDirection.Asc,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
