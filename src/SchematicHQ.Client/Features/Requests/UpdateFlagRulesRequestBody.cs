using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class UpdateFlagRulesRequestBody
{
    [JsonPropertyName("rules")]
    public List<CreateOrUpdateRuleRequestBody> Rules { get; init; }
}
