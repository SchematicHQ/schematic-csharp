using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ApiKeyRequestResponseData
{
    [JsonPropertyName("api_key_id")]
    public required string ApiKeyId { get; set; }

    [JsonPropertyName("ended_at")]
    public DateTime? EndedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("method")]
    public required string Method { get; set; }

    [JsonPropertyName("req_body")]
    public string? ReqBody { get; set; }

    [JsonPropertyName("request_type")]
    public string? RequestType { get; set; }

    [JsonPropertyName("resource_id")]
    public int? ResourceId { get; set; }

    [JsonPropertyName("resource_id_string")]
    public string? ResourceIdString { get; set; }

    [JsonPropertyName("resource_name")]
    public string? ResourceName { get; set; }

    [JsonPropertyName("resource_type")]
    public string? ResourceType { get; set; }

    [JsonPropertyName("resp_body")]
    public string? RespBody { get; set; }

    [JsonPropertyName("resp_code")]
    public int? RespCode { get; set; }

    [JsonPropertyName("secondary_resource")]
    public string? SecondaryResource { get; set; }

    [JsonPropertyName("started_at")]
    public required DateTime StartedAt { get; set; }

    [JsonPropertyName("url")]
    public required string Url { get; set; }

    [JsonPropertyName("user_agent")]
    public string? UserAgent { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }
}
