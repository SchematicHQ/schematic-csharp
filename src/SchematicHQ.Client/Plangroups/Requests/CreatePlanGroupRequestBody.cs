using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CreatePlanGroupRequestBody
{
    [JsonPropertyName("add_on_ids")]
    public IEnumerable<string> AddOnIds { get; set; } = new List<string>();

    [JsonPropertyName("custom_plan_config")]
    public CustomPlanConfig? CustomPlanConfig { get; set; }

    [JsonPropertyName("custom_plan_id")]
    public string? CustomPlanId { get; set; }

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; set; }

    [JsonPropertyName("ordered_plans")]
    public IEnumerable<OrderedPlansInGroup> OrderedPlans { get; set; } =
        new List<OrderedPlansInGroup>();

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; set; }

    [JsonPropertyName("trial_payment_method_required")]
    public bool? TrialPaymentMethodRequired { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
