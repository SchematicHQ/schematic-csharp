using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListCompanyOverridesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<CompanyOverrideResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListCompanyOverridesParams Params { get; init; }
}
