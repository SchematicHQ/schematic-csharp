using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record LookupUserResponse
{
    [JsonPropertyName("data")]
    public required UserDetailResponseData Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required LookupUserParams Params { get; set; }
}
