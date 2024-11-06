using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListUsersResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<UserDetailResponseData> Data { get; set; } =
        new List<UserDetailResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListUsersParams Params { get; set; }
}
