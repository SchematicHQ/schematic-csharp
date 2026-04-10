using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Features;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CheckFlagsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {}
            """;

        const string mockResponse = """
            {
              "data": {
                "flags": [
                  {
                    "flag": "flag",
                    "reason": "reason",
                    "value": true
                  }
                ],
                "plan": {
                  "id": "id",
                  "name": "name",
                  "trial_end_date": "2024-01-15T09:30:00.000Z",
                  "trial_status": "active"
                }
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
                    .WithPath("/flags/check")
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

        var response = await Client.Features.CheckFlagsAsync(new CheckFlagRequestBody());
        JsonAssert.AreEqual(response, mockResponse);
    }
}
