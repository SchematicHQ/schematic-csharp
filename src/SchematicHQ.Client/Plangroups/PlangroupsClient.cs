using global::System.Text.Json;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class PlangroupsClient : IPlangroupsClient
{
    private readonly RawClient _client;

    internal PlangroupsClient(RawClient client)
    {
        _client = client;
    }

    private async Task<WithRawResponse<GetPlanGroupResponse>> GetPlanGroupAsyncCore(
        GetPlanGroupRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 1)
            .Add("include_company_counts", request.IncludeCompanyCounts)
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
                    Path = "plan-groups",
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
                var responseData = JsonUtils.Deserialize<GetPlanGroupResponse>(responseBody)!;
                return new WithRawResponse<GetPlanGroupResponse>()
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

    private async Task<WithRawResponse<CreatePlanGroupResponse>> CreatePlanGroupAsyncCore(
        CreatePlanGroupRequestBody request,
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
                    Path = "plan-groups",
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
                var responseData = JsonUtils.Deserialize<CreatePlanGroupResponse>(responseBody)!;
                return new WithRawResponse<CreatePlanGroupResponse>()
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

    private async Task<WithRawResponse<UpdatePlanGroupResponse>> UpdatePlanGroupAsyncCore(
        string planGroupId,
        UpdatePlanGroupRequestBody request,
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
                        "plan-groups/{0}",
                        ValueConvert.ToPathParameterString(planGroupId)
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
                var responseData = JsonUtils.Deserialize<UpdatePlanGroupResponse>(responseBody)!;
                return new WithRawResponse<UpdatePlanGroupResponse>()
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
    /// await client.Plangroups.GetPlanGroupAsync(new GetPlanGroupRequest { IncludeCompanyCounts = true });
    /// </code></example>
    public WithRawResponseTask<GetPlanGroupResponse> GetPlanGroupAsync(
        GetPlanGroupRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<GetPlanGroupResponse>(
            GetPlanGroupAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plangroups.CreatePlanGroupAsync(
    ///     new CreatePlanGroupRequestBody
    ///     {
    ///         AddOnIds = new List&lt;string&gt;() { "add_on_ids" },
    ///         CheckoutCollectAddress = true,
    ///         CheckoutCollectEmail = true,
    ///         CheckoutCollectPhone = true,
    ///         EnableTaxCollection = true,
    ///         OrderedAddOns = new List&lt;OrderedPlansInGroup&gt;()
    ///         {
    ///             new OrderedPlansInGroup { PlanId = "plan_id" },
    ///         },
    ///         OrderedBundleList = new List&lt;PlanGroupBundleOrder&gt;()
    ///         {
    ///             new PlanGroupBundleOrder { BundleId = "bundleId" },
    ///         },
    ///         OrderedPlans = new List&lt;OrderedPlansInGroup&gt;()
    ///         {
    ///             new OrderedPlansInGroup { PlanId = "plan_id" },
    ///         },
    ///         PreventDowngradesWhenOverLimit = true,
    ///         PreventSelfServiceDowngrade = true,
    ///         ProrationBehavior = ProrationBehavior.CreateProrations,
    ///         ShowAsMonthlyPrices = true,
    ///         ShowCredits = true,
    ///         ShowFeatureDescription = true,
    ///         ShowHardLimit = true,
    ///         ShowPeriodToggle = true,
    ///         ShowZeroPriceAsFree = true,
    ///         SyncCustomerBillingDetails = true,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<CreatePlanGroupResponse> CreatePlanGroupAsync(
        CreatePlanGroupRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CreatePlanGroupResponse>(
            CreatePlanGroupAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Plangroups.UpdatePlanGroupAsync(
    ///     "plan_group_id",
    ///     new UpdatePlanGroupRequestBody
    ///     {
    ///         AddOnIds = new List&lt;string&gt;() { "add_on_ids" },
    ///         CheckoutCollectAddress = true,
    ///         CheckoutCollectEmail = true,
    ///         CheckoutCollectPhone = true,
    ///         EnableTaxCollection = true,
    ///         OrderedAddOns = new List&lt;OrderedPlansInGroup&gt;()
    ///         {
    ///             new OrderedPlansInGroup { PlanId = "plan_id" },
    ///         },
    ///         OrderedBundleList = new List&lt;PlanGroupBundleOrder&gt;()
    ///         {
    ///             new PlanGroupBundleOrder { BundleId = "bundleId" },
    ///         },
    ///         OrderedPlans = new List&lt;OrderedPlansInGroup&gt;()
    ///         {
    ///             new OrderedPlansInGroup { PlanId = "plan_id" },
    ///         },
    ///         PreventDowngradesWhenOverLimit = true,
    ///         PreventSelfServiceDowngrade = true,
    ///         ProrationBehavior = ProrationBehavior.CreateProrations,
    ///         ShowAsMonthlyPrices = true,
    ///         ShowCredits = true,
    ///         ShowFeatureDescription = true,
    ///         ShowHardLimit = true,
    ///         ShowPeriodToggle = true,
    ///         ShowZeroPriceAsFree = true,
    ///         SyncCustomerBillingDetails = true,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<UpdatePlanGroupResponse> UpdatePlanGroupAsync(
        string planGroupId,
        UpdatePlanGroupRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<UpdatePlanGroupResponse>(
            UpdatePlanGroupAsyncCore(planGroupId, request, options, cancellationToken)
        );
    }
}
