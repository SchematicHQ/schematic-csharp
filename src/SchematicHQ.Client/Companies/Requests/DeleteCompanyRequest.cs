using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeleteCompanyRequest
{
    /// <summary>
    /// company_id
    /// </summary>
    [JsonIgnore]
    public required string CompanyId { get; set; }

    [JsonIgnore]
    public bool? CancelSubscription { get; set; }

    [JsonIgnore]
    public bool? Prorate { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
