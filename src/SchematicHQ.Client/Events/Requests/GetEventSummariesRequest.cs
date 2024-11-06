namespace SchematicHQ.Client;

public record GetEventSummariesRequest
{
    public string? Q { get; set; }

    public IEnumerable<string> EventSubtypes { get; set; } = new List<string>();

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }
}
