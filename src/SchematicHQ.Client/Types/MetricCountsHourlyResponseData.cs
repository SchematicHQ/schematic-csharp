using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record MetricCountsHourlyResponseData
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("event_subtype")]
    public required string EventSubtype { get; set; }

    [JsonPropertyName("start_time")]
    public required DateTime StartTime { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("value")]
    public required int Value { get; set; }
}
