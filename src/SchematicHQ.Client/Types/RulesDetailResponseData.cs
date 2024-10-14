using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record RulesDetailResponseData
{
    [JsonPropertyName("Flag")]
    public FlagResponseData? Flag { get; set; }

    [JsonPropertyName("rules")]
    public IEnumerable<RuleDetailResponseData> Rules { get; set; } =
        new List<RuleDetailResponseData>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
