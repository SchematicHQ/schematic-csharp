using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CountFeatureUsersResponse
{
    [JsonPropertyName("data")]
    public CountResponse Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public CountFeatureUsersParams Params { get; init; }
}
