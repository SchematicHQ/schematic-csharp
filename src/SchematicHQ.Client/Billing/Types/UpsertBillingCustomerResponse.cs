using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record UpsertBillingCustomerResponse
{
    [JsonPropertyName("data")]
    public required BillingCustomerResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; } = new Dictionary<string, object>();
}
