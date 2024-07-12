using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateComponentRequestBody
{
    [JsonPropertyName("ast")]
    public IEnumerable<int>? Ast { get; init; }

    [JsonPropertyName("entity_type")]
    public UpdateComponentRequestBodyEntityType? EntityType { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
