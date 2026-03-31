using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Dataexports;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CreateDataExportTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "export_type": "company-feature-usage",
              "metadata": "metadata",
              "output_file_type": "csv"
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "account_id": "account_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "environment_id": "environment_id",
                "export_type": "company-feature-usage",
                "id": "id",
                "metadata": "metadata",
                "output_file_type": "csv",
                "status": "failure",
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
                    .WithPath("/data-exports")
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

        var response = await Client.Dataexports.CreateDataExportAsync(
            new CreateDataExportRequestBody
            {
                ExportType = "company-feature-usage",
                Metadata = "metadata",
                OutputFileType = "csv",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
