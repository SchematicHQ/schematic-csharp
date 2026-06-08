using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record RetryMigrationRequestBody
{
    [JsonPropertyName("error_codes")]
    public IEnumerable<MigrationErrorCode> ErrorCodes { get; set; } =
        new List<MigrationErrorCode>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
