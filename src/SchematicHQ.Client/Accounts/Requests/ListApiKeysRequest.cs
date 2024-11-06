namespace SchematicHQ.Client;

public record ListApiKeysRequest
{
    public string? EnvironmentId { get; set; }

    public required bool RequireEnvironment { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }
}
