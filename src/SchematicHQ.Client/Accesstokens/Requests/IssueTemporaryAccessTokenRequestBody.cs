using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record IssueTemporaryAccessTokenRequestBody
{
    [JsonPropertyName("lookup")]
    public Dictionary<string, string> Lookup { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("resource_type")]
    public string ResourceType { get; set; } = "company";

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
