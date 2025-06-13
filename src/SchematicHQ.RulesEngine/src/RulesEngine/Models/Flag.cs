using System.Text.Json.Serialization;

namespace RulesEngine.Models
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
        public List<Rule> Rules { get; set; } = new List<Rule>();

        [JsonPropertyName("default_value")]
        public bool DefaultValue { get; set; }
    }
}