using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateCompanyPlansRequestBody
{
    [JsonPropertyName("add_on_ids")]
    public IEnumerable<string> AddOnIds { get; set; } = new List<string>();

    [JsonPropertyName("base_plan_id")]
    public string? BasePlanId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
