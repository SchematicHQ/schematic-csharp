using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateApiKeyRequestBody
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
