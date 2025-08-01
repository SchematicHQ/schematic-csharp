using SchematicHQ.Client.RulesEngine.Utils;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.RulesEngine.Models
{
    public class TraitDefinition
    {
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    
    [JsonPropertyName("comparable_type")]
    public ComparableType ComparableType { get; set; }
    
    [JsonPropertyName("entity_type")]
    public EntityType EntityType { get; set; }
    
    }
}