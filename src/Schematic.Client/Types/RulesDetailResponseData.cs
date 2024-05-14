using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class RulesDetailResponseData
{
    [JsonPropertyName("Flag")]
    public FlagResponseData? Flag { get; init; }

    [JsonPropertyName("rules")]
    public List<RuleDetailResponseData> Rules { get; init; }
}
