using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListPlanEntitlementsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<PlanEntitlementResponseData> Data { get; set; } =
        new List<PlanEntitlementResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListPlanEntitlementsParams Params { get; set; }
}
