using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListPlansResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<PlanDetailResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListPlansParams Params { get; init; }
}
