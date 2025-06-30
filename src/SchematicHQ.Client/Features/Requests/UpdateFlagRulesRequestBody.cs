using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateFlagRulesRequestBody
{
    [JsonPropertyName("rules")]
    public IEnumerable<CreateOrUpdateRuleRequestBody> Rules { get; set; } =
        new List<CreateOrUpdateRuleRequestBody>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
