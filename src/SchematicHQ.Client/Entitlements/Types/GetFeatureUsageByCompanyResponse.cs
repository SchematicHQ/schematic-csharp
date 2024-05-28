using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class GetFeatureUsageByCompanyResponse
{
    [JsonPropertyName("data")]
    public FeatureUsageDetailResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public GetFeatureUsageByCompanyParams Params { get; init; }
}
