using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CountCompaniesResponse
{
    [JsonPropertyName("data")]
    public required CountResponse Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required CountCompaniesParams Params { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
