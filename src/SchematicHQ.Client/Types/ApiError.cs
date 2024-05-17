using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class ApiError
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; init; }
}
