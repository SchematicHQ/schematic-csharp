using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreatePlanGroupRequestBody
{
    [JsonPropertyName("add_on_ids")]
    public IEnumerable<string> AddOnIds { get; init; } = new List<string>();

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; init; }

    [JsonPropertyName("plan_ids")]
    public IEnumerable<string> PlanIds { get; init; } = new List<string>();

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; init; }

    [JsonPropertyName("trial_payment_method_required")]
    public bool? TrialPaymentMethodRequired { get; init; }
}
