using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CustomPlanConfig
{
    [JsonPropertyName("cta_text")]
    public required string CtaText { get; set; }

    [JsonPropertyName("cta_web_site")]
    public required string CtaWebSite { get; set; }

    [JsonPropertyName("price_text")]
    public required string PriceText { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
