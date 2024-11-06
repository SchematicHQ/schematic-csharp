using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListPaymentMethodsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<PaymentMethodResponseData> Data { get; set; } =
        new List<PaymentMethodResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListPaymentMethodsParams Params { get; set; }
}
