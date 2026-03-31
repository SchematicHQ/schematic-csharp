using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListPlanIssuesRequest
{
    [JsonIgnore]
    public required string PlanId { get; set; }

    [JsonIgnore]
    public string? PlanVersionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
