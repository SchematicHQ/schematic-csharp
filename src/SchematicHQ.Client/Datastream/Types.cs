using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using Schematic.RulesEngine.Models;

namespace SchematicHQ.Client.Datastream
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum MessageType
  {
    [JsonPropertyName("full")]
    Full,

    [JsonPropertyName("partial")]
    Partial,

    [JsonPropertyName("delete")]
    Delete,

    [JsonPropertyName("error")]
    Error
  }

  [JsonConverter(typeof(EntityTypeConverter))]
  public enum EntityType
  {
    Unknown,
    [JsonStringEnumMemberName("rulesengine.Company")]
    Company,
    [JsonStringEnumMemberName("rulesengine.Flags")]
    Flags,
    [JsonStringEnumMemberName("rulesengine.Flag")]
    Flag,
    [JsonStringEnumMemberName("rulesengine.User")]
    User
  }

  public enum Action
  {
    [JsonPropertyName("start")]
    Start,

    [JsonPropertyName("stop")]
    Stop
  }

  public class DataStreamRequest
  {
    [JsonPropertyName("action")]
    public Action? Action { get; set; }

    [JsonPropertyName("entity_type")]
    public EntityType EntityType { get; set; }

    [JsonPropertyName("keys")]
    public Dictionary<string, string>? Keys { get; set; }
  }

  public class DataStreamBaseRequest
  {
    [JsonPropertyName("data")]
    public required DataStreamRequest Data { get; set; }
  }

  public class DataStreamResponse
  {
    [JsonPropertyName("data")]
    public JsonElement? Data { get; set; }

    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }

    [JsonPropertyName("entity_type")]
    public EntityType EntityType { get; set; }

    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; set; }
  }

  public class DataStreamError
  {
    [JsonPropertyName("error")]
    public string? Error { get; set; }
    [JsonPropertyName("keys")]
    public Dictionary<string, string>? Keys { get; set; }
    [JsonPropertyName("entity_type")]
    public EntityType? EntityType { get; set; }
  }

}