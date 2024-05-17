using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class FeatureUsageDetailResponseData
{
    [JsonPropertyName("features")]
    public List<FeatureUsageResponseData> Features { get; init; }
}
