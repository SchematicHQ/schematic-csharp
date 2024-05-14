using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListUsersResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<UserDetailResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListUsersParams Params { get; init; }
}
