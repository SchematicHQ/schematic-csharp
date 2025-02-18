using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CheckFlagsResponseData
{
    [JsonPropertyName("flags")]
    public IEnumerable<CheckFlagResponseData> Flags { get; set; } =
        new List<CheckFlagResponseData>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
