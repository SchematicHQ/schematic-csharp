using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditsAutoTopupRetryFailure : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company")]
    public CreditsAutoTopupCompanySummary? Company { get; set; }

    [JsonPropertyName("consecutive_failures")]
    public required int ConsecutiveFailures { get; set; }

    [JsonPropertyName("credit")]
    public CreditsAutoTopupCreditSummary? Credit { get; set; }

    [JsonPropertyName("last_error_message")]
    public string? LastErrorMessage { get; set; }

    [JsonPropertyName("stripe_error_code")]
    public string? StripeErrorCode { get; set; }

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
