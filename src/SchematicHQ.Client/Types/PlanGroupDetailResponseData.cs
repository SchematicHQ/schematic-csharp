using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record PlanGroupDetailResponseData
{
    [JsonPropertyName("add_ons")]
    public IEnumerable<PlanGroupPlanDetailResponseData> AddOns { get; set; } =
        new List<PlanGroupPlanDetailResponseData>();

    [JsonPropertyName("default_plan")]
    public PlanGroupPlanDetailResponseData? DefaultPlan { get; set; }

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("plans")]
    public IEnumerable<PlanGroupPlanDetailResponseData> Plans { get; set; } =
        new List<PlanGroupPlanDetailResponseData>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}