using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record PreviewComponentDataRequest
{
    public string? CompanyId { get; set; }

    public string? ComponentId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
