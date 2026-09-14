using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditReservationResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("credit_type_id")]
    public required string CreditTypeId { get; set; }

    /// <summary>
    /// When the unspent hold is refunded if no track event settles it
    /// </summary>
    [JsonPropertyName("expires_at")]
    public required DateTime ExpiresAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// When the reservation was settled, released, or expired; null while the hold is open
    /// </summary>
    [JsonPropertyName("released_at")]
    public DateTime? ReleasedAt { get; set; }

    /// <summary>
    /// Credits held from the company's balance for the operation
    /// </summary>
    [JsonPropertyName("reserved_amount")]
    public required double ReservedAmount { get; set; }

    /// <summary>
    /// Credits the settling track event recorded against the hold; zero until settled. May exceed reserved_amount, in which case the overage was debited from the balance, up to the overdraft limit when one is set
    /// </summary>
    [JsonPropertyName("settled_amount")]
    public required double SettledAmount { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
