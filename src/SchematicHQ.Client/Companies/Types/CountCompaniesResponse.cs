using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CountCompaniesResponse
{
    [JsonPropertyName("data")]
    public CountResponse Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public CountCompaniesParams Params { get; init; }
}
