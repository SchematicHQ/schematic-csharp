using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class UpdateApiKeyResponse
{
    [JsonPropertyName("data")]
    public ApiKeyResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; }
}
