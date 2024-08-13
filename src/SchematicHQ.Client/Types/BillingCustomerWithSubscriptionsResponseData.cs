using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record BillingCustomerWithSubscriptionsResponseData
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }

    [JsonPropertyName("failed_to_import")]
    public required bool FailedToImport { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("subscriptions")]
    public IEnumerable<BillingCustomerSubscription> Subscriptions { get; init; } =
        new List<BillingCustomerSubscription>();

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
