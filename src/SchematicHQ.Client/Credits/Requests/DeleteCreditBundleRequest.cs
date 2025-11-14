using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeleteCreditBundleRequest
{
    /// <summary>
    /// bundle_id
    /// </summary>
    [JsonIgnore]
    public required string BundleId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
