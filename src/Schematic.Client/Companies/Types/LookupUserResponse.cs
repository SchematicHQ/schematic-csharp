using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class LookupUserResponse
{
    [JsonPropertyName("data")]
    public UserDetailResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public LookupUserParams Params { get; init; }
}
