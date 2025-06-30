using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record LookupCompanyRequest
{
    /// <summary>
    /// Key/value pairs
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
