using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PreflightRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Cost in credits of the action, keyed by credit ID, for callers that have already computed it. Takes precedence over event_quantities, event_usage and usage on credit balance conditions for the same credit. A cost of zero means the action is free, not that the input is absent
    /// </summary>
    [JsonPropertyName("credit_cost")]
    public Dictionary<string, double>? CreditCost { get; set; }

    /// <summary>
    /// An event of a specific subtype, priced the way it would burn credits: quantity at the entitlement's consumption rate, plus each of quantities at its rate in the entitlement's quantity rates. Use it to preflight an inference call by its request and token counts. Applies only to credit balance conditions whose event subtype matches, where it takes precedence over event_usage and usage; credit_cost for the same credit still wins. Not supported on check-and-reserve
    /// </summary>
    [JsonPropertyName("event_quantities")]
    public PreflightEventQuantitiesRequestBody? EventQuantities { get; set; }

    /// <summary>
    /// Usage of a specific event subtype. Preferred over usage when the subtype is known, since it only affects conditions measuring that subtype
    /// </summary>
    [JsonPropertyName("event_usage")]
    public PreflightEventUsageRequestBody? EventUsage { get; set; }

    /// <summary>
    /// Quantity of usage to simulate against any numeric condition encountered while evaluating the flag. Zero has no effect
    /// </summary>
    [JsonPropertyName("usage")]
    public long? Usage { get; set; }

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
