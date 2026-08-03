using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetUserUsageByCompanyRequest
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
    /// Restrict to a single event-based feature
    /// </summary>
    [JsonIgnore]
    public string? FeatureId { get; set; }

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
