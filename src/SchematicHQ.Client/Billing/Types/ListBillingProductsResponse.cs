using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record ListBillingProductsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<BillingProductDetailResponseData> Data { get; init; } =
        new List<BillingProductDetailResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListBillingProductsParams Params { get; init; }
}
