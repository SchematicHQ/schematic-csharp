using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListAudienceUsersResponse
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
    public Dictionary<string, object?> Params { get; set; } = new Dictionary<string, object?>();
}
