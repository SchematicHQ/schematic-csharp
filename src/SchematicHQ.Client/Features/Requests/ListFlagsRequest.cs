using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ListFlagsRequest
{
    [JsonIgnore]
    public string? FeatureId { get; set; }

    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public int? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
