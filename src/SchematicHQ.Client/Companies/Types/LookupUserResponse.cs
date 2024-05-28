using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class LookupUserResponse
{
    [JsonPropertyName("data")]
    public UserDetailResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public LookupUserParams Params { get; init; }
}
