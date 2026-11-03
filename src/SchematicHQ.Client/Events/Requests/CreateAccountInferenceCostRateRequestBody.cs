using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateAccountInferenceCostRateRequestBody
{
    /// <summary>
    /// Three-letter currency code the rates are in, matched against the event's currency. Defaults to usd.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Provider the event reports, such as anthropic or openai. Matched case-insensitively.
    /// </summary>
    [JsonPropertyName("provider")]
    public required string Provider { get; set; }

    /// <summary>
    /// Cost of a single token in the row's currency, keyed by input_tokens, cached_input_tokens, cache_creation_input_tokens and output_tokens. input_tokens prices only the input left after the cached and cache-creation subsets, and output_tokens includes reasoning.
    /// </summary>
    [JsonPropertyName("rates")]
    public Dictionary<string, double> Rates { get; set; } = new Dictionary<string, double>();

    /// <summary>
    /// Model the response reports. A dated snapshot such as claude-sonnet-4-5-20250929 falls back to the undated name when the sheet has no row for the dated one.
    /// </summary>
    [JsonPropertyName("response_model")]
    public required string ResponseModel { get; set; }

    /// <summary>
    /// When the rate takes effect. It applies to events captured from then until the next row for the same provider, model and currency.
    /// </summary>
    [JsonPropertyName("starting_at")]
    public required DateTime StartingAt { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
