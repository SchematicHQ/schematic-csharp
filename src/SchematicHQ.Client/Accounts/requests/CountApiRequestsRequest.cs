namespace SchematicHQ.Client;

public class CountApiRequestsRequest
{
    public string? Q { get; init; }

    public string? RequestType { get; init; }

    public string? EnvironmentId { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
