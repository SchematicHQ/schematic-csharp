using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListCompanyOverridesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<CompanyOverrideResponseData> Data { get; set; } =
        new List<CompanyOverrideResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListCompanyOverridesParams Params { get; set; }
}
