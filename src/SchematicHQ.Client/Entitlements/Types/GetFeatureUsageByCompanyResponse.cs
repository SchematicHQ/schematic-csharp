using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record GetFeatureUsageByCompanyResponse
{
    [JsonPropertyName("data")]
    public required FeatureUsageDetailResponseData Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required GetFeatureUsageByCompanyParams Params { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
