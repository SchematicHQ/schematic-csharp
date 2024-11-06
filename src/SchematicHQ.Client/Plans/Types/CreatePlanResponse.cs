using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreatePlanResponse
{
    [JsonPropertyName("data")]
    public required PlanDetailResponseData Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object?> Params { get; set; } = new Dictionary<string, object?>();
}
