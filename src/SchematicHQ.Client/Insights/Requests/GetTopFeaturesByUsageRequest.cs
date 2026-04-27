using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetTopFeaturesByUsageRequest
{
    [JsonIgnore]
    public required DateTime EndTime { get; set; }

    [JsonIgnore]
    public long? Limit { get; set; }

    [JsonIgnore]
    public required DateTime StartTime { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
