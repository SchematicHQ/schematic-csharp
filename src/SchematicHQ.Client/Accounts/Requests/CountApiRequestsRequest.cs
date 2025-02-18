using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CountApiRequestsRequest
{
    public string? Q { get; set; }

    public string? RequestType { get; set; }

    public string? EnvironmentId { get; set; }

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
