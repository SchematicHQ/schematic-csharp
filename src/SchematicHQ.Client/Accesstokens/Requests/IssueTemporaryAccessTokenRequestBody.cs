using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record IssueTemporaryAccessTokenRequestBody
{
    /// <summary>
    /// Key-value pairs that identify the company the token is for, such as {"id": "your-company-id"}.
    /// </summary>
    [JsonPropertyName("lookup")]
    public Dictionary<string, string> Lookup { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("resource_type")]
    public string ResourceType { get; set; } = "company";

    /// <summary>
    /// Optional key-value pairs that identify the user of your app the token is for, such as {"email": "user@example.com"}. The user must belong to the company named by lookup. Writes made with the token are attributed to this user in the audit log; the token's access is the same either way.
    /// </summary>
    [JsonPropertyName("user_lookup")]
    public Dictionary<string, string>? UserLookup { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
