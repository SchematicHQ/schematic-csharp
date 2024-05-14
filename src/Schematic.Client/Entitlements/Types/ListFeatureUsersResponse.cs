using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListFeatureUsersResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<FeatureCompanyUserResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListFeatureUsersParams Params { get; init; }
}
