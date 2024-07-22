using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountApiKeysParams
{
    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("require_environment")]
    public bool? RequireEnvironment { get; init; }
}
