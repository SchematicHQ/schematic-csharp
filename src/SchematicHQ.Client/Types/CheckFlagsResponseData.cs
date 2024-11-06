using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CheckFlagsResponseData
{
    [JsonPropertyName("flags")]
    public IEnumerable<CheckFlagOutputWithFlagKey> Flags { get; set; } =
        new List<CheckFlagOutputWithFlagKey>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
