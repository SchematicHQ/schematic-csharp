using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CreatePlanRequestBody
{
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("plan_type")]
    public required CreatePlanRequestBodyPlanType PlanType { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
