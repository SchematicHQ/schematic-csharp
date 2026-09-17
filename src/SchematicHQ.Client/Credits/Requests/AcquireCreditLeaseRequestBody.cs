using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record AcquireCreditLeaseRequestBody
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("credit_type_id")]
    public required string CreditTypeId { get; set; }

    /// <summary>
    /// When the hold lapses if the lease is never released; defaults to five minutes from now and may be at most one hour out. The unspent hold is refunded on expiry
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    [JsonPropertyName("requested_amount")]
    public required double RequestedAmount { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
