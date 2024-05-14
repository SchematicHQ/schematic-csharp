using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListCompanyPlansResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<CompanyPlanResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListCompanyPlansParams Params { get; init; }
}
