using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class CreateApiKeyRequestBody
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}
