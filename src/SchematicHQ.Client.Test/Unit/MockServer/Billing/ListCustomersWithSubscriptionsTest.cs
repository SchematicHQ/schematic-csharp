using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListCustomersWithSubscriptionsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "company_id": "company_id",
                  "deleted_at": "2024-01-15T09:30:00.000Z",
                  "email": "email",
                  "external_id": "external_id",
                  "id": "id",
                  "name": "name",
                  "provider_type": "orb",
                  "subscriptions": [
                    {
                      "currency": "currency",
                      "interval": "interval",
                      "metered_usage": true,
                      "per_unit_price": 1000000,
                      "total_price": 1000000
                    }
                  ],
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "company_ids": [
                  "company_ids"
                ],
                "limit": 1000000,
                "name": "name",
                "offset": 1000000,
                "provider_type": "orb",
                "q": "q"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/billing/customers")
                    .WithParam("name", "name")
                    .WithParam("provider_type", "orb")
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

        var response = await Client.Billing.ListCustomersWithSubscriptionsAsync(
            new ListCustomersWithSubscriptionsRequest
            {
                CompanyIds = new List<string>() { "company_ids" },
                Name = "name",
                ProviderType = BillingProviderType.Orb,
                Q = "q",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
