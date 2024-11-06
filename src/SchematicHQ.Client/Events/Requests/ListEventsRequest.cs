using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record ListEventsRequest
{
    public string? CompanyId { get; set; }

    public string? EventSubtype { get; set; }

    public IEnumerable<string> EventTypes { get; set; } = new List<string>();

    public string? FlagId { get; set; }

    public string? UserId { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
