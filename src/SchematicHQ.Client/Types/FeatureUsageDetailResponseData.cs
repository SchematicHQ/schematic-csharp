using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record FeatureUsageDetailResponseData
{
    [JsonPropertyName("features")]
    public IEnumerable<FeatureUsageResponseData> Features { get; set; } =
        new List<FeatureUsageResponseData>();
}
