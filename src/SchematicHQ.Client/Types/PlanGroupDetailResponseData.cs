using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record PlanGroupDetailResponseData
{
    [JsonPropertyName("default_plan")]
    public PlanGroupPlanDetailResponseData? DefaultPlan { get; set; }

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("plans")]
    public IEnumerable<PlanGroupPlanDetailResponseData> Plans { get; set; } =
        new List<PlanGroupPlanDetailResponseData>();
}
