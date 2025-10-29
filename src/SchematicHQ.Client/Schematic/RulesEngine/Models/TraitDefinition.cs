using Schematic.RulesEngine.Utils;
using System.Text.Json.Serialization;

namespace Schematic.RulesEngine.Models
{
    public class TraitDefinition
    {
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    
    [JsonPropertyName("comparable_type")]
    public Schematic.RulesEngine.Utils.ComparableType ComparableType { get; set; }

    [JsonPropertyName("entity_type")]
    public Schematic.RulesEngine.EntityType EntityType { get; set; }
    
    }
}