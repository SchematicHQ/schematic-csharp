namespace SchematicHQ.Client;

public record ListMetricCountsRequest
{
    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? EventSubtype { get; set; }

    public IEnumerable<string> EventSubtypes { get; set; } = new List<string>();

    public string? CompanyId { get; set; }

    public IEnumerable<string> CompanyIds { get; set; } = new List<string>();

    public string? UserId { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }

    public string? Grouping { get; set; }
}
