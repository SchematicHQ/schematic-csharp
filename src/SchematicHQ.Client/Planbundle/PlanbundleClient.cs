using System.Net.Http;
using System.Text.Json;
using System.Threading;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class PlanbundleClient
{
    private RawClient _client;

    internal PlanbundleClient(RawClient client)
    {
        _client = client;
    }

    /// <example><code>
    /// await client.Planbundle.CreatePlanBundleAsync(
    ///     new CreatePlanBundleRequestBody
    ///     {
    ///         Entitlements = new List&lt;PlanBundleEntitlementRequestBody&gt;()
    ///         {
    ///             new PlanBundleEntitlementRequestBody { Action = PlanBundleAction.Create },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<CreatePlanBundleResponse> CreatePlanBundleAsync(
        CreatePlanBundleRequestBody request,
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
                    Path = "plan-bundles",
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
                return JsonUtils.Deserialize<CreatePlanBundleResponse>(responseBody)!;
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
    /// await client.Planbundle.UpdatePlanBundleAsync(
    ///     "plan_bundle_id",
    ///     new UpdatePlanBundleRequestBody
    ///     {
    ///         Entitlements = new List&lt;PlanBundleEntitlementRequestBody&gt;()
    ///         {
    ///             new PlanBundleEntitlementRequestBody { Action = PlanBundleAction.Create },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<UpdatePlanBundleResponse> UpdatePlanBundleAsync(
        string planBundleId,
        UpdatePlanBundleRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Put,
                    Path = string.Format(
                        "plan-bundles/{0}",
                        ValueConvert.ToPathParameterString(planBundleId)
                    ),
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
                return JsonUtils.Deserialize<UpdatePlanBundleResponse>(responseBody)!;
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
