using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record RenewalDestinationRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The billing period the company moves to. Optional when the plan has one price in the custom plan's currency; required when it has several.
    /// </summary>
    [JsonPropertyName("billing_period")]
    public PlanPriceCadence? BillingPeriod { get; set; }

    /// <summary>
    /// A published standard base plan in the plan group, priced in the custom plan's currency.
    /// </summary>
    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

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
