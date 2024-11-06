using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record RuleConditionResourceResponseData
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
