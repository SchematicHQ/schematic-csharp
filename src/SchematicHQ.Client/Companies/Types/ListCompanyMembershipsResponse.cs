using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListCompanyMembershipsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<CompanyMembershipDetailResponseData> Data { get; set; } =
        new List<CompanyMembershipDetailResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListCompanyMembershipsParams Params { get; set; }
}
