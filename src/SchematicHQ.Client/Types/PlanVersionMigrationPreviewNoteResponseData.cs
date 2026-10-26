using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanVersionMigrationPreviewNoteResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Whether this note predicts that the company's migration would fail.
    /// </summary>
    [JsonPropertyName("blocking")]
    public required bool Blocking { get; set; }

    /// <summary>
    /// The error code the company's migration would fail or be skipped with. Null for a note that is only informational.
    /// </summary>
    [JsonPropertyName("code")]
    public MigrationErrorCode? Code { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

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
