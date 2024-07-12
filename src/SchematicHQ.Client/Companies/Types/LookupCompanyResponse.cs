using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record LookupCompanyResponse
{
    [JsonPropertyName("data")]
    public required CompanyDetailResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required LookupCompanyParams Params { get; init; }
}
