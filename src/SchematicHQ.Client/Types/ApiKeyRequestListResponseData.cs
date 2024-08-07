using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ApiKeyRequestListResponseData
{
    [JsonPropertyName("api_key_id")]
    public required string ApiKeyId { get; init; }

    [JsonPropertyName("ended_at")]
    public DateTime? EndedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("method")]
    public required string Method { get; init; }

    [JsonPropertyName("req_body")]
    public string? ReqBody { get; init; }

    [JsonPropertyName("request_type")]
    public string? RequestType { get; init; }

    [JsonPropertyName("resource_id")]
    public int? ResourceId { get; init; }

    [JsonPropertyName("resource_id_string")]
    public string? ResourceIdString { get; init; }

    [JsonPropertyName("resource_name")]
    public string? ResourceName { get; init; }

    [JsonPropertyName("resource_type")]
    public string? ResourceType { get; init; }

    [JsonPropertyName("resp_code")]
    public int? RespCode { get; init; }

    [JsonPropertyName("secondary_resource")]
    public string? SecondaryResource { get; init; }

    [JsonPropertyName("started_at")]
    public required DateTime StartedAt { get; init; }

    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("user_name")]
    public string? UserName { get; init; }
}
