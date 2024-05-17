namespace SchematicHQ.Client;

public class CreateBillingProductRequestBody
{
    public string Currency { get; init; }

    public string ExternalId { get; init; }

    public string Name { get; init; }

    public double Price { get; init; }

    public int Quantity { get; init; }
}
