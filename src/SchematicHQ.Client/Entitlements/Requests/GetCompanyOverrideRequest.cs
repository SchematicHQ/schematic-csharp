using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetCompanyOverrideRequest
{
    /// <summary>
    /// company_override_id
    /// </summary>
    [JsonIgnore]
    public required string CompanyOverrideId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
