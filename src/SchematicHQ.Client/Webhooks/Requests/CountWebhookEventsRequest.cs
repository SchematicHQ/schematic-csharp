namespace SchematicHQ.Client;

public record CountWebhookEventsRequest
{
    public string? Ids { get; init; }

    public string? Q { get; init; }

    public string? WebhookId { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
