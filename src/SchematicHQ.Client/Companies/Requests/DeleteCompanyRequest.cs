using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeleteCompanyRequest
{
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
