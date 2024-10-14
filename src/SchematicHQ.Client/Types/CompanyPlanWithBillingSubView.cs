using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CompanyPlanWithBillingSubView
{
    [JsonPropertyName("billing_product_id")]
    public string? BillingProductId { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("price")]
    public int? Price { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
