using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ListEnvironmentsParams
{
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
