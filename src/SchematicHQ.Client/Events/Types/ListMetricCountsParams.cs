using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListMetricCountsParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("company_ids")]
    public IEnumerable<string>? CompanyIds { get; init; }

    [JsonPropertyName("end_time")]
    public DateTime? EndTime { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("event_subtypes")]
    public IEnumerable<string>? EventSubtypes { get; init; }

    [JsonPropertyName("grouping")]
    public string? Grouping { get; init; }

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

    [JsonPropertyName("start_time")]
    public DateTime? StartTime { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }
}
