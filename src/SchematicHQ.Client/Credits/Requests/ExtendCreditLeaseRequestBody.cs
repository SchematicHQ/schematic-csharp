using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ExtendCreditLeaseRequestBody
{
    [JsonPropertyName("additional_amount")]
    public required double AdditionalAmount { get; set; }

    /// <summary>
    /// Pushes the lease's expiry out; may be at most one hour from now. Leave unset to keep the expiry the lease already has
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// A caller-chosen key for safe retries: a second request with the same key returns the lease as it stands instead of growing it again. Keys are unique per environment across every extend
    /// </summary>
    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
