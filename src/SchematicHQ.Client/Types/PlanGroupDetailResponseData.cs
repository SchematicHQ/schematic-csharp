using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record PlanGroupDetailResponseData
{
    [JsonPropertyName("add_ons")]
    public IEnumerable<PlanGroupPlanDetailResponseData> AddOns { get; init; } =
        new List<PlanGroupPlanDetailResponseData>();

    [JsonPropertyName("default_plan")]
    public PlanGroupPlanDetailResponseData? DefaultPlan { get; init; }

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("plans")]
    public IEnumerable<PlanGroupPlanDetailResponseData> Plans { get; init; } =
        new List<PlanGroupPlanDetailResponseData>();

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; init; }

    [JsonPropertyName("trial_payment_method_required")]
    public bool? TrialPaymentMethodRequired { get; init; }
}
