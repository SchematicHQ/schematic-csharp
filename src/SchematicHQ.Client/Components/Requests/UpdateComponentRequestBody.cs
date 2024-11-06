using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateComponentRequestBody
{
    [JsonPropertyName("ast")]
    public Dictionary<string, double>? Ast { get; set; }

    [JsonPropertyName("entity_type")]
    public UpdateComponentRequestBodyEntityType? EntityType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("state")]
    public UpdateComponentRequestBodyState? State { get; set; }
}
