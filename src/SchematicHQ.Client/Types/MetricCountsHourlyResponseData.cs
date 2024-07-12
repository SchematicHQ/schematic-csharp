using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record MetricCountsHourlyResponseData
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("event_subtype")]
    public required string EventSubtype { get; init; }

    [JsonPropertyName("start_time")]
    public required DateTime StartTime { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }

    [JsonPropertyName("value")]
    public required int Value { get; init; }
}
