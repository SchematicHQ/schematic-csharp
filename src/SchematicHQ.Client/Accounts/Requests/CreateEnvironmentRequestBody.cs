using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CreateEnvironmentRequestBody
{
    [JsonPropertyName("environment_type")]
    public CreateEnvironmentRequestBodyEnvironmentType EnvironmentType { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}
