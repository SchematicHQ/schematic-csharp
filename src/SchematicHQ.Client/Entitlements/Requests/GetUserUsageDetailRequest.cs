using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetUserUsageDetailRequest
{
    /// <summary>
    /// Company the user belongs to
    /// </summary>
    [JsonIgnore]
    public required string CompanyId { get; set; }

    /// <summary>
    /// End of the usage window (exclusive); defaults to now
    /// </summary>
    [JsonIgnore]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Start of the usage window; defaults to 30 days before the end
    /// </summary>
    [JsonIgnore]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// User to break usage down for
    /// </summary>
    [JsonIgnore]
    public required string UserId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
