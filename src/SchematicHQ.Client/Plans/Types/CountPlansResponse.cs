using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CountPlansResponse
{
    [JsonPropertyName("data")]
    public CountResponse Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public CountPlansParams Params { get; init; }
}
