using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListFeatureUsersResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<FeatureCompanyUserResponseData> Data { get; set; } =
        new List<FeatureCompanyUserResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListFeatureUsersParams Params { get; set; }
}
