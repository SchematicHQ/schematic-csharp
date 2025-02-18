using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record UserDetailResponseData
{
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
    public object? Traits { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
