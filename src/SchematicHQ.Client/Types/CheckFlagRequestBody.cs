using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CheckFlagRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company")]
    public Dictionary<string, string>? Company { get; set; }

    /// <summary>
    /// Hypothetical usage to evaluate the flag against, for answering "would this action be allowed?" before performing it. Only supported when checking a single flag. Values are caller-asserted and can widen a verdict as well as narrow it, so do not forward untrusted input here when the result gates access. Only the flag value reflects the preflight; the entitlement and usage figures in the response are the company's current, unsimulated ones
    /// </summary>
    [JsonPropertyName("preflight")]
    public PreflightRequestBody? Preflight { get; set; }

    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; set; }

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
