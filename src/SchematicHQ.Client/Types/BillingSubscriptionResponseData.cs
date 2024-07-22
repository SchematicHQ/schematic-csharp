using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingSubscriptionResponseData
{
    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
