using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class ListApiKeysParams
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
