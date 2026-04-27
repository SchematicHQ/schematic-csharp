using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetEnvironmentTraitUsageTimeSeriesRequest
{
    [JsonIgnore]
    public required DateTime EndTime { get; set; }

    [JsonIgnore]
    public required string FeatureId { get; set; }

    [JsonIgnore]
    public TimeSeriesGranularity? Granularity { get; set; }

    [JsonIgnore]
    public required DateTime StartTime { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
