using System.Text.Json.Serialization;

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
}
