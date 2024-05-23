namespace SchematicHQ.Client;

public class CreateBillingSubscriptionsRequestBody
{
    public string CustomerExternalId { get; init; }

    public DateTime ExpiredAt { get; init; }

    public string? Interval { get; init; }

    public List<string> ProductExternalIds { get; init; }

    public string SubscriptionExternalId { get; init; }
}
