using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListCompaniesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<CompanyDetailResponseData> Data { get; set; } =
        new List<CompanyDetailResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListCompaniesParams Params { get; set; }
}
