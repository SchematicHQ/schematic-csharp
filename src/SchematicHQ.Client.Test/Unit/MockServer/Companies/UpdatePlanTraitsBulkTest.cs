using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdatePlanTraitsBulkTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "apply_to_existing_companies": true,
              "plan_id": "plan_id",
              "traits": [
                {
                  "trait_id": "trait_id",
                  "trait_value": "trait_value"
                }
              ]
            }
            """;

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
                "key": "value"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plan-traits/bulk")
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

        var response = await Client.Companies.UpdatePlanTraitsBulkAsync(
            new UpdatePlanTraitBulkRequestBody
            {
                ApplyToExistingCompanies = true,
                PlanId = "plan_id",
                Traits = new List<UpdatePlanTraitTraitRequestBody>()
                {
                    new UpdatePlanTraitTraitRequestBody
                    {
                        TraitId = "trait_id",
                        TraitValue = "trait_value",
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
