using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

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
