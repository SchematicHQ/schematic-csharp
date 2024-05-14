using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListCompaniesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<CompanyDetailResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListCompaniesParams Params { get; init; }
}
