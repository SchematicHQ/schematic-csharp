using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// The returned resource
/// </summary>
public record PlanGroupDetailResponseData
{
    [JsonPropertyName("add_ons")]
    public IEnumerable<PlanGroupPlanDetailResponseData> AddOns { get; set; } =
        new List<PlanGroupPlanDetailResponseData>();

    [JsonPropertyName("custom_plan_config")]
    public CustomPlanViewConfigResponseData? CustomPlanConfig { get; set; }

    [JsonPropertyName("custom_plan_id")]
    public string? CustomPlanId { get; set; }

    [JsonPropertyName("default_plan")]
    public PlanGroupPlanDetailResponseData? DefaultPlan { get; set; }

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("ordered_plan_list")]
    public IEnumerable<PlanGroupPlanEntitlementsOrder> OrderedPlanList { get; set; } =
        new List<PlanGroupPlanEntitlementsOrder>();

    [JsonPropertyName("plans")]
    public IEnumerable<PlanGroupPlanDetailResponseData> Plans { get; set; } =
        new List<PlanGroupPlanDetailResponseData>();

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; set; }

    [JsonPropertyName("trial_payment_method_required")]
    public bool? TrialPaymentMethodRequired { get; set; }

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
