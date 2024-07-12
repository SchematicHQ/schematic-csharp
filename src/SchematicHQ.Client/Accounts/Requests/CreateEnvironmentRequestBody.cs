using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CreateEnvironmentRequestBody
{
    [JsonPropertyName("environment_type")]
    public required CreateEnvironmentRequestBodyEnvironmentType EnvironmentType { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
