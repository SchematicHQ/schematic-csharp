using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record IssueTemporaryAccessTokenRequestBody
{
    [JsonPropertyName("lookup")]
    public Dictionary<string, string> Lookup { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("resource_type")]
    public required string ResourceType { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
