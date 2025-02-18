using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CreateCouponRequestBody
{
    [JsonPropertyName("amount_off")]
    public required int AmountOff { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("duration")]
    public required string Duration { get; set; }

    [JsonPropertyName("duration_in_months")]
    public required int DurationInMonths { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("max_redemptions")]
    public required int MaxRedemptions { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("percent_off")]
    public required double PercentOff { get; set; }

    [JsonPropertyName("times_redeemed")]
    public required int TimesRedeemed { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
