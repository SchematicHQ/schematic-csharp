namespace Schematic.Client;

public class CreateBillingSubscriptionsRequestBody
{
    public string CustomerExternalId { get; init; }

    public DateTime ExpiredAt { get; init; }

    public List<string> ProductExternalIds { get; init; }

    public string SubscriptionExternalId { get; init; }
}
