using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CustomPlanViewConfigResponseData
{
    [JsonPropertyName("cta_text")]
    public required string CtaText { get; set; }

    [JsonPropertyName("cta_web_site")]
    public required string CtaWebSite { get; set; }

    [JsonPropertyName("price_text")]
    public required string PriceText { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
