using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class GetSegmentIntegrationStatusResponse
{
    [JsonPropertyName("data")]
    public SegmentStatusResp Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; }
}