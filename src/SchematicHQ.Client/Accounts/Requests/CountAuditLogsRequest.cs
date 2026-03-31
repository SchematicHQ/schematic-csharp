using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountAuditLogsRequest
{
    [JsonIgnore]
    public ActorType? ActorType { get; set; }

    [JsonIgnore]
    public DateTime? EndTime { get; set; }

    [JsonIgnore]
    public string? EnvironmentId { get; set; }

    [JsonIgnore]
    public string? Q { get; set; }

    [JsonIgnore]
    public DateTime? StartTime { get; set; }

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
