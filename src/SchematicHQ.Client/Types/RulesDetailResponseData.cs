using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record RulesDetailResponseData
{
    [JsonPropertyName("Flag")]
    public FlagResponseData? Flag { get; init; }

    [JsonPropertyName("rules")]
    public IEnumerable<RuleDetailResponseData> Rules { get; init; } =
        new List<RuleDetailResponseData>();
}
