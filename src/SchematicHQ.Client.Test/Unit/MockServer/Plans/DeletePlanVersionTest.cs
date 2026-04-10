using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Plans;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class DeletePlanVersionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "deleted": true
              },
              "params": {
                "promote_archived_version": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/plans/version/plan_id")
                    .UsingDelete()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Plans.DeletePlanVersionAsync(
            "plan_id",
            new DeletePlanVersionRequest { PromoteArchivedVersion = true }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
