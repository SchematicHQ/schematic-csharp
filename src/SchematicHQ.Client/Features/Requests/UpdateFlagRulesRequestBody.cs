using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateFlagRulesRequestBody
{
    /// <summary>
    /// flag_id
    /// </summary>
    [JsonIgnore]
    public required string FlagId { get; set; }

    [JsonPropertyName("rules")]
    public IEnumerable<CreateOrUpdateRuleRequestBody> Rules { get; set; } =
        new List<CreateOrUpdateRuleRequestBody>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
