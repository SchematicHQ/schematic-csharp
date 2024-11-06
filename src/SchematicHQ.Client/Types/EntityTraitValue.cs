using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EntityTraitValue
{
    [JsonPropertyName("definition_id")]
    public required string DefinitionId { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }
}
