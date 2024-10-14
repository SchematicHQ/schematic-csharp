using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record GetSegmentIntegrationStatusResponse
{
    [JsonPropertyName("data")]
    public required SegmentStatusResp Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public object Params { get; set; } = new Dictionary<string, object?>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
