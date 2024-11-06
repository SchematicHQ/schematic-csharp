using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateOrUpdateFlagRequestBody
{
    [JsonPropertyName("default_value")]
    public required bool DefaultValue { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; set; }

    [JsonPropertyName("flag_type")]
    public required string FlagType { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("maintainer_id")]
    public string? MaintainerId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
