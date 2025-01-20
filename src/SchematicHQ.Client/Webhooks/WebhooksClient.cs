using System.Net.Http;
using System.Text.Json;
using System.Threading;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public partial class WebhooksClient
{
    private RawClient _client;

    internal WebhooksClient(RawClient client)
    {
        _client = client;
    }

    /// <example>
    /// <code>
    /// await client.Webhooks.ListWebhookEventsAsync(new ListWebhookEventsRequest());
    /// </code>
    /// </example>
    public async Task<ListWebhookEventsResponse> ListWebhookEventsAsync(
        ListWebhookEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WebhookId != null)
        {
            _query["webhook_id"] = request.WebhookId;
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
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "webhook-events",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListWebhookEventsResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <example>
    /// <code>
    /// await client.Webhooks.GetWebhookEventAsync("webhook_event_id");
    /// </code>
    /// </example>
    public async Task<GetWebhookEventResponse> GetWebhookEventAsync(
        string webhookEventId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"webhook-events/{webhookEventId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<GetWebhookEventResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <example>
    /// <code>
    /// await client.Webhooks.CountWebhookEventsAsync(new CountWebhookEventsRequest());
    /// </code>
    /// </example>
    public async Task<CountWebhookEventsResponse> CountWebhookEventsAsync(
        CountWebhookEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WebhookId != null)
        {
            _query["webhook_id"] = request.WebhookId;
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
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "webhook-events/count",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountWebhookEventsResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <example>
    /// <code>
    /// await client.Webhooks.ListWebhooksAsync(new ListWebhooksRequest());
    /// </code>
    /// </example>
    public async Task<ListWebhooksResponse> ListWebhooksAsync(
        ListWebhooksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
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
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "webhooks",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListWebhooksResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <example>
    /// <code>
    /// await client.Webhooks.CreateWebhookAsync(
    ///     new CreateWebhookRequestBody
    ///     {
    ///         Name = "name",
    ///         RequestTypes = new List&lt;CreateWebhookRequestBodyRequestTypesItem&gt;()
    ///         {
    ///             CreateWebhookRequestBodyRequestTypesItem.CompanyUpdated,
    ///         },
    ///         Url = "url",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<CreateWebhookResponse> CreateWebhookAsync(
        CreateWebhookRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "webhooks",
                Body = request,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CreateWebhookResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <example>
    /// <code>
    /// await client.Webhooks.GetWebhookAsync("webhook_id");
    /// </code>
    /// </example>
    public async Task<GetWebhookResponse> GetWebhookAsync(
        string webhookId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"webhooks/{webhookId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<GetWebhookResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <example>
    /// <code>
    /// await client.Webhooks.UpdateWebhookAsync("webhook_id", new UpdateWebhookRequestBody());
    /// </code>
    /// </example>
    public async Task<UpdateWebhookResponse> UpdateWebhookAsync(
        string webhookId,
        UpdateWebhookRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Put,
                Path = $"webhooks/{webhookId}",
                Body = request,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpdateWebhookResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <example>
    /// <code>
    /// await client.Webhooks.DeleteWebhookAsync("webhook_id");
    /// </code>
    /// </example>
    public async Task<DeleteWebhookResponse> DeleteWebhookAsync(
        string webhookId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"webhooks/{webhookId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<DeleteWebhookResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <example>
    /// <code>
    /// await client.Webhooks.CountWebhooksAsync(new CountWebhooksRequest());
    /// </code>
    /// </example>
    public async Task<CountWebhooksResponse> CountWebhooksAsync(
        CountWebhooksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
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
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "webhooks/count",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountWebhooksResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }
}
