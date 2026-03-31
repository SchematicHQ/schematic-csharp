using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetPlanTraitTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "account_id": "account_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "environment_id": "environment_id",
                "id": "id",
                "plan_id": "plan_id",
                "plan_type": "plan_type",
                "trait_id": "trait_id",
                "trait_value": "trait_value",
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
                    .WithPath("/plan-traits/plan_trait_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Companies.GetPlanTraitAsync("plan_trait_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
