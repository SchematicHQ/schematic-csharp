using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class UpdateAudienceRequestBody
{
    [JsonPropertyName("condition_groups")]
    public IEnumerable<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; init; }

    [JsonPropertyName("conditions")]
    public IEnumerable<CreateOrUpdateConditionRequestBody> Conditions { get; init; }
}
