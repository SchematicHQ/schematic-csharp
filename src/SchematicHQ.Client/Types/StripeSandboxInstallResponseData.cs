using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record StripeSandboxInstallResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("api_keys")]
    public StripeSandboxKeysResponseData? ApiKeys { get; set; }

    [JsonPropertyName("claim_url")]
    public required string ClaimUrl { get; set; }

    [JsonPropertyName("claim_url_expires_at")]
    public DateTime? ClaimUrlExpiresAt { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    [JsonPropertyName("install")]
    public required IntegrationInstallResponseData Install { get; set; }

    [JsonPropertyName("sandbox_id")]
    public required string SandboxId { get; set; }

    [JsonPropertyName("stripe_account_id")]
    public required string StripeAccountId { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
