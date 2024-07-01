using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class FeatureUsageDetailResponseData
{
    [JsonPropertyName("features")]
    public IEnumerable<FeatureUsageResponseData> Features { get; init; }
}
