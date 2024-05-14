using System.Text.Json.Serialization;

namespace Schematic.Client;

public class RuleConditionResourceResponseData
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}
