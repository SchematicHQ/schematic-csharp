using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record StripeEmbedInfo
{
    [JsonPropertyName("publishable_key")]
    public required string PublishableKey { get; set; }

    [JsonPropertyName("setup_intent_client_secret")]
    public string? SetupIntentClientSecret { get; set; }
}
