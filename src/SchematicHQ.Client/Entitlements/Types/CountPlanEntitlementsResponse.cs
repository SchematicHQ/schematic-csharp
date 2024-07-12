using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CountPlanEntitlementsResponse
{
    [JsonPropertyName("data")]
    public required CountResponse Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required CountPlanEntitlementsParams Params { get; init; }
}
