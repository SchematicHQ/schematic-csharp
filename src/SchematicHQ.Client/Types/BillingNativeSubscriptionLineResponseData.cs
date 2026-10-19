using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingNativeSubscriptionLineResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("anchor")]
    public required DateTime Anchor { get; set; }

    /// <summary>
    /// The day of the month the line's periods start on.
    /// </summary>
    [JsonPropertyName("anchor_day")]
    public required long AnchorDay { get; set; }

    /// <summary>
    /// Whether the subscription takes its billing period from this line.
    /// </summary>
    [JsonPropertyName("base")]
    public required bool Base { get; set; }

    [JsonPropertyName("cadence_count")]
    public required long CadenceCount { get; set; }

    [JsonPropertyName("cadence_unit")]
    public required BillingProductPriceInterval CadenceUnit { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    /// <summary>
    /// Null on a line that hasn't started.
    /// </summary>
    [JsonPropertyName("current_period_end")]
    public DateTime? CurrentPeriodEnd { get; set; }

    /// <summary>
    /// Null on a line that hasn't started.
    /// </summary>
    [JsonPropertyName("current_period_start")]
    public DateTime? CurrentPeriodStart { get; set; }

    [JsonPropertyName("effective_from")]
    public required DateTime EffectiveFrom { get; set; }

    [JsonPropertyName("effective_to")]
    public DateTime? EffectiveTo { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("kind")]
    public required LineKind Kind { get; set; }

    [JsonPropertyName("quantity")]
    public required long Quantity { get; set; }

    /// <summary>
    /// The plan, add-on or plan credit grant the line compiles from.
    /// </summary>
    [JsonPropertyName("source_id")]
    public required string SourceId { get; set; }

    /// <summary>
    /// The name of the plan, add-on or credit the line compiles from.
    /// </summary>
    [JsonPropertyName("source_name")]
    public string? SourceName { get; set; }

    /// <summary>
    /// Set when the line compiles from a plan or add-on.
    /// </summary>
    [JsonPropertyName("source_plan_type")]
    public PlanType? SourcePlanType { get; set; }

    [JsonPropertyName("taxable")]
    public required bool Taxable { get; set; }

    [JsonPropertyName("tiers_mode")]
    public BillingTiersMode? TiersMode { get; set; }

    [JsonPropertyName("timing")]
    public required ChargeTiming Timing { get; set; }

    /// <summary>
    /// In minor units. Null on a rate line or a tiered price.
    /// </summary>
    [JsonPropertyName("unit_amount")]
    public long? UnitAmount { get; set; }

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
