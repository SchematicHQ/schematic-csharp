using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CreatePlanResponse
{
    [JsonPropertyName("data")]
    public PlanDetailResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; }
}
