using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ListFeaturesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<FeatureDetailResponseData> Data { get; set; } =
        new List<FeatureDetailResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListFeaturesParams Params { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
