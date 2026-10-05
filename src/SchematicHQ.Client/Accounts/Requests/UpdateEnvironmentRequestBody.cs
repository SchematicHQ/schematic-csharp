using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateEnvironmentRequestBody
{
    [JsonPropertyName("environment_type")]
    public EnvironmentType? EnvironmentType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("require_context_signature")]
    public bool? RequireContextSignature { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
