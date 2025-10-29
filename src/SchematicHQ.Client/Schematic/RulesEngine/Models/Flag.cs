using System.Text.Json.Serialization;

namespace Schematic.RulesEngine.Models
{
    public class Flag
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("account_id")]
        public required string AccountId { get; set; }

        [JsonPropertyName("environment_id")]
        public required string EnvironmentId { get; set; }

        [JsonPropertyName("key")]
        public required string Key { get; set; }

        [JsonPropertyName("rules")]
        public List<global::Schematic.RulesEngine.Models.Rule> Rules { get; set; } = new List<global::Schematic.RulesEngine.Models.Rule>();

        [JsonPropertyName("default_value")]
        public bool DefaultValue { get; set; }
    }
}