using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateAccountInferenceCostRateRequestBody
{
    /// <summary>
    /// Cost of a single token in the row's currency, keyed by input_tokens, cached_input_tokens, cache_creation_input_tokens and output_tokens. input_tokens prices only the input left after the cached and cache-creation subsets, and output_tokens includes reasoning.
    /// </summary>
    [JsonPropertyName("rates")]
    public Dictionary<string, double> Rates { get; set; } = new Dictionary<string, double>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
