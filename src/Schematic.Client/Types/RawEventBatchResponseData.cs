using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class RawEventBatchResponseData
{
    [JsonPropertyName("events")]
    public List<RawEventResponseData> Events { get; init; }
}
