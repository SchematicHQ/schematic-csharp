using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CreateComponentRequestBody
{
    [JsonPropertyName("ast")]
    public IEnumerable<int> Ast { get; init; } = new List<int>();

    [JsonPropertyName("entity_type")]
    public required CreateComponentRequestBodyEntityType EntityType { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
