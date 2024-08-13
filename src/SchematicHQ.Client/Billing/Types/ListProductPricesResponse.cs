using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record ListProductPricesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<BillingPriceResponseData> Data { get; init; } =
        new List<BillingPriceResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListProductPricesParams Params { get; init; }
}
