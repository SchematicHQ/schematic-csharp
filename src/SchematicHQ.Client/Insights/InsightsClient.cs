using global::System.Text.Json;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class InsightsClient : IInsightsClient
{
    private readonly RawClient _client;

    internal InsightsClient(RawClient client)
    {
        _client = client;
    }

    private async Task<WithRawResponse<GetActivityResponse>> GetActivityAsyncCore(
        GetActivityRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 1)
            .Add("limit", request.Limit)
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
                    Path = "insights/activity",
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
                var responseData = JsonUtils.Deserialize<GetActivityResponse>(responseBody)!;
                return new WithRawResponse<GetActivityResponse>()
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

    private async Task<
        WithRawResponse<GetEnvironmentFeatureUsageTimeSeriesResponse>
    > GetEnvironmentFeatureUsageTimeSeriesAsyncCore(
        GetEnvironmentFeatureUsageTimeSeriesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 4)
            .Add("end_time", request.EndTime)
            .Add("feature_id", request.FeatureId)
            .Add("granularity", request.Granularity)
            .Add("start_time", request.StartTime)
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
                    Path = "insights/feature-usage-timeseries",
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
                var responseData =
                    JsonUtils.Deserialize<GetEnvironmentFeatureUsageTimeSeriesResponse>(
                        responseBody
                    )!;
                return new WithRawResponse<GetEnvironmentFeatureUsageTimeSeriesResponse>()
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

    private async Task<WithRawResponse<GetPlanGrowthResponse>> GetPlanGrowthAsyncCore(
        GetPlanGrowthRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 1)
            .Add("months", request.Months)
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
                    Path = "insights/plan-growth",
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
                var responseData = JsonUtils.Deserialize<GetPlanGrowthResponse>(responseBody)!;
                return new WithRawResponse<GetPlanGrowthResponse>()
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

    private async Task<WithRawResponse<GetSummaryResponse>> GetSummaryAsyncCore(
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
                    Method = HttpMethod.Get,
                    Path = "insights/summary",
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
                var responseData = JsonUtils.Deserialize<GetSummaryResponse>(responseBody)!;
                return new WithRawResponse<GetSummaryResponse>()
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

    private async Task<
        WithRawResponse<GetTopFeaturesByUsageResponse>
    > GetTopFeaturesByUsageAsyncCore(
        GetTopFeaturesByUsageRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 3)
            .Add("end_time", request.EndTime)
            .Add("limit", request.Limit)
            .Add("start_time", request.StartTime)
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
                    Path = "insights/top-features",
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
                var responseData = JsonUtils.Deserialize<GetTopFeaturesByUsageResponse>(
                    responseBody
                )!;
                return new WithRawResponse<GetTopFeaturesByUsageResponse>()
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

    private async Task<
        WithRawResponse<GetEnvironmentTraitUsageTimeSeriesResponse>
    > GetEnvironmentTraitUsageTimeSeriesAsyncCore(
        GetEnvironmentTraitUsageTimeSeriesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 4)
            .Add("end_time", request.EndTime)
            .Add("feature_id", request.FeatureId)
            .Add("granularity", request.Granularity)
            .Add("start_time", request.StartTime)
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
                    Path = "insights/trait-usage-timeseries",
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
                var responseData =
                    JsonUtils.Deserialize<GetEnvironmentTraitUsageTimeSeriesResponse>(
                        responseBody
                    )!;
                return new WithRawResponse<GetEnvironmentTraitUsageTimeSeriesResponse>()
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

    /// <example><code>
    /// await client.Insights.GetActivityAsync(new GetActivityRequest { Limit = 1000000 });
    /// </code></example>
    public WithRawResponseTask<GetActivityResponse> GetActivityAsync(
        GetActivityRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<GetActivityResponse>(
            GetActivityAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Insights.GetEnvironmentFeatureUsageTimeSeriesAsync(
    ///     new GetEnvironmentFeatureUsageTimeSeriesRequest
    ///     {
    ///         EndTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
    ///         FeatureId = "feature_id",
    ///         Granularity = TimeSeriesGranularity.Daily,
    ///         StartTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<GetEnvironmentFeatureUsageTimeSeriesResponse> GetEnvironmentFeatureUsageTimeSeriesAsync(
        GetEnvironmentFeatureUsageTimeSeriesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<GetEnvironmentFeatureUsageTimeSeriesResponse>(
            GetEnvironmentFeatureUsageTimeSeriesAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Insights.GetPlanGrowthAsync(new GetPlanGrowthRequest { Months = 1000000 });
    /// </code></example>
    public WithRawResponseTask<GetPlanGrowthResponse> GetPlanGrowthAsync(
        GetPlanGrowthRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<GetPlanGrowthResponse>(
            GetPlanGrowthAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Insights.GetSummaryAsync();
    /// </code></example>
    public WithRawResponseTask<GetSummaryResponse> GetSummaryAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<GetSummaryResponse>(
            GetSummaryAsyncCore(options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Insights.GetTopFeaturesByUsageAsync(
    ///     new GetTopFeaturesByUsageRequest
    ///     {
    ///         EndTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
    ///         Limit = 1000000,
    ///         StartTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<GetTopFeaturesByUsageResponse> GetTopFeaturesByUsageAsync(
        GetTopFeaturesByUsageRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<GetTopFeaturesByUsageResponse>(
            GetTopFeaturesByUsageAsyncCore(request, options, cancellationToken)
        );
    }

    /// <example><code>
    /// await client.Insights.GetEnvironmentTraitUsageTimeSeriesAsync(
    ///     new GetEnvironmentTraitUsageTimeSeriesRequest
    ///     {
    ///         EndTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
    ///         FeatureId = "feature_id",
    ///         Granularity = TimeSeriesGranularity.Daily,
    ///         StartTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<GetEnvironmentTraitUsageTimeSeriesResponse> GetEnvironmentTraitUsageTimeSeriesAsync(
        GetEnvironmentTraitUsageTimeSeriesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<GetEnvironmentTraitUsageTimeSeriesResponse>(
            GetEnvironmentTraitUsageTimeSeriesAsyncCore(request, options, cancellationToken)
        );
    }
}
