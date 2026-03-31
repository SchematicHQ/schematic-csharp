using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Billing;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpsertBillingCustomerTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "email": "email",
              "external_id": "external_id",
              "meta": {
                "key": "value"
              },
              "name": "name"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "company_id": "company_id",
                "deleted_at": "2024-01-15T09:30:00.000Z",
                "email": "email",
                "external_id": "external_id",
                "id": "id",
                "name": "name",
                "provider_type": "schematic",
                "updated_at": "2024-01-15T09:30:00.000Z"
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
                    .WithPath("/billing/customer/upsert")
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

        var response = await Client.Billing.UpsertBillingCustomerAsync(
            new CreateBillingCustomerRequestBody
            {
                Email = "email",
                ExternalId = "external_id",
                Meta = new Dictionary<string, string>() { { "key", "value" } },
                Name = "name",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
