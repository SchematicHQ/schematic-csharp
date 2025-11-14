using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateComponentRequestBody
{
    /// <summary>
    /// component_id
    /// </summary>
    [JsonIgnore]
    public required string ComponentId { get; set; }

    [JsonPropertyName("ast")]
    public Dictionary<string, double>? Ast { get; set; }

    [JsonPropertyName("entity_type")]
    public UpdateComponentRequestBodyEntityType? EntityType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("state")]
    public UpdateComponentRequestBodyState? State { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
