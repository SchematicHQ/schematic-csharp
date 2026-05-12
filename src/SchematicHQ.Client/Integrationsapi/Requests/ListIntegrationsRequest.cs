using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListIntegrationsRequest
{
    [JsonIgnore]
    public bool? BillingOnly { get; set; }

    [JsonIgnore]
    public IEnumerable<string> ExcludeIds { get; set; } = new List<string>();

    [JsonIgnore]
    public string? Id { get; set; }

    [JsonIgnore]
    public IntegrationState? State { get; set; }

    [JsonIgnore]
    public IntegrationType? Type { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public long? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public long? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
