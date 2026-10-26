using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CatalogCreditGrantResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Flat credit amount granted to the company regardless of scaling.
    /// </summary>
    [JsonPropertyName("company_credit_amount")]
    public required long CompanyCreditAmount { get; set; }

    /// <summary>
    /// Credits granted; for per_license scaling this is the amount per license unit.
    /// </summary>
    [JsonPropertyName("credit_amount")]
    public required long CreditAmount { get; set; }

    [JsonPropertyName("credit_icon")]
    public string? CreditIcon { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("credit_name")]
    public required string CreditName { get; set; }

    [JsonPropertyName("credit_plural_name")]
    public string? CreditPluralName { get; set; }

    [JsonPropertyName("credit_singular_name")]
    public string? CreditSingularName { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("license_id")]
    public string? LicenseId { get; set; }

    [JsonPropertyName("reset_cadence")]
    public BillingPlanCreditGrantResetCadence? ResetCadence { get; set; }

    [JsonPropertyName("scaling")]
    public required PlanCreditGrantScaling Scaling { get; set; }

    /// <summary>
    /// Whether the catalog shows this credit on the plan. Display-only: a hidden grant is still granted and priced at checkout.
    /// </summary>
    [JsonPropertyName("visible")]
    public required bool Visible { get; set; }

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
