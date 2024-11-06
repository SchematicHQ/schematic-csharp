using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListCrmProductsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<CrmProductResponseData> Data { get; set; } =
        new List<CrmProductResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListCrmProductsParams Params { get; set; }
}
