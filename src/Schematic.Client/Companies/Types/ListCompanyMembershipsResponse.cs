using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListCompanyMembershipsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<CompanyMembershipDetailResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListCompanyMembershipsParams Params { get; init; }
}
