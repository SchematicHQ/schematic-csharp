namespace SchematicHQ.Client;

public record LookupCompanyRequest
{
    /// <summary>
    /// Key/value pairs
    /// </summary>
    public Dictionary<string, object> Keys { get; init; } = new Dictionary<string, object>();
}
