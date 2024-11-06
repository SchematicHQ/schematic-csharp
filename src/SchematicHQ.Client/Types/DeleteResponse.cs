using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record DeleteResponse
{
    /// <summary>
    /// Whether the delete was successful
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }
}
