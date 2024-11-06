using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ComponentCapabilities
{
    [JsonPropertyName("checkout")]
    public required bool Checkout { get; set; }
}
