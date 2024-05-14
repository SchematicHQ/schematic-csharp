using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListPlanEntitlementsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<PlanEntitlementResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListPlanEntitlementsParams Params { get; init; }
}
