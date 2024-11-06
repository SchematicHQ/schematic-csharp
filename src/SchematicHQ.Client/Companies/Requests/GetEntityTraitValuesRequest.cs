namespace SchematicHQ.Client;

public record GetEntityTraitValuesRequest
{
    public required string DefinitionId { get; set; }

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
