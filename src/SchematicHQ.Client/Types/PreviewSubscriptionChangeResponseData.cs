using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record PreviewSubscriptionChangeResponseData
{
    [JsonPropertyName("amount_off")]
    public required int AmountOff { get; init; }

    [JsonPropertyName("due_now")]
    public required int DueNow { get; init; }

    [JsonPropertyName("new_charges")]
    public required int NewCharges { get; init; }

    [JsonPropertyName("percent_off")]
    public required double PercentOff { get; init; }

    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; init; }

    [JsonPropertyName("promo_code_applied")]
    public required bool PromoCodeApplied { get; init; }

    [JsonPropertyName("proration")]
    public required int Proration { get; init; }

    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; init; }
}
