using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CountApiRequestsParams
{
    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    [JsonPropertyName("request_type")]
    public string? RequestType { get; init; }
}
