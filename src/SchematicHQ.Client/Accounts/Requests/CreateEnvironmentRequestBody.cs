using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateEnvironmentRequestBody
{
    [JsonPropertyName("environment_type")]
    public required CreateEnvironmentRequestBodyEnvironmentType EnvironmentType { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
