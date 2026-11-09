using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PreviewGroupRolloutRequest
{
    [JsonIgnore]
    public string? FromPlanVersionId { get; set; }

    [JsonIgnore]
    public string? ToPlanVersionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
