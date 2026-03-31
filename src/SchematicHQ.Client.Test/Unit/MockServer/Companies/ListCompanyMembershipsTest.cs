using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListCompanyMembershipsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "company": {
                    "created_at": "2024-01-15T09:30:00.000Z",
                    "environment_id": "environment_id",
                    "id": "id",
                    "name": "name",
                    "updated_at": "2024-01-15T09:30:00.000Z"
                  },
                  "company_id": "company_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "id": "id",
                  "updated_at": "2024-01-15T09:30:00.000Z",
                  "user_id": "user_id"
                }
              ],
              "params": {
                "company_id": "company_id",
                "limit": 1000000,
                "offset": 1000000,
                "user_id": "user_id"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/company-memberships")
                    .WithParam("company_id", "company_id")
                    .WithParam("user_id", "user_id")
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

        var response = await Client.Companies.ListCompanyMembershipsAsync(
            new ListCompanyMembershipsRequest
            {
                CompanyId = "company_id",
                UserId = "user_id",
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
