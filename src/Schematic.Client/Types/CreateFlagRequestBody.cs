using System.Text.Json.Serialization;

namespace Schematic.Client;

public class CreateFlagRequestBody
{
    [JsonPropertyName("default_value")]
    public bool DefaultValue { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; init; }

    [JsonPropertyName("flag_type")]
    public string FlagType { get; init; }

    [JsonPropertyName("key")]
    public string Key { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}
