using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountEntityTraitDefinitionsRequest
{
    [JsonIgnore]
    public EntityType? EntityType { get; set; }

    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    [JsonIgnore]
    public string? Q { get; set; }

    [JsonIgnore]
    public TraitType? TraitType { get; set; }

    [JsonIgnore]
    public IEnumerable<TraitType> TraitTypes { get; set; } = new List<TraitType>();

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
