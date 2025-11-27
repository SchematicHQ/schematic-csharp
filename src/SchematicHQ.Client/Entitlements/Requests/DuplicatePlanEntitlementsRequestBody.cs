using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DuplicatePlanEntitlementsRequestBody
{
    [JsonPropertyName("source_plan_id")]
    public required string SourcePlanId { get; set; }

    [JsonPropertyName("target_plan_id")]
    public required string TargetPlanId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
