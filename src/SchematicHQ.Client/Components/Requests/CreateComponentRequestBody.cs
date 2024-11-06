using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateComponentRequestBody
{
    [JsonPropertyName("ast")]
    public Dictionary<string, double>? Ast { get; set; }

    [JsonPropertyName("entity_type")]
    public required CreateComponentRequestBodyEntityType EntityType { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
