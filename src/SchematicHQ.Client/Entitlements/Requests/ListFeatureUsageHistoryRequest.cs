using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListFeatureUsageHistoryRequest
{
    /// <summary>
    /// Restrict to these company IDs; omit for every company in the environment
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> CompanyIds { get; set; } = new List<string>();

    /// <summary>
    /// Exclusive end of the window; must fall on an hour boundary
    /// </summary>
    [JsonIgnore]
    public required DateTime EndTime { get; set; }

    /// <summary>
    /// Restrict to these event features; omit for every event feature in the environment. Where several features measure the same event, each is reported separately and a page may carry more rows than the requested limit
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    /// <summary>
    /// Bucket the window; omit for a single total per company and feature
    /// </summary>
    [JsonIgnore]
    public TimeSeriesGranularity? Granularity { get; set; }

    /// <summary>
    /// Inclusive start of the window; must fall on an hour boundary
    /// </summary>
    [JsonIgnore]
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public long? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public long? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
