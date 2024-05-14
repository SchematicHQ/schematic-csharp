using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

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
