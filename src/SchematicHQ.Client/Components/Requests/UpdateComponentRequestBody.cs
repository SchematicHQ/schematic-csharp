using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateComponentRequestBody
{
    [JsonPropertyName("ast")]
    public Dictionary<string, double>? Ast { get; init; }

    [JsonPropertyName("entity_type")]
    public UpdateComponentRequestBodyEntityType? EntityType { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("state")]
    public UpdateComponentRequestBodyState? State { get; init; }
}
