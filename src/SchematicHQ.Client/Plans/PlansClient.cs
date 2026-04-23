using global::System.Text.Json;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class PlansClient : IPlansClient
{
    private readonly RawClient _client;

    internal PlansClient(RawClient client)
    {
        _client = client;
    }

    private async Task<WithRawResponse<UpdateCompanyPlansResponse>> UpdateCompanyPlansAsyncCore(
        string companyPlanId,
        UpdateCompanyPlansRequestBody request,
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
                        "company-plans/{0}",
                        ValueConvert.ToPathParameterString(companyPlanId)
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
                var responseData = JsonUtils.Deserialize<UpdateCompanyPlansResponse>(responseBody)!;
                return new WithRawResponse<UpdateCompanyPlansResponse>()
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
        WithRawResponse<ListCustomPlanBillingsResponse>
    > ListCustomPlanBillingsAsyncCore(
        ListCustomPlanBillingsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 6)
            .Add("company_id", request.CompanyId)
            .Add("plan_id", request.PlanId)
            .Add("status", request.Status)
            .Add("statuses", request.Statuses)
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
                    Path = "custom-plan-billings",
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
                var responseData = JsonUtils.Deserialize<ListCustomPlanBillingsResponse>(
                    responseBody
                )!;
                return new WithRawResponse<ListCustomPlanBillingsResponse>()
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
        WithRawResponse<RetryCustomPlanBillingResponse>
    > RetryCustomPlanBillingAsyncCore(
        string customPlanBillingId,
        RetryCustomPlanBillingRequestBody request,
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
                        "custom-plan-billings/{0}/retry",
                        ValueConvert.ToPathParameterString(customPlanBillingId)
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
                var responseData = JsonUtils.Deserialize<RetryCustomPlanBillingResponse>(
                    responseBody
                )!;
                return new WithRawResponse<RetryCustomPlanBillingResponse>()
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

    private async Task<WithRawResponse<CreateCustomPlanResponse>> CreateCustomPlanAsyncCore(
        CreateCustomPlanRequestBody request,
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
                    Path = "custom-plans",
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
                var responseData = JsonUtils.Deserialize<CreateCustomPlanResponse>(responseBody)!;
                return new WithRawResponse<CreateCustomPlanResponse>()
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

    private async Task<WithRawResponse<ListPlansResponse>> ListPlansAsyncCore(
        ListPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 15)
            .Add("company_id", request.CompanyId)
            .Add("exclude_company_scoped", request.ExcludeCompanyScoped)
            .Add("for_fallback_plan", request.ForFallbackPlan)
            .Add("for_initial_plan", request.ForInitialPlan)
            .Add("for_trial_expiry_plan", request.ForTrialExpiryPlan)
            .Add("has_product_id", request.HasProductId)
            .Add("ids", request.Ids)
            .Add("include_draft_versions", request.IncludeDraftVersions)
            .Add("plan_type", request.PlanType)
            .Add("q", request.Q)
            .Add("scoped_to_company_id", request.ScopedToCompanyId)
            .Add("without_entitlement_for", request.WithoutEntitlementFor)
            .Add("without_paid_product_id", request.WithoutPaidProductId)
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
                    Path = "plans",
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
                var responseData = JsonUtils.Deserialize<ListPlansResponse>(responseBody)!;
                return new WithRawResponse<ListPlansResponse>()
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

    private async Task<WithRawResponse<CreatePlanResponse>> CreatePlanAsyncCore(
        CreatePlanRequestBody request,
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
                    Path = "plans",
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
                var responseData = JsonUtils.Deserialize<CreatePlanResponse>(responseBody)!;
                return new WithRawResponse<CreatePlanResponse>()
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

    private async Task<WithRawResponse<GetPlanResponse>> GetPlanAsyncCore(
        string planId,
        GetPlanRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 1)
            .Add("plan_version_id", request.PlanVersionId)
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
                    Path = string.Format("plans/{0}", ValueConvert.ToPathParameterString(planId)),
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
                var responseData = JsonUtils.Deserialize<GetPlanResponse>(responseBody)!;
                return new WithRawResponse<GetPlanResponse>()
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

    private async Task<WithRawResponse<UpdatePlanResponse>> UpdatePlanAsyncCore(
        string planId,
        UpdatePlanRequestBody request,
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
                    Path = string.Format("plans/{0}", ValueConvert.ToPathParameterString(planId)),
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
                var responseData = JsonUtils.Deserialize<UpdatePlanResponse>(responseBody)!;
                return new WithRawResponse<UpdatePlanResponse>()
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

    private async Task<WithRawResponse<DeletePlanResponse>> DeletePlanAsyncCore(
        string planId,
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
                    Path = string.Format("plans/{0}", ValueConvert.ToPathParameterString(planId)),
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
                var responseData = JsonUtils.Deserialize<DeletePlanResponse>(responseBody)!;
                return new WithRawResponse<DeletePlanResponse>()
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
        WithRawResponse<UpsertBillingProductPlanResponse>
    > UpsertBillingProductPlanAsyncCore(
        string planId,
        UpsertBillingProductRequestBody request,
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
                        "plans/{0}/billing_products",
                        ValueConvert.ToPathParameterString(planId)
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
                var responseData = JsonUtils.Deserialize<UpsertBillingProductPlanResponse>(
                    responseBody
                )!;
                return new WithRawResponse<UpsertBillingProductPlanResponse>()
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
        WithRawResponse<UpsertPlanForBillingProductResponse>
    > UpsertPlanForBillingProductAsyncCore(
        CreateBillingLinkedPlanRequestBody request,
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
                    Path = "plans/billing-linked",
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
                var responseData = JsonUtils.Deserialize<UpsertPlanForBillingProductResponse>(
                    responseBody
                )!;
                return new WithRawResponse<UpsertPlanForBillingProductResponse>()
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
        WithRawResponse<ListBillingProductMatchCompaniesResponse>
    > ListBillingProductMatchCompaniesAsyncCore(
        ListBillingProductMatchCompaniesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 4)
            .Add("plan_id", request.PlanId)
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
                    Path = "plans/billing-product-match-companies",
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
                var responseData = JsonUtils.Deserialize<ListBillingProductMatchCompaniesResponse>(
                    responseBody
                )!;
                return new WithRawResponse<ListBillingProductMatchCompaniesResponse>()
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
        WithRawResponse<CountBillingProductMatchCompaniesResponse>
    > CountBillingProductMatchCompaniesAsyncCore(
        CountBillingProductMatchCompaniesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 4)
            .Add("plan_id", request.PlanId)
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
                    Path = "plans/billing-product-match-companies/count",
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
                var responseData = JsonUtils.Deserialize<CountBillingProductMatchCompaniesResponse>(
                    responseBody
                )!;
                return new WithRawResponse<CountBillingProductMatchCompaniesResponse>()
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

    private async Task<WithRawResponse<CountPlansResponse>> CountPlansAsyncCore(
        CountPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 15)
            .Add("company_id", request.CompanyId)
            .Add("exclude_company_scoped", request.ExcludeCompanyScoped)
            .Add("for_fallback_plan", request.ForFallbackPlan)
            .Add("for_initial_plan", request.ForInitialPlan)
            .Add("for_trial_expiry_plan", request.ForTrialExpiryPlan)
            .Add("has_product_id", request.HasProductId)
            .Add("ids", request.Ids)
            .Add("include_draft_versions", request.IncludeDraftVersions)
            .Add("plan_type", request.PlanType)
            .Add("q", request.Q)
            .Add("scoped_to_company_id", request.ScopedToCompanyId)
            .Add("without_entitlement_for", request.WithoutEntitlementFor)
            .Add("without_paid_product_id", request.WithoutPaidProductId)
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
                    Path = "plans/count",
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
                var responseData = JsonUtils.Deserialize<CountPlansResponse>(responseBody)!;
                return new WithRawResponse<CountPlansResponse>()
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

    private async Task<WithRawResponse<ListPlanIssuesResponse>> ListPlanIssuesAsyncCore(
        ListPlanIssuesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 2)
            .Add("plan_id", request.PlanId)
            .Add("plan_version_id", request.PlanVersionId)
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
                    Path = "plans/issues",
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
                var responseData = JsonUtils.Deserialize<ListPlanIssuesResponse>(responseBody)!;
                return new WithRawResponse<ListPlanIssuesResponse>()
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

    private async Task<WithRawResponse<DeletePlanVersionResponse>> DeletePlanVersionAsyncCore(
        string planId,
        DeletePlanVersionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 1)
            .Add("promote_archived_version", request.PromoteArchivedVersion)
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
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "plans/version/{0}",
                        ValueConvert.ToPathParameterString(planId)
                    ),
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
                var responseData = JsonUtils.Deserialize<DeletePlanVersionResponse>(responseBody)!;
                return new WithRawResponse<DeletePlanVersionResponse>()
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

    private async Task<WithRawResponse<PublishPlanVersionResponse>> PublishPlanVersionAsyncCore(
        string planId,
        PublishPlanVersionRequestBody request,
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
                        "plans/version/{0}/publish",
                        ValueConvert.ToPathParameterString(planId)
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
                var responseData = JsonUtils.Deserialize<PublishPlanVersionResponse>(responseBody)!;
                return new WithRawResponse<PublishPlanVersionResponse>()
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
    /// await client.Plans.UpdateCompanyPlansAsync(
    ///     "company_plan_id",
    ///     new UpdateCompanyPlansRequestBody { AddOnIds = new List&lt;string&gt;() { "add_on_ids" } }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpdateCompanyPlansResponse> UpdateCompanyPlansAsync(
        string companyPlanId,
        UpdateCompanyPlansRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpdateCompanyPlansResponse>(
            UpdateCompanyPlansAsyncCore(companyPlanId, request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.ListCustomPlanBillingsAsync(
    ///     new ListCustomPlanBillingsRequest
    ///     {
    ///         CompanyId = "company_id",
    ///         PlanId = "plan_id",
    ///         Status = CustomPlanBillingStatus.Active,
    ///         Statuses = [new List&lt;CustomPlanBillingStatus&gt;() { CustomPlanBillingStatus.Active }],
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListCustomPlanBillingsResponse> ListCustomPlanBillingsAsync(
        ListCustomPlanBillingsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListCustomPlanBillingsResponse>(
            ListCustomPlanBillingsAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.RetryCustomPlanBillingAsync(
    ///     "custom_plan_billing_id",
    ///     new RetryCustomPlanBillingRequestBody
    ///     {
    ///         CustomerEmail = "customer_email",
    ///         PayInAdvance = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<RetryCustomPlanBillingResponse> RetryCustomPlanBillingAsync(
        string customPlanBillingId,
        RetryCustomPlanBillingRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<RetryCustomPlanBillingResponse>(
            RetryCustomPlanBillingAsyncCore(
                customPlanBillingId,
                request,
                options,
                cancellationToken
            )
        );
    }

    /// <example><code>
    /// await client.Plans.CreateCustomPlanAsync(
    ///     new CreateCustomPlanRequestBody
    ///     {
    ///         CompanyId = "company_id",
    ///         Description = "description",
    ///         Name = "name",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<CreateCustomPlanResponse> CreateCustomPlanAsync(
        CreateCustomPlanRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CreateCustomPlanResponse>(
            CreateCustomPlanAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.ListPlansAsync(
    ///     new ListPlansRequest
    ///     {
    ///         CompanyId = "company_id",
    ///         ExcludeCompanyScoped = true,
    ///         ForFallbackPlan = true,
    ///         ForInitialPlan = true,
    ///         ForTrialExpiryPlan = true,
    ///         HasProductId = true,
    ///         Ids = [new List&lt;string&gt;() { "ids" }],
    ///         IncludeDraftVersions = true,
    ///         PlanType = PlanType.Plan,
    ///         Q = "q",
    ///         ScopedToCompanyId = "scoped_to_company_id",
    ///         WithoutEntitlementFor = "without_entitlement_for",
    ///         WithoutPaidProductId = true,
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListPlansResponse> ListPlansAsync(
        ListPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListPlansResponse>(
            ListPlansAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.CreatePlanAsync(
    ///     new CreatePlanRequestBody
    ///     {
    ///         Description = "description",
    ///         Name = "name",
    ///         PlanType = PlanType.Plan,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<CreatePlanResponse> CreatePlanAsync(
        CreatePlanRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CreatePlanResponse>(
            CreatePlanAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.GetPlanAsync(
    ///     "plan_id",
    ///     new GetPlanRequest { PlanVersionId = "plan_version_id" }
    /// );
    /// </code></example>
    public WithRawResponseTask<GetPlanResponse> GetPlanAsync(
        string planId,
        GetPlanRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<GetPlanResponse>(
            GetPlanAsyncCore(planId, request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.UpdatePlanAsync("plan_id", new UpdatePlanRequestBody { Name = "name" });
    /// </code></example>
    public WithRawResponseTask<UpdatePlanResponse> UpdatePlanAsync(
        string planId,
        UpdatePlanRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpdatePlanResponse>(
            UpdatePlanAsyncCore(planId, request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.DeletePlanAsync("plan_id");
    /// </code></example>
    public WithRawResponseTask<DeletePlanResponse> DeletePlanAsync(
        string planId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<DeletePlanResponse>(
            DeletePlanAsyncCore(planId, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.UpsertBillingProductPlanAsync(
    ///     "plan_id",
    ///     new UpsertBillingProductRequestBody { ChargeType = ChargeType.Free, IsTrialable = true }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertBillingProductPlanResponse> UpsertBillingProductPlanAsync(
        string planId,
        UpsertBillingProductRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertBillingProductPlanResponse>(
            UpsertBillingProductPlanAsyncCore(planId, request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.UpsertPlanForBillingProductAsync(
    ///     new CreateBillingLinkedPlanRequestBody
    ///     {
    ///         BillingProvider = BillingProviderType.Orb,
    ///         Description = "description",
    ///         ExternalResourceId = "external_resource_id",
    ///         Name = "name",
    ///         PlanType = PlanType.Plan,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpsertPlanForBillingProductResponse> UpsertPlanForBillingProductAsync(
        CreateBillingLinkedPlanRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpsertPlanForBillingProductResponse>(
            UpsertPlanForBillingProductAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.ListBillingProductMatchCompaniesAsync(
    ///     new ListBillingProductMatchCompaniesRequest
    ///     {
    ///         PlanId = "plan_id",
    ///         Q = "q",
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListBillingProductMatchCompaniesResponse> ListBillingProductMatchCompaniesAsync(
        ListBillingProductMatchCompaniesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListBillingProductMatchCompaniesResponse>(
            ListBillingProductMatchCompaniesAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.CountBillingProductMatchCompaniesAsync(
    ///     new CountBillingProductMatchCompaniesRequest
    ///     {
    ///         PlanId = "plan_id",
    ///         Q = "q",
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<CountBillingProductMatchCompaniesResponse> CountBillingProductMatchCompaniesAsync(
        CountBillingProductMatchCompaniesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CountBillingProductMatchCompaniesResponse>(
            CountBillingProductMatchCompaniesAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.CountPlansAsync(
    ///     new CountPlansRequest
    ///     {
    ///         CompanyId = "company_id",
    ///         ExcludeCompanyScoped = true,
    ///         ForFallbackPlan = true,
    ///         ForInitialPlan = true,
    ///         ForTrialExpiryPlan = true,
    ///         HasProductId = true,
    ///         Ids = [new List&lt;string&gt;() { "ids" }],
    ///         IncludeDraftVersions = true,
    ///         PlanType = PlanType.Plan,
    ///         Q = "q",
    ///         ScopedToCompanyId = "scoped_to_company_id",
    ///         WithoutEntitlementFor = "without_entitlement_for",
    ///         WithoutPaidProductId = true,
    ///         Limit = 1000000,
    ///         Offset = 1000000,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<CountPlansResponse> CountPlansAsync(
        CountPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CountPlansResponse>(
            CountPlansAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.ListPlanIssuesAsync(
    ///     new ListPlanIssuesRequest { PlanId = "plan_id", PlanVersionId = "plan_version_id" }
    /// );
    /// </code></example>
    public WithRawResponseTask<ListPlanIssuesResponse> ListPlanIssuesAsync(
        ListPlanIssuesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ListPlanIssuesResponse>(
            ListPlanIssuesAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.DeletePlanVersionAsync(
    ///     "plan_id",
    ///     new DeletePlanVersionRequest { PromoteArchivedVersion = true }
    /// );
    /// </code></example>
    public WithRawResponseTask<DeletePlanVersionResponse> DeletePlanVersionAsync(
        string planId,
        DeletePlanVersionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<DeletePlanVersionResponse>(
            DeletePlanVersionAsyncCore(planId, request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plans.PublishPlanVersionAsync(
    ///     "plan_id",
    ///     new PublishPlanVersionRequestBody
    ///     {
    ///         ExcludedCompanyIds = new List&lt;string&gt;() { "excluded_company_ids" },
    ///         MigrationStrategy = PlanVersionMigrationStrategy.Immediate,
    ///         PayInAdvance = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<PublishPlanVersionResponse> PublishPlanVersionAsync(
        string planId,
        PublishPlanVersionRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<PublishPlanVersionResponse>(
            PublishPlanVersionAsyncCore(planId, request, options, cancellationToken)
        );
    }
}
