using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CreateEnvironmentRequestBody
{
    [JsonPropertyName("environment_type")]
    public required CreateEnvironmentRequestBodyEnvironmentType EnvironmentType { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
