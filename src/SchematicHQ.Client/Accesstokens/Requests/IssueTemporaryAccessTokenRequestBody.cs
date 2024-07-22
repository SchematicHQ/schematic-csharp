using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record IssueTemporaryAccessTokenRequestBody
{
    [JsonPropertyName("lookup")]
    public Dictionary<string, string> Lookup { get; init; } = new Dictionary<string, string>();

    [JsonPropertyName("resource_type")]
    public required string ResourceType { get; init; }
}
