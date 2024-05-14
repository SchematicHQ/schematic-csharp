namespace Schematic.Client;

public class GetLatestFlagChecksRequest
{
    public string? FlagId { get; init; }

    public string? FlagIds { get; init; }

    public string? Id { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
