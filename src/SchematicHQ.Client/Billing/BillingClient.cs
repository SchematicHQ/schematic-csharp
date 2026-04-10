using global::System.Text.Json;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class BillingClient : IBillingClient
{
    private readonly RawClient _client;

    internal BillingClient(RawClient client)
    {
        _client = client;
    }

    private async Task<WithRawResponse<ListCouponsResponse>> ListCouponsAsyncCore(
        ListCouponsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 4)
            .Add("is_active", request.IsActive)
            .Add("q", request.Q)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/coupons",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<ListCouponsResponse>(responseBody)!;
                return new WithRawResponse<ListCouponsResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<UpsertBillingCouponResponse>> UpsertBillingCouponAsyncCore(
        CreateCouponRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Post,
                    Path = "billing/coupons",
                    Body = request,
                    Headers = _headers,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<UpsertBillingCouponResponse>(
                    responseBody
                )!;
                return new WithRawResponse<UpsertBillingCouponResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<
        WithRawResponse<UpsertBillingCustomerResponse>
    > UpsertBillingCustomerAsyncCore(
        CreateBillingCustomerRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Post,
                    Path = "billing/customer/upsert",
                    Body = request,
                    Headers = _headers,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<UpsertBillingCustomerResponse>(
                    responseBody
                )!;
                return new WithRawResponse<UpsertBillingCustomerResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<
        WithRawResponse<ListCustomersWithSubscriptionsResponse>
    > ListCustomersWithSubscriptionsAsyncCore(
        ListCustomersWithSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 6)
            .Add("company_ids", request.CompanyIds)
            .Add("name", request.Name)
            .Add("provider_type", request.ProviderType)
            .Add("q", request.Q)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/customers",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<ListCustomersWithSubscriptionsResponse>(
                    responseBody
                )!;
                return new WithRawResponse<ListCustomersWithSubscriptionsResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<CountCustomersResponse>> CountCustomersAsyncCore(
        CountCustomersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 6)
            .Add("company_ids", request.CompanyIds)
            .Add("name", request.Name)
            .Add("provider_type", request.ProviderType)
            .Add("q", request.Q)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/customers/count",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<CountCustomersResponse>(responseBody)!;
                return new WithRawResponse<CountCustomersResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<ListInvoicesResponse>> ListInvoicesAsyncCore(
        ListInvoicesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 5)
            .Add("company_id", request.CompanyId)
            .Add("customer_external_id", request.CustomerExternalId)
            .Add("subscription_external_id", request.SubscriptionExternalId)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/invoices",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<ListInvoicesResponse>(responseBody)!;
                return new WithRawResponse<ListInvoicesResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<UpsertInvoiceResponse>> UpsertInvoiceAsyncCore(
        CreateInvoiceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Post,
                    Path = "billing/invoices",
                    Body = request,
                    Headers = _headers,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<UpsertInvoiceResponse>(responseBody)!;
                return new WithRawResponse<UpsertInvoiceResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<ListMetersResponse>> ListMetersAsyncCore(
        ListMetersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 3)
            .Add("display_name", request.DisplayName)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/meter",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<ListMetersResponse>(responseBody)!;
                return new WithRawResponse<ListMetersResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<UpsertBillingMeterResponse>> UpsertBillingMeterAsyncCore(
        CreateMeterRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Post,
                    Path = "billing/meter/upsert",
                    Body = request,
                    Headers = _headers,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<UpsertBillingMeterResponse>(responseBody)!;
                return new WithRawResponse<UpsertBillingMeterResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<ListPaymentMethodsResponse>> ListPaymentMethodsAsyncCore(
        ListPaymentMethodsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 4)
            .Add("company_id", request.CompanyId)
            .Add("customer_external_id", request.CustomerExternalId)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/payment-methods",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<ListPaymentMethodsResponse>(responseBody)!;
                return new WithRawResponse<ListPaymentMethodsResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<UpsertPaymentMethodResponse>> UpsertPaymentMethodAsyncCore(
        CreatePaymentMethodRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Post,
                    Path = "billing/payment-methods",
                    Body = request,
                    Headers = _headers,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<UpsertPaymentMethodResponse>(
                    responseBody
                )!;
                return new WithRawResponse<UpsertPaymentMethodResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<ListBillingPricesResponse>> ListBillingPricesAsyncCore(
        ListBillingPricesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 16)
            .Add("currency", request.Currency)
            .Add("for_initial_plan", request.ForInitialPlan)
            .Add("for_trial_expiry_plan", request.ForTrialExpiryPlan)
            .Add("ids", request.Ids)
            .Add("interval", request.Interval)
            .Add("is_active", request.IsActive)
            .Add("price", request.Price)
            .Add("product_id", request.ProductId)
            .Add("product_ids", request.ProductIds)
            .Add("provider_type", request.ProviderType)
            .Add("q", request.Q)
            .Add("tiers_mode", request.TiersMode)
            .Add("usage_type", request.UsageType)
            .Add("with_meter", request.WithMeter)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/price",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<ListBillingPricesResponse>(responseBody)!;
                return new WithRawResponse<ListBillingPricesResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<UpsertBillingPriceResponse>> UpsertBillingPriceAsyncCore(
        CreateBillingPriceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Post,
                    Path = "billing/price/upsert",
                    Body = request,
                    Headers = _headers,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<UpsertBillingPriceResponse>(responseBody)!;
                return new WithRawResponse<UpsertBillingPriceResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<DeleteBillingProductResponse>> DeleteBillingProductAsyncCore(
        string billingId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "billing/product/{0}",
                        ValueConvert.ToPathParameterString(billingId)
                    ),
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<DeleteBillingProductResponse>(
                    responseBody
                )!;
                return new WithRawResponse<DeleteBillingProductResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<
        WithRawResponse<ListBillingProductPricesResponse>
    > ListBillingProductPricesAsyncCore(
        ListBillingProductPricesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 16)
            .Add("currency", request.Currency)
            .Add("for_initial_plan", request.ForInitialPlan)
            .Add("for_trial_expiry_plan", request.ForTrialExpiryPlan)
            .Add("ids", request.Ids)
            .Add("interval", request.Interval)
            .Add("is_active", request.IsActive)
            .Add("price", request.Price)
            .Add("product_id", request.ProductId)
            .Add("product_ids", request.ProductIds)
            .Add("provider_type", request.ProviderType)
            .Add("q", request.Q)
            .Add("tiers_mode", request.TiersMode)
            .Add("usage_type", request.UsageType)
            .Add("with_meter", request.WithMeter)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/product/prices",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<ListBillingProductPricesResponse>(
                    responseBody
                )!;
                return new WithRawResponse<ListBillingProductPricesResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<DeleteProductPriceResponse>> DeleteProductPriceAsyncCore(
        string billingId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "billing/product/prices/{0}",
                        ValueConvert.ToPathParameterString(billingId)
                    ),
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<DeleteProductPriceResponse>(responseBody)!;
                return new WithRawResponse<DeleteProductPriceResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<UpsertBillingProductResponse>> UpsertBillingProductAsyncCore(
        CreateBillingProductRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Post,
                    Path = "billing/product/upsert",
                    Body = request,
                    Headers = _headers,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<UpsertBillingProductResponse>(
                    responseBody
                )!;
                return new WithRawResponse<UpsertBillingProductResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<ListBillingProductsResponse>> ListBillingProductsAsyncCore(
        ListBillingProductsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 12)
            .Add("ids", request.Ids)
            .Add("is_active", request.IsActive)
            .Add("name", request.Name)
            .Add("price_usage_type", request.PriceUsageType)
            .Add("provider_type", request.ProviderType)
            .Add("q", request.Q)
            .Add("with_one_time_charges", request.WithOneTimeCharges)
            .Add("with_prices_only", request.WithPricesOnly)
            .Add("with_zero_price", request.WithZeroPrice)
            .Add("without_linked_to_plan", request.WithoutLinkedToPlan)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/products",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<ListBillingProductsResponse>(
                    responseBody
                )!;
                return new WithRawResponse<ListBillingProductsResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<WithRawResponse<CountBillingProductsResponse>> CountBillingProductsAsyncCore(
        CountBillingProductsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 12)
            .Add("ids", request.Ids)
            .Add("is_active", request.IsActive)
            .Add("name", request.Name)
            .Add("price_usage_type", request.PriceUsageType)
            .Add("provider_type", request.ProviderType)
            .Add("q", request.Q)
            .Add("with_one_time_charges", request.WithOneTimeCharges)
            .Add("with_prices_only", request.WithPricesOnly)
            .Add("with_zero_price", request.WithZeroPrice)
            .Add("without_linked_to_plan", request.WithoutLinkedToPlan)
            .Add("limit", request.Limit)
            .Add("offset", request.Offset)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/products/count",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<CountBillingProductsResponse>(
                    responseBody
                )!;
                return new WithRawResponse<CountBillingProductsResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async Task<
        WithRawResponse<UpsertBillingSubscriptionResponse>
    > UpsertBillingSubscriptionAsyncCore(
        CreateBillingSubscriptionRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Post,
                    Path = "billing/subscription/upsert",
                    Body = request,
                    Headers = _headers,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var responseData = JsonUtils.Deserialize<UpsertBillingSubscriptionResponse>(
                    responseBody
                )!;
                return new WithRawResponse<UpsertBillingSubscriptionResponse>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new SchematicApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
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
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <example><code>
    /// await client.Billing.ListCouponsAsync(
    ///     new ListCouponsRequest
    ///     {
    ///         IsActive = true,
    ///         Q = "q",
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListCouponsResponse> ListCouponsAsync(
        ListCouponsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListCouponsResponse>(
            ListCouponsAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.UpsertBillingCouponAsync(
    ///     new CreateCouponRequestBody
    ///     {
    ///         AmountOff = 1000000,
    ///         Duration = "duration",
    ///         DurationInMonths = 1000000,
    ///         ExternalId = "external_id",
    ///         MaxRedemptions = 1000000,
    ///         Name = "name",
    ///         PercentOff = 1.1,
    ///         TimesRedeemed = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertBillingCouponResponse> UpsertBillingCouponAsync(
        CreateCouponRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertBillingCouponResponse>(
            UpsertBillingCouponAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.UpsertBillingCustomerAsync(
    ///     new CreateBillingCustomerRequestBody
    ///     {
    ///         Email = "email",
    ///         ExternalId = "external_id",
    ///         Meta = new Dictionary&lt;string, string&gt;() { { "key", "value" } },
    ///         Name = "name",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertBillingCustomerResponse> UpsertBillingCustomerAsync(
        CreateBillingCustomerRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertBillingCustomerResponse>(
            UpsertBillingCustomerAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.ListCustomersWithSubscriptionsAsync(
    ///     new ListCustomersWithSubscriptionsRequest
    ///     {
    ///         Name = "name",
    ///         ProviderType = BillingProviderType.Orb,
    ///         Q = "q",
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListCustomersWithSubscriptionsResponse> ListCustomersWithSubscriptionsAsync(
        ListCustomersWithSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListCustomersWithSubscriptionsResponse>(
            ListCustomersWithSubscriptionsAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.CountCustomersAsync(
    ///     new CountCustomersRequest
    ///     {
    ///         Name = "name",
    ///         ProviderType = BillingProviderType.Orb,
    ///         Q = "q",
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<CountCustomersResponse> CountCustomersAsync(
        CountCustomersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CountCustomersResponse>(
            CountCustomersAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.ListInvoicesAsync(
    ///     new ListInvoicesRequest
    ///     {
    ///         CompanyId = "company_id",
    ///         CustomerExternalId = "customer_external_id",
    ///         SubscriptionExternalId = "subscription_external_id",
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListInvoicesResponse> ListInvoicesAsync(
        ListInvoicesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListInvoicesResponse>(
            ListInvoicesAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.UpsertInvoiceAsync(
    ///     new CreateInvoiceRequestBody
    ///     {
    ///         AmountDue = 1000000,
    ///         AmountPaid = 1000000,
    ///         AmountRemaining = 1000000,
    ///         CollectionMethod = "collection_method",
    ///         Currency = "currency",
    ///         CustomerExternalId = "customer_external_id",
    ///         Subtotal = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertInvoiceResponse> UpsertInvoiceAsync(
        CreateInvoiceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertInvoiceResponse>(
            UpsertInvoiceAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.ListMetersAsync(
    ///     new ListMetersRequest
    ///     {
    ///         DisplayName = "display_name",
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListMetersResponse> ListMetersAsync(
        ListMetersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListMetersResponse>(
            ListMetersAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.UpsertBillingMeterAsync(
    ///     new CreateMeterRequestBody
    ///     {
    ///         DisplayName = "display_name",
    ///         EventName = "event_name",
    ///         EventPayloadKey = "event_payload_key",
    ///         ExternalId = "external_id",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertBillingMeterResponse> UpsertBillingMeterAsync(
        CreateMeterRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertBillingMeterResponse>(
            UpsertBillingMeterAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.ListPaymentMethodsAsync(
    ///     new ListPaymentMethodsRequest
    ///     {
    ///         CompanyId = "company_id",
    ///         CustomerExternalId = "customer_external_id",
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListPaymentMethodsResponse> ListPaymentMethodsAsync(
        ListPaymentMethodsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListPaymentMethodsResponse>(
            ListPaymentMethodsAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.UpsertPaymentMethodAsync(
    ///     new CreatePaymentMethodRequestBody
    ///     {
    ///         CustomerExternalId = "customer_external_id",
    ///         ExternalId = "external_id",
    ///         PaymentMethodType = "payment_method_type",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertPaymentMethodResponse> UpsertPaymentMethodAsync(
        CreatePaymentMethodRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertPaymentMethodResponse>(
            UpsertPaymentMethodAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.ListBillingPricesAsync(
    ///     new ListBillingPricesRequest
    ///     {
    ///         Currency = "currency",
    ///         ForInitialPlan = true,
    ///         ForTrialExpiryPlan = true,
    ///         Interval = "interval",
    ///         IsActive = true,
    ///         Price = 1000000,
    ///         ProductId = "product_id",
    ///         ProviderType = BillingProviderType.Orb,
    ///         Q = "q",
    ///         TiersMode = BillingTiersMode.Graduated,
    ///         UsageType = BillingPriceUsageType.Licensed,
    ///         WithMeter = true,
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListBillingPricesResponse> ListBillingPricesAsync(
        ListBillingPricesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListBillingPricesResponse>(
            ListBillingPricesAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.UpsertBillingPriceAsync(
    ///     new CreateBillingPriceRequestBody
    ///     {
    ///         BillingScheme = BillingPriceScheme.PerUnit,
    ///         Currency = "currency",
    ///         ExternalAccountId = "external_account_id",
    ///         Interval = "interval",
    ///         IsActive = true,
    ///         Price = 1000000,
    ///         PriceExternalId = "price_external_id",
    ///         PriceTiers = new List&lt;CreateBillingPriceTierRequestBody&gt;()
    ///         {
    ///             new CreateBillingPriceTierRequestBody { PriceExternalId = "price_external_id" },
    ///         },
    ///         ProductExternalId = "product_external_id",
    ///         UsageType = BillingPriceUsageType.Licensed,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertBillingPriceResponse> UpsertBillingPriceAsync(
        CreateBillingPriceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertBillingPriceResponse>(
            UpsertBillingPriceAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.DeleteBillingProductAsync("billing_id");
    /// </code></example>
    public WithRawResponseTask<DeleteBillingProductResponse> DeleteBillingProductAsync(
        string billingId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<DeleteBillingProductResponse>(
            DeleteBillingProductAsyncCore(billingId, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.ListBillingProductPricesAsync(
    ///     new ListBillingProductPricesRequest
    ///     {
    ///         Currency = "currency",
    ///         ForInitialPlan = true,
    ///         ForTrialExpiryPlan = true,
    ///         Interval = "interval",
    ///         IsActive = true,
    ///         Price = 1000000,
    ///         ProductId = "product_id",
    ///         ProviderType = BillingProviderType.Orb,
    ///         Q = "q",
    ///         TiersMode = BillingTiersMode.Graduated,
    ///         UsageType = BillingPriceUsageType.Licensed,
    ///         WithMeter = true,
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListBillingProductPricesResponse> ListBillingProductPricesAsync(
        ListBillingProductPricesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListBillingProductPricesResponse>(
            ListBillingProductPricesAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.DeleteProductPriceAsync("billing_id");
    /// </code></example>
    public WithRawResponseTask<DeleteProductPriceResponse> DeleteProductPriceAsync(
        string billingId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<DeleteProductPriceResponse>(
            DeleteProductPriceAsyncCore(billingId, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.UpsertBillingProductAsync(
    ///     new CreateBillingProductRequestBody { ExternalId = "external_id", Price = 1.1 }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertBillingProductResponse> UpsertBillingProductAsync(
        CreateBillingProductRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertBillingProductResponse>(
            UpsertBillingProductAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.ListBillingProductsAsync(
    ///     new ListBillingProductsRequest
    ///     {
    ///         IsActive = true,
    ///         Name = "name",
    ///         PriceUsageType = BillingPriceUsageType.Licensed,
    ///         ProviderType = BillingProviderType.Orb,
    ///         Q = "q",
    ///         WithOneTimeCharges = true,
    ///         WithPricesOnly = true,
    ///         WithZeroPrice = true,
    ///         WithoutLinkedToPlan = true,
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListBillingProductsResponse> ListBillingProductsAsync(
        ListBillingProductsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListBillingProductsResponse>(
            ListBillingProductsAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.CountBillingProductsAsync(
    ///     new CountBillingProductsRequest
    ///     {
    ///         IsActive = true,
    ///         Name = "name",
    ///         PriceUsageType = BillingPriceUsageType.Licensed,
    ///         ProviderType = BillingProviderType.Orb,
    ///         Q = "q",
    ///         WithOneTimeCharges = true,
    ///         WithPricesOnly = true,
    ///         WithZeroPrice = true,
    ///         WithoutLinkedToPlan = true,
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<CountBillingProductsResponse> CountBillingProductsAsync(
        CountBillingProductsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CountBillingProductsResponse>(
            CountBillingProductsAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Billing.UpsertBillingSubscriptionAsync(
    ///     new CreateBillingSubscriptionRequestBody
    ///     {
    ///         CancelAtPeriodEnd = true,
    ///         Currency = "currency",
    ///         CustomerExternalId = "customer_external_id",
    ///         Discounts = new List&lt;BillingSubscriptionDiscount&gt;()
    ///         {
    ///             new BillingSubscriptionDiscount
    ///             {
    ///                 CouponExternalId = "coupon_external_id",
    ///                 ExternalId = "external_id",
    ///                 IsActive = true,
    ///                 StartedAt = new DateTime(2024, 01, 15, 09, 30, 00, 000),
    ///             },
    ///         },
    ///         ExpiredAt = new DateTime(2024, 01, 15, 09, 30, 00, 000),
    ///         ProductExternalIds = new List&lt;BillingProductPricing&gt;()
    ///         {
    ///             new BillingProductPricing
    ///             {
    ///                 Currency = "currency",
    ///                 Interval = "interval",
    ///                 Price = 1000000,
    ///                 PriceExternalId = "price_external_id",
    ///                 ProductExternalId = "product_external_id",
    ///                 Quantity = 1000000,
    ///                 UsageType = BillingPriceUsageType.Licensed,
    ///             },
    ///         },
    ///         SubscriptionExternalId = "subscription_external_id",
    ///         TotalPrice = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertBillingSubscriptionResponse> UpsertBillingSubscriptionAsync(
        CreateBillingSubscriptionRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertBillingSubscriptionResponse>(
            UpsertBillingSubscriptionAsyncCore(request, options, cancellationToken)
        );
    }
}
