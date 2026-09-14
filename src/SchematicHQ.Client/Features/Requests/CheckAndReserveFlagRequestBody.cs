using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CheckAndReserveFlagRequestBody
{
    [JsonPropertyName("company")]
    public Dictionary<string, string>? Company { get; set; }

    /// <summary>
    /// When the hold lapses if no track event settles it; defaults to one minute from now and may be at most one hour out. The unspent hold is refunded on expiry
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// Hypothetical usage to evaluate the flag against. When credit_cost names the entitlement's credit, that cost is what gets held; otherwise the hold is quantity times the entitlement's consumption rate
    /// </summary>
    [JsonPropertyName("preflight")]
    public PreflightRequestBody? Preflight { get; set; }

    /// <summary>
    /// Units of the feature the operation will consume; defaults to 1. Sets the hold size together with the entitlement's consumption rate, and is echoed back on the reservation for the settling track event
    /// </summary>
    [JsonPropertyName("quantity")]
    public double? Quantity { get; set; }

    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
