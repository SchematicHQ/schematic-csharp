using System.Text.Json.Serialization;

namespace SchematicHQ.Client.RulesEngine.Models
{
  public class Trait
  {

    [JsonPropertyName("trait_definition")]
    public TraitDefinition? TraitDefinition { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }

  }
}