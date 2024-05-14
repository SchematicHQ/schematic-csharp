using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListProductsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<BillingProductResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListProductsParams Params { get; init; }
}
