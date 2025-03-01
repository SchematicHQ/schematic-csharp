using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ComponentCapabilities
{
    [JsonPropertyName("checkout")]
    public required bool Checkout { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
