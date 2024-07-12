using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record UpdatePlanRequestBody
{
    [JsonPropertyName("audience_type")]
    public required string AudienceType { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("plan_type")]
    public UpdatePlanRequestBodyPlanType? PlanType { get; init; }
}
