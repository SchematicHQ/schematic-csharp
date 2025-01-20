using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateTrialEndRequestBody
{
    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; init; }
}
