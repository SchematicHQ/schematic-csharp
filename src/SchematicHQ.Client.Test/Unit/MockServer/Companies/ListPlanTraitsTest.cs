using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListPlanTraitsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "account_id": "account_id",
                  "created_at": "2024-01-15T09:30:00.000Z",
                  "environment_id": "environment_id",
                  "id": "id",
                  "plan_id": "plan_id",
                  "plan_type": "plan_type",
                  "trait_id": "trait_id",
                  "trait_value": "trait_value",
                  "updated_at": "2024-01-15T09:30:00.000Z"
                }
              ],
              "params": {
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "offset": 1000000,
                "plan_id": "plan_id",
                "trait_id": "trait_id",
                "trait_ids": [
                  "trait_ids"
                ]
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plan-traits")
                    .WithParam("plan_id", "plan_id")
                    .WithParam("trait_id", "trait_id")
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

        var response = await Client.Companies.ListPlanTraitsAsync(
            new ListPlanTraitsRequest
            {
                Ids = new List<string>() { "ids" },
                PlanId = "plan_id",
                TraitId = "trait_id",
                TraitIds = new List<string>() { "trait_ids" },
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
