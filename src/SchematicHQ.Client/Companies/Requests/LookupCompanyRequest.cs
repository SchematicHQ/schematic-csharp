using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record LookupCompanyRequest
{
    /// <summary>
    /// Key/value pairs
    /// </summary>
    public object Keys { get; set; } = new Dictionary<string, object?>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
