using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListFeatureCompaniesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<FeatureCompanyResponseData> Data { get; set; } =
        new List<FeatureCompanyResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListFeatureCompaniesParams Params { get; set; }
}
