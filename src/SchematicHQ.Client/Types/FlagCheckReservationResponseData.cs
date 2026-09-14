using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record FlagCheckReservationResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    /// <summary>
    /// Credits per unit of usage the hold was priced at
    /// </summary>
    [JsonPropertyName("consumption_rate")]
    public required double ConsumptionRate { get; set; }

    [JsonPropertyName("credit_type_id")]
    public required string CreditTypeId { get; set; }

    /// <summary>
    /// Credits held from the company's balance
    /// </summary>
    [JsonPropertyName("credits_reserved")]
    public required double CreditsReserved { get; set; }

    /// <summary>
    /// The event subtype the settling track event should carry
    /// </summary>
    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    /// <summary>
    /// When the unspent hold is refunded if no track event settles it
    /// </summary>
    [JsonPropertyName("expires_at")]
    public required DateTime ExpiresAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Units of usage the hold covers, as requested
    /// </summary>
    [JsonPropertyName("quantity_reserved")]
    public required double QuantityReserved { get; set; }

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
