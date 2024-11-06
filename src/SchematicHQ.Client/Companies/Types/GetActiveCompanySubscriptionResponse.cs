using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GetActiveCompanySubscriptionResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<CompanySubscriptionResponseData> Data { get; set; } =
        new List<CompanySubscriptionResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required GetActiveCompanySubscriptionParams Params { get; set; }
}
