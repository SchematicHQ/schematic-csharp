using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

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
