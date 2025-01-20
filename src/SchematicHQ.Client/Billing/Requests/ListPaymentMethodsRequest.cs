namespace SchematicHQ.Client;

public record ListPaymentMethodsRequest
{
    public string? CompanyId { get; init; }

    public required string CustomerExternalId { get; init; }

    public string? SubscriptionExternalId { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
