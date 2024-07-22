namespace SchematicHQ.Client;

public record GetFeatureUsageByCompanyRequest
{
    /// <summary>
    /// Key/value pairs
    /// </summary>
    public Dictionary<string, object> Keys { get; init; } = new Dictionary<string, object>();
}
