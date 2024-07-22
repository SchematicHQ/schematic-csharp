using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EntityTraitValue
{
    [JsonPropertyName("definition_id")]
    public required string DefinitionId { get; init; }

    [JsonPropertyName("value")]
    public required string Value { get; init; }
}
