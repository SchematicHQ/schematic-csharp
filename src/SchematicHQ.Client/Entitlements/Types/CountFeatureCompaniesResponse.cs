using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CountFeatureCompaniesResponse
{
    [JsonPropertyName("data")]
    public required CountResponse Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required CountFeatureCompaniesParams Params { get; init; }
}
