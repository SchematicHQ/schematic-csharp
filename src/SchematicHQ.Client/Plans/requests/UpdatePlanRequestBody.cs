using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdatePlanRequestBody
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("plan_type")]
    public UpdatePlanRequestBodyPlanType? PlanType { get; init; }
}
