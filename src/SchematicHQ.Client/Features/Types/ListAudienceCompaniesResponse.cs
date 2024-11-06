using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListAudienceCompaniesResponse
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
    public Dictionary<string, object?> Params { get; set; } = new Dictionary<string, object?>();
}
