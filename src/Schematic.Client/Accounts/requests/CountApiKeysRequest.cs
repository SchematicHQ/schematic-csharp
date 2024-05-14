namespace Schematic.Client;

public class CountApiKeysRequest
{
    public string? EnvironmentId { get; init; }

    public bool RequireEnvironment { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
