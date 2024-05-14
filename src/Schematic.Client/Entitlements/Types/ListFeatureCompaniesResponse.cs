using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListFeatureCompaniesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<FeatureCompanyResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListFeatureCompaniesParams Params { get; init; }
}
