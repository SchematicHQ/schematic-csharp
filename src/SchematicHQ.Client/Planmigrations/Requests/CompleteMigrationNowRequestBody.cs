using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompleteMigrationNowRequestBody
{
    [JsonPropertyName("proration_behavior")]
    public MigrationProrationBehavior? ProrationBehavior { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
