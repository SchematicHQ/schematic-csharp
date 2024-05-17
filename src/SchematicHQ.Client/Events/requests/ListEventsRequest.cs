namespace SchematicHQ.Client;

public class ListEventsRequest
{
    public string? CompanyId { get; init; }

    public string? UserId { get; init; }

    public string? EventSubtype { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
