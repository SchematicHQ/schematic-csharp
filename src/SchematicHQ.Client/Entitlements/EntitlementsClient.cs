using System.Net.Http;
using System.Text.Json;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public partial class EntitlementsClient
{
    private RawClient _client;

    internal EntitlementsClient(RawClient client)
    {
        _client = client;
    }

    public async Task<ListCompanyOverridesResponse> ListCompanyOverridesAsync(
        ListCompanyOverridesRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["company_ids"] = request.CompanyIds;
        _query["feature_ids"] = request.FeatureIds;
        _query["ids"] = request.Ids;
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
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
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "company-overrides",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListCompanyOverridesResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<CreateCompanyOverrideResponse> CreateCompanyOverrideAsync(
        CreateCompanyOverrideRequestBody request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "company-overrides",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CreateCompanyOverrideResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<GetCompanyOverrideResponse> GetCompanyOverrideAsync(
        string companyOverrideId,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"company-overrides/{companyOverrideId}",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<GetCompanyOverrideResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<UpdateCompanyOverrideResponse> UpdateCompanyOverrideAsync(
        string companyOverrideId,
        UpdateCompanyOverrideRequestBody request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Put,
                Path = $"company-overrides/{companyOverrideId}",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpdateCompanyOverrideResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<DeleteCompanyOverrideResponse> DeleteCompanyOverrideAsync(
        string companyOverrideId,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"company-overrides/{companyOverrideId}",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<DeleteCompanyOverrideResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<CountCompanyOverridesResponse> CountCompanyOverridesAsync(
        CountCompanyOverridesRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["company_ids"] = request.CompanyIds;
        _query["feature_ids"] = request.FeatureIds;
        _query["ids"] = request.Ids;
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
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
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "company-overrides/count",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountCompanyOverridesResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<ListFeatureCompaniesResponse> ListFeatureCompaniesAsync(
        ListFeatureCompaniesRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["feature_id"] = request.FeatureId;
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "feature-companies",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListFeatureCompaniesResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<CountFeatureCompaniesResponse> CountFeatureCompaniesAsync(
        CountFeatureCompaniesRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["feature_id"] = request.FeatureId;
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "feature-companies/count",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountFeatureCompaniesResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<ListFeatureUsageResponse> ListFeatureUsageAsync(
        ListFeatureUsageRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["feature_ids"] = request.FeatureIds;
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.CompanyKeys != null)
        {
            _query["company_keys"] = request.CompanyKeys.ToString();
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithoutNegativeEntitlements != null)
        {
            _query["without_negative_entitlements"] =
                request.WithoutNegativeEntitlements.ToString();
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "feature-usage",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListFeatureUsageResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<CountFeatureUsageResponse> CountFeatureUsageAsync(
        CountFeatureUsageRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["feature_ids"] = request.FeatureIds;
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.CompanyKeys != null)
        {
            _query["company_keys"] = request.CompanyKeys.ToString();
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithoutNegativeEntitlements != null)
        {
            _query["without_negative_entitlements"] =
                request.WithoutNegativeEntitlements.ToString();
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "feature-usage/count",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountFeatureUsageResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<ListFeatureUsersResponse> ListFeatureUsersAsync(
        ListFeatureUsersRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["feature_id"] = request.FeatureId;
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "feature-users",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListFeatureUsersResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<CountFeatureUsersResponse> CountFeatureUsersAsync(
        CountFeatureUsersRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["feature_id"] = request.FeatureId;
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "feature-users/count",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountFeatureUsersResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<ListPlanEntitlementsResponse> ListPlanEntitlementsAsync(
        ListPlanEntitlementsRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["feature_ids"] = request.FeatureIds;
        _query["ids"] = request.Ids;
        _query["plan_ids"] = request.PlanIds;
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
        }
        if (request.PlanId != null)
        {
            _query["plan_id"] = request.PlanId;
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "plan-entitlements",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListPlanEntitlementsResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<CreatePlanEntitlementResponse> CreatePlanEntitlementAsync(
        CreatePlanEntitlementRequestBody request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "plan-entitlements",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CreatePlanEntitlementResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<GetPlanEntitlementResponse> GetPlanEntitlementAsync(
        string planEntitlementId,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"plan-entitlements/{planEntitlementId}",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<GetPlanEntitlementResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<UpdatePlanEntitlementResponse> UpdatePlanEntitlementAsync(
        string planEntitlementId,
        UpdatePlanEntitlementRequestBody request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Put,
                Path = $"plan-entitlements/{planEntitlementId}",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<UpdatePlanEntitlementResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<DeletePlanEntitlementResponse> DeletePlanEntitlementAsync(
        string planEntitlementId,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"plan-entitlements/{planEntitlementId}",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<DeletePlanEntitlementResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<CountPlanEntitlementsResponse> CountPlanEntitlementsAsync(
        CountPlanEntitlementsRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["feature_ids"] = request.FeatureIds;
        _query["ids"] = request.Ids;
        _query["plan_ids"] = request.PlanIds;
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
        }
        if (request.PlanId != null)
        {
            _query["plan_id"] = request.PlanId;
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "plan-entitlements/count",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CountPlanEntitlementsResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    public async Task<GetFeatureUsageByCompanyResponse> GetFeatureUsageByCompanyAsync(
        GetFeatureUsageByCompanyRequest request,
        RequestOptions? options = null
    )
    {
        var _query = new Dictionary<string, object>() { };
        _query["keys"] = request.Keys.ToString();
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "usage-by-company",
                Query = _query,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<GetFeatureUsageByCompanyResponse>(responseBody)!;
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }
}
