using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CountApiRequestsResponse
{
    [JsonPropertyName("data")]
    public CountResponse Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public CountApiRequestsParams Params { get; init; }
}