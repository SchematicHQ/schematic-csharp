using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyBillingDetailsView : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("address")]
    public CompanyBillingAddressView? Address { get; set; }

    [JsonPropertyName("checkout_settings")]
    public required CompanyBillingCheckoutSettings CheckoutSettings { get; set; }

    [JsonPropertyName("custom_fields")]
    public IEnumerable<CheckoutFieldWithValue> CustomFields { get; set; } =
        new List<CheckoutFieldWithValue>();

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("tax_ids")]
    public IEnumerable<CompanyTaxIdView> TaxIds { get; set; } = new List<CompanyTaxIdView>();

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
