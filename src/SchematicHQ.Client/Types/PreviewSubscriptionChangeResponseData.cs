using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PreviewSubscriptionChangeResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("amount_off")]
    public required long AmountOff { get; set; }

    [JsonPropertyName("due_now")]
    public required long DueNow { get; set; }

    [JsonPropertyName("finance")]
    public PreviewSubscriptionFinanceResponseData? Finance { get; set; }

    [JsonPropertyName("is_scheduled_downgrade")]
    public required bool IsScheduledDowngrade { get; set; }

    [JsonPropertyName("new_charges")]
    public required long NewCharges { get; set; }

    [JsonPropertyName("payment_method_required")]
    public required bool PaymentMethodRequired { get; set; }

    [JsonPropertyName("percent_off")]
    public required double PercentOff { get; set; }

    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    [JsonPropertyName("promo_code_applied")]
    public required bool PromoCodeApplied { get; set; }

    [JsonPropertyName("proration")]
    public required long Proration { get; set; }

    [JsonPropertyName("scheduled_change_time")]
    public DateTime? ScheduledChangeTime { get; set; }

    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; set; }

    [JsonPropertyName("usage_violations")]
    public IEnumerable<FeatureUsageResponseData> UsageViolations { get; set; } =
        new List<FeatureUsageResponseData>();

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
