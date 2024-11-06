namespace SchematicHQ.Client;

public record ListCrmProductsRequest
{
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    public string? Name { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }
}
