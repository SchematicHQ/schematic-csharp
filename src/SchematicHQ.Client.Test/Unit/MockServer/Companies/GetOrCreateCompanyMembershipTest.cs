using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetOrCreateCompanyMembershipTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "company_id": "company_id",
              "user_id": "user_id"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "company": {
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "environment_id": "environment_id",
                  "id": "id",
                  "last_seen_at": "2024-01-15T09:30:00.000Z",
                  "logo_url": "logo_url",
                  "name": "name",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                },
                "company_id": "company_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "id": "id",
                "updated_at": "2024-01-15T09:30:00.000Z",
                "user_id": "user_id"
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
                    .WithPath("/company-memberships")
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

        var response = await Client.Companies.GetOrCreateCompanyMembershipAsync(
            new GetOrCreateCompanyMembershipRequestBody
            {
                CompanyId = "company_id",
                UserId = "user_id",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
