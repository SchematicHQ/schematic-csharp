namespace SchematicHQ.Client;

public record GetFeatureUsageByCompanyRequest
{
    /// <summary>
    /// Key/value pairs
    /// </summary>
    public Dictionary<string, object?> Keys { get; set; } = new Dictionary<string, object?>();
}
