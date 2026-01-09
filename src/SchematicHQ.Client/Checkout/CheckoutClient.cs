using System.Net.Http;
using System.Text.Json;
using System.Threading;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class CheckoutClient
{
    private RawClient _client;

    internal CheckoutClient(RawClient client)
    {
        _client = client;
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
    ///             new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1 },
    ///         },
    ///         NewPlanId = "new_plan_id",
    ///         NewPriceId = "new_price_id",
    ///         PayInAdvance = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1 },
    ///         },
    ///         SkipTrial = true,
    ///     }
    /// );
    /// </code></example>
    public async Task<CheckoutInternalResponse> InternalAsync(
        ChangeSubscriptionInternalRequestBody request,
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
                    Path = "checkout-internal",
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
                return JsonUtils.Deserialize<CheckoutInternalResponse>(responseBody)!;
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
    /// await client.Checkout.GetCheckoutDataAsync(
    ///     new CheckoutDataRequestBody { CompanyId = "company_id" }
    /// );
    /// </code></example>
    public async Task<GetCheckoutDataResponse> GetCheckoutDataAsync(
        CheckoutDataRequestBody request,
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
                    Path = "checkout-internal/data",
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
                return JsonUtils.Deserialize<GetCheckoutDataResponse>(responseBody)!;
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
    ///             new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1 },
    ///         },
    ///         NewPlanId = "new_plan_id",
    ///         NewPriceId = "new_price_id",
    ///         PayInAdvance = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1 },
    ///         },
    ///         SkipTrial = true,
    ///     }
    /// );
    /// </code></example>
    public async Task<PreviewCheckoutInternalResponse> PreviewCheckoutInternalAsync(
        ChangeSubscriptionInternalRequestBody request,
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
                    Path = "checkout-internal/preview",
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
                return JsonUtils.Deserialize<PreviewCheckoutInternalResponse>(responseBody)!;
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
    /// await client.Checkout.ManagePlanAsync(
    ///     new ManagePlanRequest
    ///     {
    ///         AddOnSelections = new List&lt;PlanSelection&gt;() { new PlanSelection { PlanId = "plan_id" } },
    ///         CompanyId = "company_id",
    ///         CreditBundles = new List&lt;UpdateCreditBundleRequestBody&gt;()
    ///         {
    ///             new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1 },
    ///         },
    ///         PayInAdvanceEntitlements = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1 },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<ManagePlanResponse> ManagePlanAsync(
        ManagePlanRequest request,
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
                    Path = "manage-plan",
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
                return JsonUtils.Deserialize<ManagePlanResponse>(responseBody)!;
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
    /// await client.Checkout.PreviewManagePlanAsync(
    ///     new ManagePlanRequest
    ///     {
    ///         AddOnSelections = new List&lt;PlanSelection&gt;() { new PlanSelection { PlanId = "plan_id" } },
    ///         CompanyId = "company_id",
    ///         CreditBundles = new List&lt;UpdateCreditBundleRequestBody&gt;()
    ///         {
    ///             new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1 },
    ///         },
    ///         PayInAdvanceEntitlements = new List&lt;UpdatePayInAdvanceRequestBody&gt;()
    ///         {
    ///             new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1 },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<PreviewManagePlanResponse> PreviewManagePlanAsync(
        ManagePlanRequest request,
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
                    Path = "manage-plan/preview",
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
                return JsonUtils.Deserialize<PreviewManagePlanResponse>(responseBody)!;
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
    /// await client.Checkout.CancelSubscriptionAsync(
    ///     new CancelSubscriptionRequest { CompanyId = "company_id" }
    /// );
    /// </code></example>
    public async Task<CancelSubscriptionResponse> CancelSubscriptionAsync(
        CancelSubscriptionRequest request,
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
                    Path = "manage-plan/subscription/cancel",
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
                return JsonUtils.Deserialize<CancelSubscriptionResponse>(responseBody)!;
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
    /// await client.Checkout.UpdateCustomerSubscriptionTrialEndAsync(
    ///     "subscription_id",
    ///     new UpdateTrialEndRequestBody()
    /// );
    /// </code></example>
    public async Task<UpdateCustomerSubscriptionTrialEndResponse> UpdateCustomerSubscriptionTrialEndAsync(
        string subscriptionId,
        UpdateTrialEndRequestBody request,
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
                        "subscription/{0}/edit-trial-end",
                        ValueConvert.ToPathParameterString(subscriptionId)
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
                return JsonUtils.Deserialize<UpdateCustomerSubscriptionTrialEndResponse>(
                    responseBody
                )!;
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
