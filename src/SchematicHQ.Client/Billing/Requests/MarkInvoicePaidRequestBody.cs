using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record MarkInvoicePaidRequestBody
{
    /// <summary>
    /// Amount paid, in minor units of the invoice's currency. Leave empty to mark the whole remaining amount as paid.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <summary>
    /// A retry with the same key records the payment once.
    /// </summary>
    [JsonPropertyName("idempotency_key")]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// A note that identifies the payment, such as a bank transfer reference.
    /// </summary>
    [JsonPropertyName("reference")]
    public string? Reference { get; set; }

    /// <summary>
    /// When the payment arrived. Defaults to now, and cannot be in the future.
    /// </summary>
    [JsonPropertyName("settled_at")]
    public DateTime? SettledAt { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
