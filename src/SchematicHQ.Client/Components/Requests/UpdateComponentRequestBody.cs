using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateComponentRequestBody
{
    [JsonPropertyName("ast")]
    public Dictionary<string, double>? Ast { get; set; }

    [JsonPropertyName("entity_type")]
    public ComponentEntityType? EntityType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("state")]
    public ComponentState? State { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
