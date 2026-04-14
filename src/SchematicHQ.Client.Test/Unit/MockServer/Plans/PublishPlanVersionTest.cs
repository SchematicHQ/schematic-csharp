using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plans;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class PublishPlanVersionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "excluded_company_ids": [
                "excluded_company_ids"
              ],
              "migration_strategy": "immediate",
              "pay_in_advance": [
                {
                  "price_id": "price_id",
                  "quantity": 1000000
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "created_at": "2024-01-15T09:30:00.000Z",
                "description": "description",
                "environment_id": "environment_id",
                "icon": "amber",
                "id": "id",
                "name": "name",
                "original_plan_id": "original_plan_id",
                "plan_type": "plan",
                "status": "published",
                "updated_at": "2024-01-15T09:30:00.000Z",
                "version": 1000000
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
                    .WithPath("/plans/version/plan_id/publish")
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

        var response = await Client.Plans.PublishPlanVersionAsync(
            "plan_id",
            new PublishPlanVersionRequestBody
            {
                ExcludedCompanyIds = new List<string>() { "excluded_company_ids" },
                MigrationStrategy = PlanVersionMigrationStrategy.Immediate,
                PayInAdvance = new List<UpdatePayInAdvanceRequestBody>()
                {
                    new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
