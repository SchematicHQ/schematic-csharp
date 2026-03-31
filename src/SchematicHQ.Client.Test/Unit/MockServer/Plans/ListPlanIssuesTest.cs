using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plans;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ListPlanIssuesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "code": "code",
                  "description": "description",
                  "detail": "detail",
                  "id": "id"
                }
              ],
              "params": {
                "plan_id": "plan_id",
                "plan_version_id": "plan_version_id"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plans/issues")
                    .WithParam("plan_id", "plan_id")
                    .WithParam("plan_version_id", "plan_version_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Plans.ListPlanIssuesAsync(
            new ListPlanIssuesRequest { PlanId = "plan_id", PlanVersionId = "plan_version_id" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
