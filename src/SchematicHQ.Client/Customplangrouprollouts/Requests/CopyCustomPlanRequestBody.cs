using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CopyCustomPlanRequestBody
{
    /// <summary>
    /// The company the copy is for, such as a location the plan's company pays for.
    /// </summary>
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    /// <summary>
    /// The copy's name. Defaults to the source plan's.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
