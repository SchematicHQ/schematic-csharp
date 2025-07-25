using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountCustomersRequest
{
    [JsonIgnore]
    public IEnumerable<string> CompanyIds { get; set; } = new List<string>();

    [JsonIgnore]
    public string? Name { get; set; }

    [JsonIgnore]
    public bool? FailedToImport { get; set; }

    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public int? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
