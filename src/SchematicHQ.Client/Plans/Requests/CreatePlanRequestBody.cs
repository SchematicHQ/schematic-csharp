using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CreatePlanRequestBody
{
    [JsonPropertyName("audience_type")]
    public string? AudienceType { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("plan_type")]
    public required CreatePlanRequestBodyPlanType PlanType { get; init; }
}
