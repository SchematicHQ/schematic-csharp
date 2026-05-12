using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record StripeIntegrationConfig : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Connected Stripe account ID (acct_*)
    /// </summary>
    [JsonPropertyName("account_id")]
    public string? AccountId { get; set; }

    /// <summary>
    /// Display name of the connected Stripe account
    /// </summary>
    [JsonPropertyName("account_name")]
    public string? AccountName { get; set; }

    /// <summary>
    /// When importing Stripe customers, only update existing companies, do not create new companies
    /// </summary>
    [JsonPropertyName("company_update_only")]
    public bool? CompanyUpdateOnly { get; set; }

    /// <summary>
    /// Whether the integration is connected to a Stripe sandbox account
    /// </summary>
    [JsonPropertyName("is_sandbox")]
    public required bool IsSandbox { get; set; }

    /// <summary>
    /// Whether the integration is connected to a live Stripe account
    /// </summary>
    [JsonPropertyName("live_mode")]
    public required bool LiveMode { get; set; }

    /// <summary>
    /// Onboarding URL returned during the v2 (Connect) install flow before activation
    /// </summary>
    [JsonPropertyName("onboard_url")]
    public string? OnboardUrl { get; set; }

    /// <summary>
    /// Stripe integration config version (1 = legacy API key install, 2 = Connect/App install)
    /// </summary>
    [JsonPropertyName("version")]
    public required int Version { get; set; }

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
