using System.Net.Http;
using System.Text.Json;
using System.Threading;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public partial class FeaturesClient
{
    private RawClient _client;

    internal FeaturesClient(RawClient client)
    {
        _client = client;
    }

    /// <example>
    /// <code>
    /// await client.Features.CountAudienceCompaniesAsync(
    ///     new AudienceRequestBody
    ///     {
    ///         ConditionGroups = new List&lt;CreateOrUpdateConditionGroupRequestBody&gt;()
    ///         {
    ///             new CreateOrUpdateConditionGroupRequestBody
    ///             {
    ///                 Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///                 {
    ///                     new CreateOrUpdateConditionRequestBody
    ///                     {
    ///                         ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                         Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                         ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///                     },
    ///                 },
    ///             },
    ///         },
    ///         Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///         {
    ///             new CreateOrUpdateConditionRequestBody
    ///             {
    ///                 ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                 Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                 ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<CountAudienceCompaniesResponse> CountAudienceCompaniesAsync(
        AudienceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "audience/count-companies",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountAudienceCompaniesResponse>(responseBody)!;
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
    /// await client.Features.CountAudienceUsersAsync(
    ///     new AudienceRequestBody
    ///     {
    ///         ConditionGroups = new List&lt;CreateOrUpdateConditionGroupRequestBody&gt;()
    ///         {
    ///             new CreateOrUpdateConditionGroupRequestBody
    ///             {
    ///                 Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///                 {
    ///                     new CreateOrUpdateConditionRequestBody
    ///                     {
    ///                         ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                         Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                         ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///                     },
    ///                 },
    ///             },
    ///         },
    ///         Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///         {
    ///             new CreateOrUpdateConditionRequestBody
    ///             {
    ///                 ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                 Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                 ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<CountAudienceUsersResponse> CountAudienceUsersAsync(
        AudienceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "audience/count-users",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountAudienceUsersResponse>(responseBody)!;
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
    /// await client.Features.ListAudienceCompaniesAsync(
    ///     new AudienceRequestBody
    ///     {
    ///         ConditionGroups = new List&lt;CreateOrUpdateConditionGroupRequestBody&gt;()
    ///         {
    ///             new CreateOrUpdateConditionGroupRequestBody
    ///             {
    ///                 Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///                 {
    ///                     new CreateOrUpdateConditionRequestBody
    ///                     {
    ///                         ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                         Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                         ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///                     },
    ///                 },
    ///             },
    ///         },
    ///         Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///         {
    ///             new CreateOrUpdateConditionRequestBody
    ///             {
    ///                 ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                 Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                 ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<ListAudienceCompaniesResponse> ListAudienceCompaniesAsync(
        AudienceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "audience/get-companies",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListAudienceCompaniesResponse>(responseBody)!;
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
    /// await client.Features.ListAudienceUsersAsync(
    ///     new AudienceRequestBody
    ///     {
    ///         ConditionGroups = new List&lt;CreateOrUpdateConditionGroupRequestBody&gt;()
    ///         {
    ///             new CreateOrUpdateConditionGroupRequestBody
    ///             {
    ///                 Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///                 {
    ///                     new CreateOrUpdateConditionRequestBody
    ///                     {
    ///                         ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                         Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                         ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///                     },
    ///                 },
    ///             },
    ///         },
    ///         Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///         {
    ///             new CreateOrUpdateConditionRequestBody
    ///             {
    ///                 ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                 Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                 ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<ListAudienceUsersResponse> ListAudienceUsersAsync(
        AudienceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "audience/get-users",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListAudienceUsersResponse>(responseBody)!;
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
    /// await client.Features.ListFeaturesAsync(new ListFeaturesRequest());
    /// </code>
    /// </example>
    public async Task<ListFeaturesResponse> ListFeaturesAsync(
        ListFeaturesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithoutCompanyOverrideFor != null)
        {
            _query["without_company_override_for"] = request.WithoutCompanyOverrideFor;
        }
        if (request.WithoutPlanEntitlementFor != null)
        {
            _query["without_plan_entitlement_for"] = request.WithoutPlanEntitlementFor;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.Value.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.Value.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "features",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListFeaturesResponse>(responseBody)!;
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
    /// await client.Features.CreateFeatureAsync(
    ///     new CreateFeatureRequestBody
    ///     {
    ///         Description = "description",
    ///         FeatureType = CreateFeatureRequestBodyFeatureType.Boolean,
    ///         Name = "name",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<CreateFeatureResponse> CreateFeatureAsync(
        CreateFeatureRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "features",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CreateFeatureResponse>(responseBody)!;
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
    /// await client.Features.GetFeatureAsync("feature_id");
    /// </code>
    /// </example>
    public async Task<GetFeatureResponse> GetFeatureAsync(
        string featureId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"features/{featureId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<GetFeatureResponse>(responseBody)!;
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
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ApiError>(responseBody));
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
    /// await client.Features.UpdateFeatureAsync("feature_id", new UpdateFeatureRequestBody());
    /// </code>
    /// </example>
    public async Task<UpdateFeatureResponse> UpdateFeatureAsync(
        string featureId,
        UpdateFeatureRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Put,
                Path = $"features/{featureId}",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpdateFeatureResponse>(responseBody)!;
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
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ApiError>(responseBody));
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
    /// await client.Features.DeleteFeatureAsync("feature_id");
    /// </code>
    /// </example>
    public async Task<DeleteFeatureResponse> DeleteFeatureAsync(
        string featureId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"features/{featureId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<DeleteFeatureResponse>(responseBody)!;
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
    /// await client.Features.CountFeaturesAsync(new CountFeaturesRequest());
    /// </code>
    /// </example>
    public async Task<CountFeaturesResponse> CountFeaturesAsync(
        CountFeaturesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithoutCompanyOverrideFor != null)
        {
            _query["without_company_override_for"] = request.WithoutCompanyOverrideFor;
        }
        if (request.WithoutPlanEntitlementFor != null)
        {
            _query["without_plan_entitlement_for"] = request.WithoutPlanEntitlementFor;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.Value.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.Value.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "features/count",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountFeaturesResponse>(responseBody)!;
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
    /// await client.Features.ListFlagsAsync(new ListFlagsRequest());
    /// </code>
    /// </example>
    public async Task<ListFlagsResponse> ListFlagsAsync(
        ListFlagsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
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
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "flags",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListFlagsResponse>(responseBody)!;
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
    /// await client.Features.CreateFlagAsync(
    ///     new CreateFlagRequestBody
    ///     {
    ///         DefaultValue = true,
    ///         Description = "description",
    ///         FlagType = "flag_type",
    ///         Key = "key",
    ///         Name = "name",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<CreateFlagResponse> CreateFlagAsync(
        CreateFlagRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "flags",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CreateFlagResponse>(responseBody)!;
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
    /// await client.Features.GetFlagAsync("flag_id");
    /// </code>
    /// </example>
    public async Task<GetFlagResponse> GetFlagAsync(
        string flagId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"flags/{flagId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<GetFlagResponse>(responseBody)!;
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
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ApiError>(responseBody));
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
    /// await client.Features.UpdateFlagAsync(
    ///     "flag_id",
    ///     new CreateFlagRequestBody
    ///     {
    ///         DefaultValue = true,
    ///         Description = "description",
    ///         FlagType = "flag_type",
    ///         Key = "key",
    ///         Name = "name",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<UpdateFlagResponse> UpdateFlagAsync(
        string flagId,
        CreateFlagRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Put,
                Path = $"flags/{flagId}",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpdateFlagResponse>(responseBody)!;
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
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ApiError>(responseBody));
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
    /// await client.Features.DeleteFlagAsync("flag_id");
    /// </code>
    /// </example>
    public async Task<DeleteFlagResponse> DeleteFlagAsync(
        string flagId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"flags/{flagId}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<DeleteFlagResponse>(responseBody)!;
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
    /// await client.Features.UpdateFlagRulesAsync(
    ///     "flag_id",
    ///     new UpdateFlagRulesRequestBody
    ///     {
    ///         Rules = new List&lt;CreateOrUpdateRuleRequestBody&gt;()
    ///         {
    ///             new CreateOrUpdateRuleRequestBody
    ///             {
    ///                 ConditionGroups = new List&lt;CreateOrUpdateConditionGroupRequestBody&gt;()
    ///                 {
    ///                     new CreateOrUpdateConditionGroupRequestBody
    ///                     {
    ///                         Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///                         {
    ///                             new CreateOrUpdateConditionRequestBody
    ///                             {
    ///                                 ConditionType =
    ///                                     CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                                 Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                                 ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///                             },
    ///                         },
    ///                     },
    ///                 },
    ///                 Conditions = new List&lt;CreateOrUpdateConditionRequestBody&gt;()
    ///                 {
    ///                     new CreateOrUpdateConditionRequestBody
    ///                     {
    ///                         ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
    ///                         Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
    ///                         ResourceIds = new List&lt;string&gt;() { "resource_ids" },
    ///                     },
    ///                 },
    ///                 Name = "name",
    ///                 Priority = 1,
    ///                 Value = true,
    ///             },
    ///         },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<UpdateFlagRulesResponse> UpdateFlagRulesAsync(
        string flagId,
        UpdateFlagRulesRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Put,
                Path = $"flags/{flagId}/rules",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpdateFlagRulesResponse>(responseBody)!;
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
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ApiError>(responseBody));
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
    /// await client.Features.CheckFlagAsync("key", new CheckFlagRequestBody());
    /// </code>
    /// </example>
    public async Task<CheckFlagResponse> CheckFlagAsync(
        string key,
        CheckFlagRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = $"flags/{key}/check",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CheckFlagResponse>(responseBody)!;
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
    /// await client.Features.CheckFlagsAsync(new CheckFlagRequestBody());
    /// </code>
    /// </example>
    public async Task<CheckFlagsResponse> CheckFlagsAsync(
        CheckFlagRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "flags/check",
                Body = request,
                ContentType = "application/json",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CheckFlagsResponse>(responseBody)!;
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
    /// await client.Features.CountFlagsAsync(new CountFlagsRequest());
    /// </code>
    /// </example>
    public async Task<CountFlagsResponse> CountFlagsAsync(
        CountFlagsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["ids"] = request.Ids;
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
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
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "flags/count",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountFlagsResponse>(responseBody)!;
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
