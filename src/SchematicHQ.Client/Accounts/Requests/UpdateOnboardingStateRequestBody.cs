using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateOnboardingStateRequestBody
{
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("dismissed")]
    public bool? Dismissed { get; set; }

    [JsonPropertyName("path")]
    public OnboardingPath? Path { get; set; }

    [JsonPropertyName("pricing_page_url")]
    public string? PricingPageUrl { get; set; }

    [JsonPropertyName("track")]
    public OnboardingTrack? Track { get; set; }

    [JsonPropertyName("website_url")]
    public string? WebsiteUrl { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
