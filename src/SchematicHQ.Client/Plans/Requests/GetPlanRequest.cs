using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetPlanRequest
{
    /// <summary>
    /// Fetch billing settings for a specific plan version
    /// </summary>
    [JsonIgnore]
    public string? PlanVersionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
