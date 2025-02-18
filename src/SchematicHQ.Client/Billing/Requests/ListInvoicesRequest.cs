using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ListInvoicesRequest
{
    public string? CompanyId { get; set; }

    public required string CustomerExternalId { get; set; }

    public string? SubscriptionExternalId { get; set; }

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
