using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record EventBodyFlagCheck : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Schematic company ID (starting with 'comp_') of the company evaluated, if any
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Report an error that occurred during the flag check
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Schematic flag ID (starting with 'flag_') for the flag matching the key, if any
    /// </summary>
    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

    /// <summary>
    /// The key of the flag being checked
    /// </summary>
    [JsonPropertyName("flag_key")]
    public required string FlagKey { get; set; }

    /// <summary>
    /// The reason why the value was returned
    /// </summary>
    [JsonPropertyName("reason")]
    public required string Reason { get; set; }

    /// <summary>
    /// Key-value pairs used to to identify company for which the flag was checked
    /// </summary>
    [JsonPropertyName("req_company")]
    public Dictionary<string, string>? ReqCompany { get; set; }

    /// <summary>
    /// Key-value pairs used to to identify user for which the flag was checked
    /// </summary>
    [JsonPropertyName("req_user")]
    public Dictionary<string, string>? ReqUser { get; set; }

    /// <summary>
    /// Schematic rule ID (starting with 'rule_') of the rule that matched for the flag, if any
    /// </summary>
    [JsonPropertyName("rule_id")]
    public string? RuleId { get; set; }

    /// <summary>
    /// Schematic user ID (starting with 'user_') of the user evaluated, if any
    /// </summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    /// <summary>
    /// The value of the flag for the given company and/or user
    /// </summary>
    [JsonPropertyName("value")]
    public required bool Value { get; set; }

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
