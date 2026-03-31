using NUnit.Framework;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Accounts;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetAuditLogTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "actor_type": "api_key",
                "api_key_id": "api_key_id",
                "ended_at": "2024-01-15T09:30:00.000Z",
                "environment_id": "environment_id",
                "id": "id",
                "method": "method",
                "req_body": "req_body",
                "resource_id": 1000000,
                "resource_id_string": "resource_id_string",
                "resource_name": "resource_name",
                "resource_type": "resource_type",
                "resp_body": "resp_body",
                "resp_code": 1000000,
                "secondary_resource": "secondary_resource",
                "started_at": "2024-01-15T09:30:00.000Z",
                "url": "url",
                "user_id": "user_id",
                "user_name": "user_name"
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
                    .WithPath("/audit-log/audit_log_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Accounts.GetAuditLogAsync("audit_log_id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
