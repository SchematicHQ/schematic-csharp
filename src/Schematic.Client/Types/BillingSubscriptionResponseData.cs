using System.Text.Json.Serialization;

namespace Schematic.Client;

public class BillingSubscriptionResponseData
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; init; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; init; }

    [JsonPropertyName("external_id")]
    public string ExternalId { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}
