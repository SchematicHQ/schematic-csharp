using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountPlanTraitsTest : BaseMockServerTest
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
                    .WithPath("/plan-traits/count")
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

        var response = await Client.Companies.CountPlanTraitsAsync(
            new CountPlanTraitsRequest
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
