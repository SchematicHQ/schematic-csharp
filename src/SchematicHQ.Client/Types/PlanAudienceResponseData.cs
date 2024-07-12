using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record PlanAudienceResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }

    [JsonPropertyName("priority")]
    public required int Priority { get; init; }

    [JsonPropertyName("rule_type")]
    public required string RuleType { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("value")]
    public required bool Value { get; init; }
}
