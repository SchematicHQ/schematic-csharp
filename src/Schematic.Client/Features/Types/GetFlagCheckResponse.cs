using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class GetFlagCheckResponse
{
    [JsonPropertyName("data")]
    public FlagCheckLogDetailResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; }
}
