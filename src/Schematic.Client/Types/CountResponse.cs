using System.Text.Json.Serialization;

namespace Schematic.Client;

public class CountResponse
{
    [JsonPropertyName("count")]
    public int Count { get; init; }
}
