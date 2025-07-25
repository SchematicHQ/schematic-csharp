using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateEnvironmentRequestBody
{
    [JsonPropertyName("environment_type")]
    public UpdateEnvironmentRequestBodyEnvironmentType? EnvironmentType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
