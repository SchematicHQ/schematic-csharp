using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class FeatureUsageDetailResponseData
{
    [JsonPropertyName("features")]
    public List<FeatureUsageResponseData> Features { get; init; }
}
