using System.Text.Json.Serialization;

namespace Schematic.RulesEngine.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("account_id")]
        public required string AccountId { get; set; }

        [JsonPropertyName("environment_id")]
        public required string EnvironmentId { get; set; }

        [JsonPropertyName("keys")]
        public IDictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("traits")]
        public List<global::Schematic.RulesEngine.Models.Trait> Traits { get; set; } = new List<global::Schematic.RulesEngine.Models.Trait>();

        [JsonPropertyName("rules")]
        public List<global::Schematic.RulesEngine.Models.Rule> Rules { get; set; } = new List<global::Schematic.RulesEngine.Models.Rule>();

    }
}