using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateAudienceRequestBody
{
    [JsonPropertyName("condition_groups")]
    public IEnumerable<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; set; } =
        new List<CreateOrUpdateConditionGroupRequestBody>();

    [JsonPropertyName("conditions")]
    public IEnumerable<CreateOrUpdateConditionRequestBody> Conditions { get; set; } =
        new List<CreateOrUpdateConditionRequestBody>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
