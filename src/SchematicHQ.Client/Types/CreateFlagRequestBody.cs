using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateFlagRequestBody
{
    [JsonPropertyName("default_value")]
    public required bool DefaultValue { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; init; }

    [JsonPropertyName("flag_type")]
    public required string FlagType { get; init; }

    [JsonPropertyName("key")]
    public required string Key { get; init; }

    [JsonPropertyName("maintainer_id")]
    public string? MaintainerId { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
