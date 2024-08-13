using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingPlanGroupResponseData
{
    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("plan_ids")]
    public IEnumerable<string> PlanIds { get; init; } = new List<string>();
}
