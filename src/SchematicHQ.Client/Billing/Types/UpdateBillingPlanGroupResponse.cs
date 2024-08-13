using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateBillingPlanGroupResponse
{
    [JsonPropertyName("data")]
    public required BillingPlanGroupResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; } = new Dictionary<string, object>();
}
