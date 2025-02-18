using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record UpdateTrialEndRequestBody
{
    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
