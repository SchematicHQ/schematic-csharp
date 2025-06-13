using System.Text.Json.Serialization;

namespace RulesEngine.Models
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
        public List<Trait> Traits { get; set; } = new List<Trait>();

    }
}