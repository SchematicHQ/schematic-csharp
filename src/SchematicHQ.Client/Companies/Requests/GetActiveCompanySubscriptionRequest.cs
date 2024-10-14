using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record GetActiveCompanySubscriptionRequest
{
    public required string CompanyId { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
