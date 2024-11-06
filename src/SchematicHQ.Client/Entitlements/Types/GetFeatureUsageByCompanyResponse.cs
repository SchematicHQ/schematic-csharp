using System.Text.Json.Serialization;

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
}
