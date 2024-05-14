using System.Text.Json.Serialization;

namespace Schematic.Client;

public class GetFeatureUsageByCompanyParams
{
    [JsonPropertyName("keys")]
    public Dictionary<string, object>? Keys { get; init; }
}
