using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ListFeatureUsageResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<FeatureUsageResponseData> Data { get; set; } =
        new List<FeatureUsageResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListFeatureUsageParams Params { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
