using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CreateEventResponse
{
    [JsonPropertyName("data")]
    public RawEventResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; }
}
