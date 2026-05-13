using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using OneOf;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

/// <summary>
/// Represents an event in the format expected by the capture service
/// </summary>
internal class CaptureEventPayload
{
    [JsonPropertyName("api_key")]
    public required string ApiKey { get; set; }

    [JsonPropertyName("body")]
    public OneOf<EventBodyTrack, EventBodyFlagCheck, EventBodyIdentify>? Body { get; set; }

    [JsonPropertyName("type")]
    public required EventType EventType { get; set; }

    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; set; }
}

/// <summary>
/// Represents the batch request body for the capture service
/// </summary>
internal class CaptureEventBatchPayload
{
    [JsonPropertyName("events")]
    public required List<CaptureEventPayload> Events { get; set; }
}

/// <summary>
/// HTTP client for sending events to the Schematic event capture service
/// </summary>
internal class EventCaptureClient
{
    public const string DefaultBaseUrl = "https://c.schematichq.com";

    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly ILogger _logger;

    public EventCaptureClient(HttpClient httpClient, string apiKey, ILogger logger, string? baseUrl = null)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _baseUrl = baseUrl ?? DefaultBaseUrl;
    }

    /// <summary>
    /// Sends a batch of events to the capture service
    /// </summary>
    public async Task SendBatchAsync(List<CreateEventRequestBody> events)
    {
        if (events == null || events.Count == 0)
            return;

        var endpoint = _baseUrl.TrimEnd('/') + "/batch";

        // Transform events to capture payload format
        var captureEvents = new List<CaptureEventPayload>(events.Count);
        foreach (var evt in events)
        {
            captureEvents.Add(new CaptureEventPayload
            {
                ApiKey = _apiKey,
                Body = evt.Body,
                EventType = evt.EventType,
                SentAt = evt.SentAt
            });
        }

        var batchPayload = new CaptureEventBatchPayload
        {
            Events = captureEvents
        };

        var json = JsonSerializer.Serialize(batchPayload, JsonOptions.JsonSerializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _logger.LogDebug("Sending {EventCount} events to capture service at {Endpoint}", events.Count, endpoint);

        using var response = await _httpClient.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Capture service returned HTTP {(int)response.StatusCode}: {responseBody}");
        }
    }
}
