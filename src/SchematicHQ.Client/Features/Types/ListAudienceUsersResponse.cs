using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

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
    public object Params { get; set; } = new Dictionary<string, object?>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
