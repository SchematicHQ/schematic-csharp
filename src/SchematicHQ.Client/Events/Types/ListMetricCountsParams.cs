using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record ListMetricCountsParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("company_ids")]
    public IEnumerable<string>? CompanyIds { get; set; }

    [JsonPropertyName("end_time")]
    public DateTime? EndTime { get; set; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    [JsonPropertyName("event_subtypes")]
    public IEnumerable<string>? EventSubtypes { get; set; }

    [JsonPropertyName("grouping")]
    public string? Grouping { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("start_time")]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
