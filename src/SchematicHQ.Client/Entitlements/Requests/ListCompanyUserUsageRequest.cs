using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListCompanyUserUsageRequest
{
    /// <summary>
    /// Company to break usage down for
    /// </summary>
    [JsonIgnore]
    public required string CompanyId { get; set; }

    /// <summary>
    /// End of the usage window (exclusive); defaults to now
    /// </summary>
    [JsonIgnore]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// The event-based feature to break down; required when metric is feature
    /// </summary>
    [JsonIgnore]
    public string? FeatureId { get; set; }

    /// <summary>
    /// Which metric to break usage down by
    /// </summary>
    [JsonIgnore]
    public required UserUsageMetric Metric { get; set; }

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

    /// <summary>
    /// Start of the usage window; defaults to 30 days before the end
    /// </summary>
    [JsonIgnore]
    public DateTime? StartTime { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
