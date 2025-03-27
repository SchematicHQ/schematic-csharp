using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ListEventsRequest
{
    [JsonIgnore]
    public string? CompanyId { get; set; }

    [JsonIgnore]
    public string? EventSubtype { get; set; }

    [JsonIgnore]
    public IEnumerable<string> EventTypes { get; set; } = new List<string>();

    [JsonIgnore]
    public string? FlagId { get; set; }

    [JsonIgnore]
    public string? UserId { get; set; }

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
