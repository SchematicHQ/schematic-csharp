using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record GetFeatureUsageByCompanyResponse
{
    [JsonPropertyName("data")]
    public required FeatureUsageDetailResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required GetFeatureUsageByCompanyParams Params { get; init; }
}
