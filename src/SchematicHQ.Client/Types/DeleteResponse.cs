using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class DeleteResponse
{
    /// <summary>
    /// Whether the delete was successful
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; init; }
}
