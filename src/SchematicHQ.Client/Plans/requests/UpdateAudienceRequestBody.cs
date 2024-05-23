using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdateAudienceRequestBody
{
    [JsonPropertyName("condition_groups")]
    public List<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; init; }

    [JsonPropertyName("conditions")]
    public List<CreateOrUpdateConditionRequestBody> Conditions { get; init; }
}
