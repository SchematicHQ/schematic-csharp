using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record BillingCustomerSubscription
{
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("metered_usage")]
    public required bool MeteredUsage { get; set; }

    [JsonPropertyName("per_unit_price")]
    public required int PerUnitPrice { get; set; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
