using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record FeatureUsageDetailResponseData
{
    [JsonPropertyName("features")]
    public IEnumerable<FeatureUsageResponseData> Features { get; init; } =
        new List<FeatureUsageResponseData>();
}
