using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdatePlanTraitTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "plan_id": "plan_id",
              "trait_value": "trait_value"
            }
            """;

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
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Companies.UpdatePlanTraitAsync(
            "plan_trait_id",
            new UpdatePlanTraitRequestBody { PlanId = "plan_id", TraitValue = "trait_value" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
