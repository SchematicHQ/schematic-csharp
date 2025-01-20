using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CompanyEventPeriodMetricsResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; init; }

    [JsonPropertyName("captured_at_max")]
    public required DateTime CapturedAtMax { get; init; }

    [JsonPropertyName("captured_at_min")]
    public required DateTime CapturedAtMin { get; init; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("event_subtype")]
    public required string EventSubtype { get; init; }

    [JsonPropertyName("month_reset")]
    public required string MonthReset { get; init; }

    [JsonPropertyName("period")]
    public required string Period { get; init; }

    [JsonPropertyName("valid_until")]
    public DateTime? ValidUntil { get; init; }

    [JsonPropertyName("value")]
    public required int Value { get; init; }
}
