using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListInvoicesParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("customer_external_id")]
    public string? CustomerExternalId { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("subscription_external_id")]
    public string? SubscriptionExternalId { get; init; }
}
