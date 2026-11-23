using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetInferenceCostRequest
{
    /// <summary>
    /// End of the window (exclusive). Pass together with start_time; when both are omitted, the current calendar month in UTC is used
    /// </summary>
    [JsonIgnore]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Start of the window (inclusive). Pass together with end_time; when both are omitted, the current calendar month in UTC is used
    /// </summary>
    [JsonIgnore]
    public DateTime? StartTime { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
