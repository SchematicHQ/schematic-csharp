using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class LookupCompanyResponse
{
    [JsonPropertyName("data")]
    public CompanyDetailResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public LookupCompanyParams Params { get; init; }
}
