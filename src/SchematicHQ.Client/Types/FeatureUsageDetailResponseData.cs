using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record FeatureUsageDetailResponseData
{
    [JsonPropertyName("features")]
    public IEnumerable<FeatureUsageResponseData> Features { get; set; } =
        new List<FeatureUsageResponseData>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
