using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record RawEventBatchResponseData
{
    [JsonPropertyName("events")]
    public IEnumerable<RawEventResponseData> Events { get; set; } =
        new List<RawEventResponseData>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
