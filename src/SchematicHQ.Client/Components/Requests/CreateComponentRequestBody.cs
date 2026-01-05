using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateComponentRequestBody
{
    [JsonPropertyName("ast")]
    public Dictionary<string, double>? Ast { get; set; }

    [JsonPropertyName("entity_type")]
    public required ComponentEntityType EntityType { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
