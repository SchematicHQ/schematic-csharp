using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class MetricCountsHourlyResponseData
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("event_subtype")]
    public string EventSubtype { get; init; }

    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }

    [JsonPropertyName("value")]
    public int Value { get; init; }
}
