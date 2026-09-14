using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ReserveCreditsRequestBody
{
    /// <summary>
    /// Credits to hold for the operation. The full amount must be available; a partial hold is never taken
    /// </summary>
    [JsonPropertyName("amount")]
    public required double Amount { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("credit_type_id")]
    public required string CreditTypeId { get; set; }

    /// <summary>
    /// When the hold lapses if no track event settles it; defaults to one minute from now and may be at most one hour out. The unspent hold is refunded on expiry
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// A caller-chosen key for safe retries: a second request with the same key returns the original reservation instead of taking another hold
    /// </summary>
    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
