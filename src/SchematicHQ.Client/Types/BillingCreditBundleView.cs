using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingCreditBundleView : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("billing_invoice_id")]
    public string? BillingInvoiceId { get; set; }

    [JsonPropertyName("bundle_type")]
    public required string BundleType { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("credit_description")]
    public string? CreditDescription { get; set; }

    [JsonPropertyName("credit_icon")]
    public string? CreditIcon { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("credit_name")]
    public required string CreditName { get; set; }

    [JsonPropertyName("expiry_type")]
    public required string ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public required string ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("has_grants")]
    public required bool HasGrants { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("plural_name")]
    public string? PluralName { get; set; }

    [JsonPropertyName("price")]
    public BillingProductPriceResponseData? Price { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("singular_name")]
    public string? SingularName { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("unit_price")]
    public BillingProductPriceResponseData? UnitPrice { get; set; }

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
