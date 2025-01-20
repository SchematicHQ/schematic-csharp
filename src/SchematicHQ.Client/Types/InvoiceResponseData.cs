using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record InvoiceResponseData
{
    [JsonPropertyName("amount_due")]
    public required int AmountDue { get; init; }

    [JsonPropertyName("amount_paid")]
    public required int AmountPaid { get; init; }

    [JsonPropertyName("amount_remaining")]
    public required int AmountRemaining { get; init; }

    [JsonPropertyName("collection_method")]
    public required string CollectionMethod { get; init; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; init; }

    [JsonPropertyName("due_date")]
    public DateTime? DueDate { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("payment_method_external_id")]
    public string? PaymentMethodExternalId { get; init; }

    [JsonPropertyName("subscription_external_id")]
    public string? SubscriptionExternalId { get; init; }

    [JsonPropertyName("subtotal")]
    public required int Subtotal { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }
}
