using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdateEnvironmentRequestBody
{
    [JsonPropertyName("environment_type")]
    public UpdateEnvironmentRequestBodyEnvironmentType? EnvironmentType { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
