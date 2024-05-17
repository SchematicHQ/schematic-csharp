using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class RulesDetailResponseData
{
    [JsonPropertyName("Flag")]
    public FlagResponseData? Flag { get; init; }

    [JsonPropertyName("rules")]
    public List<RuleDetailResponseData> Rules { get; init; }
}
