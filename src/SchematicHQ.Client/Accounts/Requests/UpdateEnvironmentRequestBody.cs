using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateEnvironmentRequestBody
{
    [JsonPropertyName("environment_type")]
    public UpdateEnvironmentRequestBodyEnvironmentType? EnvironmentType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
