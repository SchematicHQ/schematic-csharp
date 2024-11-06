namespace SchematicHQ.Client;

public record CountCustomersRequest
{
    public string? Name { get; set; }

    public bool? FailedToImport { get; set; }

    public string? Q { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }
}
