namespace SchematicHQ.Client;

public record ListCustomersRequest
{
    public string? Name { get; init; }

    public bool? FailedToImport { get; init; }

    public string? Q { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
