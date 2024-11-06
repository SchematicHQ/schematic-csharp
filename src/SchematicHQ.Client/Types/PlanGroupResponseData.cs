using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record PlanGroupResponseData
{
    [JsonPropertyName("add_on_ids")]
    public IEnumerable<string> AddOnIds { get; set; } = new List<string>();

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("plan_ids")]
    public IEnumerable<string> PlanIds { get; set; } = new List<string>();
}
