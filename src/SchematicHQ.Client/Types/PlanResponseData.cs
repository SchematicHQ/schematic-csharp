using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record PlanResponseData
{
    [JsonPropertyName("audience_type")]
    public required string AudienceType { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("plan_type")]
    public required string PlanType { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
