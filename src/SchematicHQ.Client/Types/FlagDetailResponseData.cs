using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record FlagDetailResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("default_value")]
    public required bool DefaultValue { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("feature")]
    public FeatureResponseData? Feature { get; init; }

    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; init; }

    [JsonPropertyName("flag_type")]
    public required string FlagType { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("key")]
    public required string Key { get; init; }

    [JsonPropertyName("last_checked_at")]
    public DateTime? LastCheckedAt { get; init; }

    [JsonPropertyName("maintainer_id")]
    public string? MaintainerId { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("rules")]
    public IEnumerable<RuleDetailResponseData> Rules { get; init; } =
        new List<RuleDetailResponseData>();

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
