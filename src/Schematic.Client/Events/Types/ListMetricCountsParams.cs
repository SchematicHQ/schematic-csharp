using System.Text.Json.Serialization;

namespace Schematic.Client;

public class ListMetricCountsParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("company_ids")]
    public List<string>? CompanyIds { get; init; }

    [JsonPropertyName("end_time")]
    public DateTime? EndTime { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("event_subtypes")]
    public List<string>? EventSubtypes { get; init; }

    [JsonPropertyName("grouping")]
    public string? Grouping { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("start_time")]
    public DateTime? StartTime { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }
}
