using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record EventBodyInference : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Number of input tokens served from cache
    /// </summary>
    [JsonPropertyName("cached_input_tokens")]
    public long? CachedInputTokens { get; set; }

    /// <summary>
    /// Key-value pairs to identify the company associated with the inference event
    /// </summary>
    [JsonPropertyName("company")]
    public Dictionary<string, string> Company { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// Provided cost of the inference request as a decimal string; derived from model pricing when omitted
    /// </summary>
    [JsonPropertyName("cost")]
    public string? Cost { get; set; }

    /// <summary>
    /// ISO 4217 currency code for the provided cost; defaults to 'usd'
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Optional track event name to fan out for usage-based billing
    /// </summary>
    [JsonPropertyName("event")]
    public string? Event { get; set; }

    /// <summary>
    /// Number of input tokens for the inference request
    /// </summary>
    [JsonPropertyName("input_tokens")]
    public required long InputTokens { get; set; }

    /// <summary>
    /// The inference operation; defaults to 'chat'
    /// </summary>
    [JsonPropertyName("operation")]
    public string? Operation { get; set; }

    /// <summary>
    /// Number of output tokens for the inference request
    /// </summary>
    [JsonPropertyName("output_tokens")]
    public required long OutputTokens { get; set; }

    /// <summary>
    /// The inference provider (e.g. 'anthropic', 'openai')
    /// </summary>
    [JsonPropertyName("provider")]
    public required string Provider { get; set; }

    /// <summary>
    /// Number of reasoning tokens for the inference request
    /// </summary>
    [JsonPropertyName("reasoning_tokens")]
    public long? ReasoningTokens { get; set; }

    /// <summary>
    /// The model requested for the inference request
    /// </summary>
    [JsonPropertyName("request_model")]
    public string? RequestModel { get; set; }

    /// <summary>
    /// Number of requests represented by this event; defaults to 1
    /// </summary>
    [JsonPropertyName("requests")]
    public long? Requests { get; set; }

    /// <summary>
    /// The model that served the inference response
    /// </summary>
    [JsonPropertyName("response_model")]
    public required string ResponseModel { get; set; }

    /// <summary>
    /// Key-value pairs to identify the user associated with the inference event
    /// </summary>
    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; set; }

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
