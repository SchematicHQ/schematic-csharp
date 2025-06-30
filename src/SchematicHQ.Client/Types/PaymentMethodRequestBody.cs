using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PaymentMethodRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("account_last4")]
    public string? AccountLast4 { get; set; }

    [JsonPropertyName("account_name")]
    public string? AccountName { get; set; }

    [JsonPropertyName("bank_name")]
    public string? BankName { get; set; }

    [JsonPropertyName("billing_email")]
    public string? BillingEmail { get; set; }

    [JsonPropertyName("billing_name")]
    public string? BillingName { get; set; }

    [JsonPropertyName("card_brand")]
    public string? CardBrand { get; set; }

    [JsonPropertyName("card_exp_month")]
    public int? CardExpMonth { get; set; }

    [JsonPropertyName("card_exp_year")]
    public int? CardExpYear { get; set; }

    [JsonPropertyName("card_last4")]
    public string? CardLast4 { get; set; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; set; }

    [JsonPropertyName("payment_method_type")]
    public required string PaymentMethodType { get; set; }

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
