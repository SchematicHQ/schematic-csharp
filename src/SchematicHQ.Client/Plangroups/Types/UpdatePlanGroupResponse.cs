using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record UpdatePlanGroupResponse
{
    [JsonPropertyName("data")]
    public required PlanGroupResponseData Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object?> Params { get; set; } = new Dictionary<string, object?>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}