using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Features;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CheckFlagsBulkTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "contexts": [
                {}
              ]
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "data": [
                  {
                    "flags": [
                      {
                        "flag": "flag",
                        "reason": "reason",
                        "value": true
                      }
                    ]
                  }
                ]
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
                    .WithPath("/flags/check-bulk")
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

        var response = await Client.Features.CheckFlagsBulkAsync(
            new CheckFlagsBulkRequestBody
            {
                Contexts = new List<CheckFlagRequestBody>() { new CheckFlagRequestBody() },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
