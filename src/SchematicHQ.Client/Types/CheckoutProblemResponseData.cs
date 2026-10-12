using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CheckoutProblemResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Whether completing the checkout would be refused while this stands.
    /// </summary>
    [JsonPropertyName("blocking")]
    public required bool Blocking { get; set; }

    [JsonPropertyName("bundle_id")]
    public string? BundleId { get; set; }

    [JsonPropertyName("code")]
    public required CheckoutProblemCode Code { get; set; }

    /// <summary>
    /// The currency this problem is about, when it is about one.
    /// </summary>
    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    /// <summary>
    /// Diagnostic detail for operators, including what the billing provider said. Never shown to a customer.
    /// </summary>
    [JsonPropertyName("detail")]
    public string? Detail { get; set; }

    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; set; }

    [JsonPropertyName("field_id")]
    public string? FieldId { get; set; }

    /// <summary>
    /// What is wrong, in words safe for anyone allowed to read the checkout.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("payment_method_id")]
    public string? PaymentMethodId { get; set; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    [JsonPropertyName("price_id")]
    public string? PriceId { get; set; }

    [JsonPropertyName("source")]
    public required CheckoutProblemSource Source { get; set; }

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
