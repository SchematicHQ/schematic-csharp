namespace SchematicHQ.Client;

public class GetEventSummariesRequest
{
    public string? Q { get; init; }

    public string? EventSubtypes { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
