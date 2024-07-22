using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record RawEventBatchResponseData
{
    [JsonPropertyName("events")]
    public IEnumerable<RawEventResponseData> Events { get; init; } =
        new List<RawEventResponseData>();
}
