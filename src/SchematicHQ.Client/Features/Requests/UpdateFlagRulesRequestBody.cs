using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateFlagRulesRequestBody
{
    [JsonPropertyName("rules")]
    public IEnumerable<CreateOrUpdateRuleRequestBody> Rules { get; set; } =
        new List<CreateOrUpdateRuleRequestBody>();
}
