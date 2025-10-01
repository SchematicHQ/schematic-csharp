using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingPlanCreditGrantResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("credit_amount")]
    public required int CreditAmount { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("credit_name")]
    public required string CreditName { get; set; }

    [JsonPropertyName("credit_plural_name")]
    public string? CreditPluralName { get; set; }

    [JsonPropertyName("credit_singular_name")]
    public string? CreditSingularName { get; set; }

    [JsonPropertyName("expiry_type")]
    public string? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public string? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("plan_name")]
    public required string PlanName { get; set; }

    [JsonPropertyName("reset_cadence")]
    public required string ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public required string ResetStart { get; set; }

    [JsonPropertyName("reset_type")]
    public string? ResetType { get; set; }

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
