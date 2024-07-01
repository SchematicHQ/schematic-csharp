using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class UserDetailResponseData
{
    [JsonPropertyName("company_memberships")]
    public IEnumerable<CompanyMembershipDetailResponseData> CompanyMemberships { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("entity_traits")]
    public IEnumerable<EntityTraitDetailResponseData> EntityTraits { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("keys")]
    public IEnumerable<EntityKeyDetailResponseData> Keys { get; init; }

    [JsonPropertyName("last_seen_at")]
    public DateTime? LastSeenAt { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<string, object>? Traits { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}
