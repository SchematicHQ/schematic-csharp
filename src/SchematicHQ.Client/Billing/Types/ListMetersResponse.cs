using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListMetersResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<BillingMeterResponseData> Data { get; set; } =
        new List<BillingMeterResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListMetersParams Params { get; set; }
}
