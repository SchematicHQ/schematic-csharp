using System.Net.Http;
using System.Text.Json;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public class EventsClient
{
    private RawClient _client;

    public EventsClient(RawClient client)
    {
        _client = client;
    }

    public async Task<CreateEventBatchResponse> CreateEventBatchAsync(
        CreateEventBatchRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "event-batch",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CreateEventBatchResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetEventSummariesResponse> GetEventSummariesAsync(
        GetEventSummariesRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.EventSubtypes != null)
        {
            _query["event_subtypes"] = request.EventSubtypes;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = "event-types",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetEventSummariesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListEventsResponse> ListEventsAsync(ListEventsRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.EventSubtype != null)
        {
            _query["event_subtype"] = request.EventSubtype;
        }
        if (request.EventTypes != null)
        {
            _query["event_types"] = request.EventTypes;
        }
        if (request.FlagId != null)
        {
            _query["flag_id"] = request.FlagId;
        }
        if (request.UserId != null)
        {
            _query["user_id"] = request.UserId;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = "events",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListEventsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CreateEventResponse> CreateEventAsync(CreateEventRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "events",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CreateEventResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetEventResponse> GetEventAsync(string eventId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = $"events/{eventId}" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetEventResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetSegmentIntegrationStatusResponse> GetSegmentIntegrationStatusAsync()
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = "segment-integration" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetSegmentIntegrationStatusResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
