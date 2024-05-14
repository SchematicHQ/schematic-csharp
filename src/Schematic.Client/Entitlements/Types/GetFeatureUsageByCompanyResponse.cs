using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

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
