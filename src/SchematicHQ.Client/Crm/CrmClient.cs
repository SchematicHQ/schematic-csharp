using System.Net.Http;
using System.Text.Json;
using System.Threading;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class CrmClient
{
    private RawClient _client;

    internal CrmClient(RawClient client)
    {
        _client = client;
    }

    /// <example>
    /// <code>
    /// await client.Crm.UpsertDealLineItemAssociationAsync(
    ///     new CreateCrmDealLineItemAssociationRequestBody
    ///     {
    ///         DealExternalId = "deal_external_id",
    ///         LineItemExternalId = "line_item_external_id",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<UpsertDealLineItemAssociationResponse> UpsertDealLineItemAssociationAsync(
        CreateCrmDealLineItemAssociationRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .MakeRequestAsync(
                new RawClient.JsonApiRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "crm/associations/deal-line-item",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpsertDealLineItemAssociationResponse>(responseBody)!;
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
    /// await client.Crm.UpsertLineItemAsync(
    ///     new CreateCrmLineItemRequestBody
    ///     {
    ///         Amount = "amount",
    ///         Interval = "interval",
    ///         LineItemExternalId = "line_item_external_id",
    ///         ProductExternalId = "product_external_id",
    ///         Quantity = 1,
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<UpsertLineItemResponse> UpsertLineItemAsync(
        CreateCrmLineItemRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .MakeRequestAsync(
                new RawClient.JsonApiRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "crm/deal-line-item/upsert",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpsertLineItemResponse>(responseBody)!;
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
    /// await client.Crm.UpsertCrmDealAsync(
    ///     new CreateCrmDealRequestBody
    ///     {
    ///         CrmCompanyKey = "crm_company_key",
    ///         CrmType = "crm_type",
    ///         DealExternalId = "deal_external_id",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<UpsertCrmDealResponse> UpsertCrmDealAsync(
        CreateCrmDealRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .MakeRequestAsync(
                new RawClient.JsonApiRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "crm/deals/upsert",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpsertCrmDealResponse>(responseBody)!;
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
    /// await client.Crm.ListCrmProductsAsync(new ListCrmProductsRequest());
    /// </code>
    /// </example>
    public async Task<ListCrmProductsResponse> ListCrmProductsAsync(
        ListCrmProductsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        if (request.Name != null)
        {
            _query["name"] = request.Name;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.Value.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.Value.ToString();
        }
        var response = await _client
            .MakeRequestAsync(
                new RawClient.JsonApiRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "crm/products",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListCrmProductsResponse>(responseBody)!;
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
    /// await client.Crm.UpsertCrmProductAsync(
    ///     new CreateCrmProductRequestBody
    ///     {
    ///         Currency = "currency",
    ///         Description = "description",
    ///         ExternalId = "external_id",
    ///         Interval = "interval",
    ///         Name = "name",
    ///         Price = "price",
    ///         Quantity = 1,
    ///         Sku = "sku",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<UpsertCrmProductResponse> UpsertCrmProductAsync(
        CreateCrmProductRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .MakeRequestAsync(
                new RawClient.JsonApiRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "crm/products/upsert",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpsertCrmProductResponse>(responseBody)!;
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
