using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingCustomerWithSubscriptionsResponseData
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("failed_to_import")]
    public required bool FailedToImport { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("subscriptions")]
    public IEnumerable<BillingCustomerSubscription> Subscriptions { get; set; } =
        new List<BillingCustomerSubscription>();

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }
}
