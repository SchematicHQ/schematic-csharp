namespace SchematicHQ.Client;

public record LookupUserRequest
{
    /// <summary>
    /// Key/value pairs
    /// </summary>
    public Dictionary<string, object?> Keys { get; set; } = new Dictionary<string, object?>();
}
