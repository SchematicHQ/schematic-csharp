using System.Text.Json.Serialization;

namespace Schematic.Client;

public class CountApiKeysParams
{
    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("require_environment")]
    public bool? RequireEnvironment { get; init; }
}
