using System.Text.Json.Serialization;

namespace Schematic.Client;

public class EntityTraitValue
{
    [JsonPropertyName("definition_id")]
    public string DefinitionId { get; init; }

    [JsonPropertyName("value")]
    public string Value { get; init; }
}
