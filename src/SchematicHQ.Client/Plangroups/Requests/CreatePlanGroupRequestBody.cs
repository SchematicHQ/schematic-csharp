using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreatePlanGroupRequestBody
{
    [JsonPropertyName("add_on_compatibilities")]
    public IEnumerable<CompatiblePlans>? AddOnCompatibilities { get; set; }

    /// <summary>
    /// Use OrderedAddOns instead
    /// </summary>
    [JsonPropertyName("add_on_ids")]
    public IEnumerable<string> AddOnIds { get; set; } = new List<string>();

    [JsonPropertyName("custom_plan_config")]
    public CustomPlanConfig? CustomPlanConfig { get; set; }

    [JsonPropertyName("custom_plan_id")]
    public string? CustomPlanId { get; set; }

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; set; }

    [JsonPropertyName("ordered_add_ons")]
    public IEnumerable<OrderedPlansInGroup> OrderedAddOns { get; set; } =
        new List<OrderedPlansInGroup>();

    [JsonPropertyName("ordered_bundle_list")]
    public IEnumerable<PlanGroupBundleOrder> OrderedBundleList { get; set; } =
        new List<PlanGroupBundleOrder>();

    [JsonPropertyName("ordered_plans")]
    public IEnumerable<OrderedPlansInGroup> OrderedPlans { get; set; } =
        new List<OrderedPlansInGroup>();

    [JsonPropertyName("show_period_toggle")]
    public required bool ShowPeriodToggle { get; set; }

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
