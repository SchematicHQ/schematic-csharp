using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CompanyEventPeriodMetricsResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("captured_at_max")]
    public required DateTime CapturedAtMax { get; set; }

    [JsonPropertyName("captured_at_min")]
    public required DateTime CapturedAtMin { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("event_subtype")]
    public required string EventSubtype { get; set; }

    [JsonPropertyName("month_reset")]
    public required string MonthReset { get; set; }

    [JsonPropertyName("period")]
    public required string Period { get; set; }

    [JsonPropertyName("valid_until")]
    public DateTime? ValidUntil { get; set; }

    [JsonPropertyName("value")]
    public required int Value { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
