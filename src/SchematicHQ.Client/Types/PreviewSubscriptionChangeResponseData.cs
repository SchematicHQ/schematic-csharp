using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// The requested resource
/// </summary>
public record PreviewSubscriptionChangeResponseData
{
    [JsonPropertyName("amount_off")]
    public required int AmountOff { get; set; }

    [JsonPropertyName("due_now")]
    public required int DueNow { get; set; }

    [JsonPropertyName("new_charges")]
    public required int NewCharges { get; set; }

    [JsonPropertyName("percent_off")]
    public required double PercentOff { get; set; }

    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    [JsonPropertyName("promo_code_applied")]
    public required bool PromoCodeApplied { get; set; }

    [JsonPropertyName("proration")]
    public required int Proration { get; set; }

    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
