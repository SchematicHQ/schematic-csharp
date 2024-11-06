using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListCustomersResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<BillingCustomerWithSubscriptionsResponseData> Data { get; set; } =
        new List<BillingCustomerWithSubscriptionsResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListCustomersParams Params { get; set; }
}
