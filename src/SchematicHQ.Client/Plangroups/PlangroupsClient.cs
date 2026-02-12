using System.Net.Http;
using System.Text.Json;
using System.Threading;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class PlangroupsClient
{
    private RawClient _client;

    internal PlangroupsClient(RawClient client)
    {
        _client = client;
    }

    /// <example><code>
    /// await client.Plangroups.GetPlanGroupAsync();
    /// </code></example>
    public async Task<GetPlanGroupResponse> GetPlanGroupAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "plan-groups",
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
                return JsonUtils.Deserialize<GetPlanGroupResponse>(responseBody)!;
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
    ///         ShowPeriodToggle = true,
    ///         ShowZeroPriceAsFree = true,
    ///         SyncCustomerBillingDetails = true,
    ///     }
    /// );
    /// </code></example>
    public async Task<CreatePlanGroupResponse> CreatePlanGroupAsync(
        CreatePlanGroupRequestBody request,
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
                    Path = "plan-groups",
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
                return JsonUtils.Deserialize<CreatePlanGroupResponse>(responseBody)!;
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
    ///         ShowPeriodToggle = true,
    ///         ShowZeroPriceAsFree = true,
    ///         SyncCustomerBillingDetails = true,
    ///     }
    /// );
    /// </code></example>
    public async Task<UpdatePlanGroupResponse> UpdatePlanGroupAsync(
        string planGroupId,
        UpdatePlanGroupRequestBody request,
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
                        "plan-groups/{0}",
                        ValueConvert.ToPathParameterString(planGroupId)
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
                return JsonUtils.Deserialize<UpdatePlanGroupResponse>(responseBody)!;
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
