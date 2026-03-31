using global::System.Text.Json;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class CheckoutClient : ICheckoutClient
{
    private readonly RawClient _client;

    internal CheckoutClient(RawClient client)
    {
        _client = client;
    }

    private async Task<WithRawResponse<CheckoutInternalResponse>> InternalAsyncCore(
        ChangeSubscriptionInternalRequestBody request,
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
                    Path = "checkout-internal",
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
                var responseData = JsonUtils.Deserialize<CheckoutInternalResponse>(responseBody)!;
                return new WithRawResponse<CheckoutInternalResponse>()
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

    private async Task<WithRawResponse<GetCheckoutDataResponse>> GetCheckoutDataAsyncCore(
        CheckoutDataRequestBody request,
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
                    Path = "checkout-internal/data",
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
                var responseData = JsonUtils.Deserialize<GetCheckoutDataResponse>(responseBody)!;
                return new WithRawResponse<GetCheckoutDataResponse>()
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
        WithRawResponse<PreviewCheckoutInternalResponse>
    > PreviewCheckoutInternalAsyncCore(
        ChangeSubscriptionInternalRequestBody request,
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
                    Path = "checkout-internal/preview",
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
                var responseData = JsonUtils.Deserialize<PreviewCheckoutInternalResponse>(
                    responseBody
                )!;
                return new WithRawResponse<PreviewCheckoutInternalResponse>()
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

    private async Task<WithRawResponse<ManagePlanResponse>> ManagePlanAsyncCore(
        ManagePlanRequest request,
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
                    Path = "manage-plan",
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
                var responseData = JsonUtils.Deserialize<ManagePlanResponse>(responseBody)!;
                return new WithRawResponse<ManagePlanResponse>()
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

    private async Task<WithRawResponse<PreviewManagePlanResponse>> PreviewManagePlanAsyncCore(
        ManagePlanRequest request,
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
                    Path = "manage-plan/preview",
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
                var responseData = JsonUtils.Deserialize<PreviewManagePlanResponse>(responseBody)!;
                return new WithRawResponse<PreviewManagePlanResponse>()
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

    private async Task<WithRawResponse<CancelSubscriptionResponse>> CancelSubscriptionAsyncCore(
        CancelSubscriptionRequest request,
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
                    Path = "manage-plan/subscription/cancel",
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
                var responseData = JsonUtils.Deserialize<CancelSubscriptionResponse>(responseBody)!;
                return new WithRawResponse<CancelSubscriptionResponse>()
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
        WithRawResponse<UpdateCustomerSubscriptionTrialEndResponse>
    > UpdateCustomerSubscriptionTrialEndAsyncCore(
        string subscriptionId,
        UpdateTrialEndRequestBody request,
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
                    Method = HttpMethod.Put,
                    Path = string.Format(
                        "subscription/{0}/edit-trial-end",
                        ValueConvert.ToPathParameterString(subscriptionId)
                    ),
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
                var responseData =
                    JsonUtils.Deserialize<UpdateCustomerSubscriptionTrialEndResponse>(
                        responseBody
                    )!;
                return new WithRawResponse<UpdateCustomerSubscriptionTrialEndResponse>()
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
    /// await client.Checkout.InternalAsync(
    ///     new ChangeSubscriptionInternalRequestBody
    ///     {
    ///         AddOnIds = new List&lt;UpdateAddOnRequestBody&gt;()
    ///         {
    ///             new UpdateAddOnRequestBody { AddOnId = "add_on_id", PriceId = "price_id" },
    ///         },
    ///         CompanyId = "company_id",
    ///         CreditBundles = new List&lt;UpdateCreditBundleRequestBody&gt;()
    ///         {
    ///             new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1000000 },
    ///         },
    ///         NewPlanId = "new_plan_id",
    ///         NewPriceId = "new_price_id",
    ///         PayInAdvance = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
    ///         },
    ///         SkipTrial = true,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<CheckoutInternalResponse> InternalAsync(
        ChangeSubscriptionInternalRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CheckoutInternalResponse>(
            InternalAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Checkout.GetCheckoutDataAsync(
    ///     new CheckoutDataRequestBody { CompanyId = "company_id" }
    /// );
    /// </code></example>
    public WithRawResponseTask<GetCheckoutDataResponse> GetCheckoutDataAsync(
        CheckoutDataRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<GetCheckoutDataResponse>(
            GetCheckoutDataAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Checkout.PreviewCheckoutInternalAsync(
    ///     new ChangeSubscriptionInternalRequestBody
    ///     {
    ///         AddOnIds = new List&lt;UpdateAddOnRequestBody&gt;()
    ///         {
    ///             new UpdateAddOnRequestBody { AddOnId = "add_on_id", PriceId = "price_id" },
    ///         },
    ///         CompanyId = "company_id",
    ///         CreditBundles = new List&lt;UpdateCreditBundleRequestBody&gt;()
    ///         {
    ///             new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1000000 },
    ///         },
    ///         NewPlanId = "new_plan_id",
    ///         NewPriceId = "new_price_id",
    ///         PayInAdvance = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
    ///         },
    ///         SkipTrial = true,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<PreviewCheckoutInternalResponse> PreviewCheckoutInternalAsync(
        ChangeSubscriptionInternalRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<PreviewCheckoutInternalResponse>(
            PreviewCheckoutInternalAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Checkout.ManagePlanAsync(
    ///     new ManagePlanRequest
    ///     {
    ///         AddOnSelections = new List&lt;PlanSelection&gt;() { new PlanSelection { PlanId = "plan_id" } },
    ///         CompanyId = "company_id",
    ///         CreditBundles = new List&lt;UpdateCreditBundleRequestBody&gt;()
    ///         {
    ///             new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1000000 },
    ///         },
    ///         PayInAdvanceEntitlements = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ManagePlanResponse> ManagePlanAsync(
        ManagePlanRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ManagePlanResponse>(
            ManagePlanAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Checkout.PreviewManagePlanAsync(
    ///     new ManagePlanRequest
    ///     {
    ///         AddOnSelections = new List&lt;PlanSelection&gt;() { new PlanSelection { PlanId = "plan_id" } },
    ///         CompanyId = "company_id",
    ///         CreditBundles = new List&lt;UpdateCreditBundleRequestBody&gt;()
    ///         {
    ///             new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1000000 },
    ///         },
    ///         PayInAdvanceEntitlements = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<PreviewManagePlanResponse> PreviewManagePlanAsync(
        ManagePlanRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<PreviewManagePlanResponse>(
            PreviewManagePlanAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Checkout.CancelSubscriptionAsync(
    ///     new CancelSubscriptionRequest { CompanyId = "company_id" }
    /// );
    /// </code></example>
    public WithRawResponseTask<CancelSubscriptionResponse> CancelSubscriptionAsync(
        CancelSubscriptionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CancelSubscriptionResponse>(
            CancelSubscriptionAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Checkout.UpdateCustomerSubscriptionTrialEndAsync(
    ///     "subscription_id",
    ///     new UpdateTrialEndRequestBody()
    /// );
    /// </code></example>
    public WithRawResponseTask<UpdateCustomerSubscriptionTrialEndResponse> UpdateCustomerSubscriptionTrialEndAsync(
        string subscriptionId,
        UpdateTrialEndRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpdateCustomerSubscriptionTrialEndResponse>(
            UpdateCustomerSubscriptionTrialEndAsyncCore(
                subscriptionId,
                request,
                options,
                cancellationToken
            )
        );
    }
}
