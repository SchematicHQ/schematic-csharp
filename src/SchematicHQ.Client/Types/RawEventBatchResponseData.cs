using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class RawEventBatchResponseData
{
    [JsonPropertyName("events")]
    public IEnumerable<RawEventResponseData> Events { get; init; }
}
