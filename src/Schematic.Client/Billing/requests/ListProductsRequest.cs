namespace Schematic.Client;

public class ListProductsRequest
{
    public string? Ids { get; init; }

    public string? Name { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
