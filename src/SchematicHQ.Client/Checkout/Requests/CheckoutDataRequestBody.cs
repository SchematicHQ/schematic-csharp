using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CheckoutDataRequestBody
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("selected_plan_id")]
    public string? SelectedPlanId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
