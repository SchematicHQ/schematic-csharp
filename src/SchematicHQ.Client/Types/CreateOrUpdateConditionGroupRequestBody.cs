using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CreateOrUpdateConditionGroupRequestBody
{
    [JsonPropertyName("conditions")]
    public List<CreateOrUpdateConditionRequestBody> Conditions { get; init; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }
}
