using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record InvoiceRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("amount_due")]
    public required int AmountDue { get; set; }

    [JsonPropertyName("amount_paid")]
    public required int AmountPaid { get; set; }

    [JsonPropertyName("amount_remaining")]
    public required int AmountRemaining { get; set; }

    [JsonPropertyName("collection_method")]
    public required string CollectionMethod { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; set; }

    [JsonPropertyName("due_date")]
    public DateTime? DueDate { get; set; }

    [JsonPropertyName("payment_method_external_id")]
    public string? PaymentMethodExternalId { get; set; }

    [JsonPropertyName("subscription_external_id")]
    public string? SubscriptionExternalId { get; set; }

    [JsonPropertyName("subtotal")]
    public required int Subtotal { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

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
