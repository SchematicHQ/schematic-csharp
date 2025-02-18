using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record StripeEmbedInfo
{
    [JsonPropertyName("publishable_key")]
    public required string PublishableKey { get; set; }

    [JsonPropertyName("setup_intent_client_secret")]
    public string? SetupIntentClientSecret { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
