using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class GetActiveCompanySubscriptionResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<CompanySubscriptionResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public GetActiveCompanySubscriptionParams Params { get; init; }
}
