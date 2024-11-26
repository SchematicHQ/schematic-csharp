using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record LookupCompanyRequest
{
    /// <summary>
    /// Key/value pairs
    /// </summary>
    public Dictionary<string, object?> Keys { get; set; } = new Dictionary<string, object?>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
