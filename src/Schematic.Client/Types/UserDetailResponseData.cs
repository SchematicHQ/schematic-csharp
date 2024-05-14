using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class UserDetailResponseData
{
    [JsonPropertyName("company_memberships")]
    public List<CompanyMembershipDetailResponseData> CompanyMemberships { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("entity_traits")]
    public List<EntityTraitDetailResponseData> EntityTraits { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("keys")]
    public List<EntityKeyDetailResponseData> Keys { get; init; }

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
