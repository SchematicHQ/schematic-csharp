using System.Text.Json;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class WebhooksClient
{
    private RawClient _client;

    public WebhooksClient(RawClient client)
    {
        _client = client;
    }

    public async Task<ListWebhookEventsResponse> ListWebhookEventsAsync(
        ListWebhookEventsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.WebhookId != null)
        {
            _query["webhook_id"] = request.WebhookId;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
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
                Path = "webhook-events",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListWebhookEventsResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<GetWebhookEventResponse> GetWebhookEventAsync(string webhookEventId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"webhook-events/{webhookEventId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetWebhookEventResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CountWebhookEventsResponse> CountWebhookEventsAsync(
        CountWebhookEventsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.WebhookId != null)
        {
            _query["webhook_id"] = request.WebhookId;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
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
                Path = "webhook-events/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountWebhookEventsResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<ListWebhooksResponse> ListWebhooksAsync(ListWebhooksRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Q != null)
        {
            _query["q"] = request.Q;
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
                Path = "webhooks",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListWebhooksResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CreateWebhookResponse> CreateWebhookAsync(CreateWebhookRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "webhooks",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CreateWebhookResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<GetWebhookResponse> GetWebhookAsync(string webhookId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = $"webhooks/{webhookId}" }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetWebhookResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdateWebhookResponse> UpdateWebhookAsync(
        string webhookId,
        UpdateWebhookRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"webhooks/{webhookId}",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpdateWebhookResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<DeleteWebhookResponse> DeleteWebhookAsync(string webhookId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"webhooks/{webhookId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<DeleteWebhookResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CountWebhooksResponse> CountWebhooksAsync(CountWebhooksRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Q != null)
        {
            _query["q"] = request.Q;
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
                Path = "webhooks/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountWebhooksResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }
}
