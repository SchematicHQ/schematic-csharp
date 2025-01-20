using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CreateOrUpdateConditionGroupRequestBody
{
    [JsonPropertyName("conditions")]
    public IEnumerable<CreateOrUpdateConditionRequestBody> Conditions { get; set; } =
        new List<CreateOrUpdateConditionRequestBody>();

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
