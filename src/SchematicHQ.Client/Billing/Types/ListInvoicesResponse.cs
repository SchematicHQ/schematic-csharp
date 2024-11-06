using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListInvoicesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<InvoiceResponseData> Data { get; set; } = new List<InvoiceResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListInvoicesParams Params { get; set; }
}
