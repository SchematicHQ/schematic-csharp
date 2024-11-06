using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ApiError
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("error")]
    public required string Error { get; set; }
}
