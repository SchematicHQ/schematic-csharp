using System.Net.Http;
using System.Text.Json;
using System.Threading;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class BillingClient
{
    private RawClient _client;

    internal BillingClient(RawClient client)
    {
        _client = client;
    }

    /// <example><code>
    /// await client.Billing.ListCouponsAsync(
    ///     new ListCouponsRequest
    ///     {
    ///         IsActive = true,
    ///         Q = "q",
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<ListCouponsResponse> ListCouponsAsync(
        ListCouponsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.IsActive != null)
        {
            _query["is_active"] = JsonUtils.Serialize(request.IsActive.Value);
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/coupons",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<ListCouponsResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.UpsertBillingCouponAsync(
    ///     new CreateCouponRequestBody
    ///     {
    ///         AmountOff = 1,
    ///         Duration = "duration",
    ///         DurationInMonths = 1,
    ///         ExternalId = "external_id",
    ///         MaxRedemptions = 1,
    ///         Name = "name",
    ///         PercentOff = 1.1,
    ///         TimesRedeemed = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<UpsertBillingCouponResponse> UpsertBillingCouponAsync(
        CreateCouponRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "billing/coupons",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<UpsertBillingCouponResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.UpsertBillingCustomerAsync(
    ///     new CreateBillingCustomerRequestBody
    ///     {
    ///         Email = "email",
    ///         ExternalId = "external_id",
    ///         FailedToImport = true,
    ///         Meta = new Dictionary&lt;string, string&gt;() { { "key", "value" } },
    ///         Name = "name",
    ///     }
    /// );
    /// </code></example>
    public async Task<UpsertBillingCustomerResponse> UpsertBillingCustomerAsync(
        CreateBillingCustomerRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "billing/customer/upsert",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<UpsertBillingCustomerResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.ListCustomersWithSubscriptionsAsync(
    ///     new ListCustomersWithSubscriptionsRequest
    ///     {
    ///         FailedToImport = true,
    ///         Name = "name",
    ///         ProviderType = BillingProviderType.Schematic,
    ///         Q = "q",
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<ListCustomersWithSubscriptionsResponse> ListCustomersWithSubscriptionsAsync(
        ListCustomersWithSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["company_ids"] = request.CompanyIds;
        if (request.FailedToImport != null)
        {
            _query["failed_to_import"] = JsonUtils.Serialize(request.FailedToImport.Value);
        }
        if (request.Name != null)
        {
            _query["name"] = request.Name;
        }
        if (request.ProviderType != null)
        {
            _query["provider_type"] = request.ProviderType.Value.Stringify();
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/customers",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<ListCustomersWithSubscriptionsResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.CountCustomersAsync(
    ///     new CountCustomersRequest
    ///     {
    ///         FailedToImport = true,
    ///         Name = "name",
    ///         ProviderType = BillingProviderType.Schematic,
    ///         Q = "q",
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<CountCustomersResponse> CountCustomersAsync(
        CountCustomersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["company_ids"] = request.CompanyIds;
        if (request.FailedToImport != null)
        {
            _query["failed_to_import"] = JsonUtils.Serialize(request.FailedToImport.Value);
        }
        if (request.Name != null)
        {
            _query["name"] = request.Name;
        }
        if (request.ProviderType != null)
        {
            _query["provider_type"] = request.ProviderType.Value.Stringify();
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/customers/count",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<CountCustomersResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.ListInvoicesAsync(
    ///     new ListInvoicesRequest
    ///     {
    ///         CompanyId = "company_id",
    ///         CustomerExternalId = "customer_external_id",
    ///         SubscriptionExternalId = "subscription_external_id",
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<ListInvoicesResponse> ListInvoicesAsync(
        ListInvoicesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["customer_external_id"] = request.CustomerExternalId;
        _query["subscription_external_id"] = request.SubscriptionExternalId;
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/invoices",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<ListInvoicesResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.UpsertInvoiceAsync(
    ///     new CreateInvoiceRequestBody
    ///     {
    ///         AmountDue = 1,
    ///         AmountPaid = 1,
    ///         AmountRemaining = 1,
    ///         CollectionMethod = "collection_method",
    ///         Currency = "currency",
    ///         CustomerExternalId = "customer_external_id",
    ///         Subtotal = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<UpsertInvoiceResponse> UpsertInvoiceAsync(
        CreateInvoiceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "billing/invoices",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<UpsertInvoiceResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.ListMetersAsync(
    ///     new ListMetersRequest
    ///     {
    ///         DisplayName = "display_name",
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<ListMetersResponse> ListMetersAsync(
        ListMetersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.DisplayName != null)
        {
            _query["display_name"] = request.DisplayName;
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/meter",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<ListMetersResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    public async Task<UpsertBillingMeterResponse> UpsertBillingMeterAsync(
        CreateMeterRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "billing/meter/upsert",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<UpsertBillingMeterResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.ListPaymentMethodsAsync(
    ///     new ListPaymentMethodsRequest
    ///     {
    ///         CompanyId = "company_id",
    ///         CustomerExternalId = "customer_external_id",
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<ListPaymentMethodsResponse> ListPaymentMethodsAsync(
        ListPaymentMethodsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["customer_external_id"] = request.CustomerExternalId;
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/payment-methods",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<ListPaymentMethodsResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.UpsertPaymentMethodAsync(
    ///     new CreatePaymentMethodRequestBody
    ///     {
    ///         CustomerExternalId = "customer_external_id",
    ///         ExternalId = "external_id",
    ///         PaymentMethodType = "payment_method_type",
    ///     }
    /// );
    /// </code></example>
    public async Task<UpsertPaymentMethodResponse> UpsertPaymentMethodAsync(
        CreatePaymentMethodRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "billing/payment-methods",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<UpsertPaymentMethodResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.ListBillingPricesAsync(
    ///     new ListBillingPricesRequest
    ///     {
    ///         ForInitialPlan = true,
    ///         ForTrialExpiryPlan = true,
    ///         Interval = "interval",
    ///         IsActive = true,
    ///         Price = 1,
    ///         ProductId = "product_id",
    ///         ProviderType = BillingProviderType.Schematic,
    ///         Q = "q",
    ///         TiersMode = BillingTiersMode.Graduated,
    ///         UsageType = BillingPriceUsageType.Licensed,
    ///         WithMeter = true,
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<ListBillingPricesResponse> ListBillingPricesAsync(
        ListBillingPricesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        _query["product_ids"] = request.ProductIds;
        if (request.ForInitialPlan != null)
        {
            _query["for_initial_plan"] = JsonUtils.Serialize(request.ForInitialPlan.Value);
        }
        if (request.ForTrialExpiryPlan != null)
        {
            _query["for_trial_expiry_plan"] = JsonUtils.Serialize(request.ForTrialExpiryPlan.Value);
        }
        if (request.Interval != null)
        {
            _query["interval"] = request.Interval;
        }
        if (request.IsActive != null)
        {
            _query["is_active"] = JsonUtils.Serialize(request.IsActive.Value);
        }
        if (request.Price != null)
        {
            _query["price"] = request.Price.Value.ToString();
        }
        if (request.ProductId != null)
        {
            _query["product_id"] = request.ProductId;
        }
        if (request.ProviderType != null)
        {
            _query["provider_type"] = request.ProviderType.Value.Stringify();
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.TiersMode != null)
        {
            _query["tiers_mode"] = request.TiersMode.Value.Stringify();
        }
        if (request.UsageType != null)
        {
            _query["usage_type"] = request.UsageType.Value.Stringify();
        }
        if (request.WithMeter != null)
        {
            _query["with_meter"] = JsonUtils.Serialize(request.WithMeter.Value);
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/price",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<ListBillingPricesResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.UpsertBillingPriceAsync(
    ///     new CreateBillingPriceRequestBody
    ///     {
    ///         BillingScheme = BillingPriceScheme.PerUnit,
    ///         Currency = "currency",
    ///         ExternalAccountId = "external_account_id",
    ///         Interval = "interval",
    ///         IsActive = true,
    ///         Price = 1,
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
    public async Task<UpsertBillingPriceResponse> UpsertBillingPriceAsync(
        CreateBillingPriceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "billing/price/upsert",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<UpsertBillingPriceResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.DeleteBillingProductAsync("billing_id");
    /// </code></example>
    public async Task<DeleteBillingProductResponse> DeleteBillingProductAsync(
        string billingId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "billing/product/{0}",
                        ValueConvert.ToPathParameterString(billingId)
                    ),
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<DeleteBillingProductResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.ListBillingProductPricesAsync(
    ///     new ListBillingProductPricesRequest
    ///     {
    ///         ForInitialPlan = true,
    ///         ForTrialExpiryPlan = true,
    ///         Interval = "interval",
    ///         IsActive = true,
    ///         Price = 1,
    ///         ProductId = "product_id",
    ///         ProviderType = BillingProviderType.Schematic,
    ///         Q = "q",
    ///         TiersMode = BillingTiersMode.Graduated,
    ///         UsageType = BillingPriceUsageType.Licensed,
    ///         WithMeter = true,
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<ListBillingProductPricesResponse> ListBillingProductPricesAsync(
        ListBillingProductPricesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        _query["product_ids"] = request.ProductIds;
        if (request.ForInitialPlan != null)
        {
            _query["for_initial_plan"] = JsonUtils.Serialize(request.ForInitialPlan.Value);
        }
        if (request.ForTrialExpiryPlan != null)
        {
            _query["for_trial_expiry_plan"] = JsonUtils.Serialize(request.ForTrialExpiryPlan.Value);
        }
        if (request.Interval != null)
        {
            _query["interval"] = request.Interval;
        }
        if (request.IsActive != null)
        {
            _query["is_active"] = JsonUtils.Serialize(request.IsActive.Value);
        }
        if (request.Price != null)
        {
            _query["price"] = request.Price.Value.ToString();
        }
        if (request.ProductId != null)
        {
            _query["product_id"] = request.ProductId;
        }
        if (request.ProviderType != null)
        {
            _query["provider_type"] = request.ProviderType.Value.Stringify();
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.TiersMode != null)
        {
            _query["tiers_mode"] = request.TiersMode.Value.Stringify();
        }
        if (request.UsageType != null)
        {
            _query["usage_type"] = request.UsageType.Value.Stringify();
        }
        if (request.WithMeter != null)
        {
            _query["with_meter"] = JsonUtils.Serialize(request.WithMeter.Value);
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/product/prices",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<ListBillingProductPricesResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.DeleteProductPriceAsync("billing_id");
    /// </code></example>
    public async Task<DeleteProductPriceResponse> DeleteProductPriceAsync(
        string billingId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "billing/product/prices/{0}",
                        ValueConvert.ToPathParameterString(billingId)
                    ),
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<DeleteProductPriceResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.UpsertBillingProductAsync(
    ///     new CreateBillingProductRequestBody
    ///     {
    ///         ExternalId = "external_id",
    ///         Name = "name",
    ///         Price = 1.1,
    ///     }
    /// );
    /// </code></example>
    public async Task<UpsertBillingProductResponse> UpsertBillingProductAsync(
        CreateBillingProductRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "billing/product/upsert",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<UpsertBillingProductResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.ListBillingProductsAsync(
    ///     new ListBillingProductsRequest
    ///     {
    ///         IsActive = true,
    ///         Name = "name",
    ///         PriceUsageType = BillingPriceUsageType.Licensed,
    ///         ProviderType = BillingProviderType.Schematic,
    ///         Q = "q",
    ///         WithOneTimeCharges = true,
    ///         WithPricesOnly = true,
    ///         WithZeroPrice = true,
    ///         WithoutLinkedToPlan = true,
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<ListBillingProductsResponse> ListBillingProductsAsync(
        ListBillingProductsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        if (request.IsActive != null)
        {
            _query["is_active"] = JsonUtils.Serialize(request.IsActive.Value);
        }
        if (request.Name != null)
        {
            _query["name"] = request.Name;
        }
        if (request.PriceUsageType != null)
        {
            _query["price_usage_type"] = request.PriceUsageType.Value.Stringify();
        }
        if (request.ProviderType != null)
        {
            _query["provider_type"] = request.ProviderType.Value.Stringify();
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithOneTimeCharges != null)
        {
            _query["with_one_time_charges"] = JsonUtils.Serialize(request.WithOneTimeCharges.Value);
        }
        if (request.WithPricesOnly != null)
        {
            _query["with_prices_only"] = JsonUtils.Serialize(request.WithPricesOnly.Value);
        }
        if (request.WithZeroPrice != null)
        {
            _query["with_zero_price"] = JsonUtils.Serialize(request.WithZeroPrice.Value);
        }
        if (request.WithoutLinkedToPlan != null)
        {
            _query["without_linked_to_plan"] = JsonUtils.Serialize(
                request.WithoutLinkedToPlan.Value
            );
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/products",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<ListBillingProductsResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    /// await client.Billing.CountBillingProductsAsync(
    ///     new CountBillingProductsRequest
    ///     {
    ///         IsActive = true,
    ///         Name = "name",
    ///         PriceUsageType = BillingPriceUsageType.Licensed,
    ///         ProviderType = BillingProviderType.Schematic,
    ///         Q = "q",
    ///         WithOneTimeCharges = true,
    ///         WithPricesOnly = true,
    ///         WithZeroPrice = true,
    ///         WithoutLinkedToPlan = true,
    ///         Limit = 1,
    ///         Offset = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<CountBillingProductsResponse> CountBillingProductsAsync(
        CountBillingProductsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        if (request.IsActive != null)
        {
            _query["is_active"] = JsonUtils.Serialize(request.IsActive.Value);
        }
        if (request.Name != null)
        {
            _query["name"] = request.Name;
        }
        if (request.PriceUsageType != null)
        {
            _query["price_usage_type"] = request.PriceUsageType.Value.Stringify();
        }
        if (request.ProviderType != null)
        {
            _query["provider_type"] = request.ProviderType.Value.Stringify();
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithOneTimeCharges != null)
        {
            _query["with_one_time_charges"] = JsonUtils.Serialize(request.WithOneTimeCharges.Value);
        }
        if (request.WithPricesOnly != null)
        {
            _query["with_prices_only"] = JsonUtils.Serialize(request.WithPricesOnly.Value);
        }
        if (request.WithZeroPrice != null)
        {
            _query["with_zero_price"] = JsonUtils.Serialize(request.WithZeroPrice.Value);
        }
        if (request.WithoutLinkedToPlan != null)
        {
            _query["without_linked_to_plan"] = JsonUtils.Serialize(
                request.WithoutLinkedToPlan.Value
            );
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
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "billing/products/count",
                    Query = _query,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<CountBillingProductsResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    ///                 Price = 1,
    ///                 PriceExternalId = "price_external_id",
    ///                 ProductExternalId = "product_external_id",
    ///                 Quantity = 1,
    ///                 UsageType = BillingPriceUsageType.Licensed,
    ///             },
    ///         },
    ///         SubscriptionExternalId = "subscription_external_id",
    ///         TotalPrice = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<UpsertBillingSubscriptionResponse> UpsertBillingSubscriptionAsync(
        CreateBillingSubscriptionRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "billing/subscription/upsert",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<UpsertBillingSubscriptionResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
}
