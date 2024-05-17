using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class RawEventBatchResponseData
{
    [JsonPropertyName("events")]
    public List<RawEventResponseData> Events { get; init; }
}
