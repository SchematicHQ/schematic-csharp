using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateFlagRulesRequestBody
{
    [JsonPropertyName("rules")]
    public IEnumerable<CreateOrUpdateRuleRequestBody> Rules { get; set; } =
        new List<CreateOrUpdateRuleRequestBody>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
