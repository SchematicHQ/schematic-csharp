using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UserDetailResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_memberships")]
    public IEnumerable<CompanyMembershipDetailResponseData> CompanyMemberships { get; set; } =
        new List<CompanyMembershipDetailResponseData>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("entity_traits")]
    public IEnumerable<EntityTraitDetailResponseData> EntityTraits { get; set; } =
        new List<EntityTraitDetailResponseData>();

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("keys")]
    public IEnumerable<EntityKeyDetailResponseData> Keys { get; set; } =
        new List<EntityKeyDetailResponseData>();

    [JsonPropertyName("last_seen_at")]
    public DateTime? LastSeenAt { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<string, object?>? Traits { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
