using System.Text.Json;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class EntitlementsClient
{
    private RawClient _client;

    public EntitlementsClient(RawClient client)
    {
        _client = client;
    }

    public async Task<ListCompanyOverridesResponse> ListCompanyOverridesAsync(
        ListCompanyOverridesRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.CompanyIds != null)
        {
            _query["company_ids"] = request.CompanyIds;
        }
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
        }
        if (request.FeatureIds != null)
        {
            _query["feature_ids"] = request.FeatureIds;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/company-overrides",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListCompanyOverridesResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CreateCompanyOverrideResponse> CreateCompanyOverrideAsync(
        CreateCompanyOverrideRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/company-overrides",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CreateCompanyOverrideResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<GetCompanyOverrideResponse> GetCompanyOverrideAsync(string companyOverrideId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/company-overrides/{companyOverrideId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetCompanyOverrideResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdateCompanyOverrideResponse> UpdateCompanyOverrideAsync(
        string companyOverrideId,
        UpdateCompanyOverrideRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"/company-overrides/{companyOverrideId}",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpdateCompanyOverrideResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<DeleteCompanyOverrideResponse> DeleteCompanyOverrideAsync(
        string companyOverrideId
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"/company-overrides/{companyOverrideId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<DeleteCompanyOverrideResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CountCompanyOverridesResponse> CountCompanyOverridesAsync(
        CountCompanyOverridesRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.CompanyIds != null)
        {
            _query["company_ids"] = request.CompanyIds;
        }
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
        }
        if (request.FeatureIds != null)
        {
            _query["feature_ids"] = request.FeatureIds;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/company-overrides/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountCompanyOverridesResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<ListFeatureCompaniesResponse> ListFeatureCompaniesAsync(
        ListFeatureCompaniesRequest request
    )
    {
        var _query = new Dictionary<string, object>() { { "feature_id", request.FeatureId }, };
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/feature-companies",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListFeatureCompaniesResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CountFeatureCompaniesResponse> CountFeatureCompaniesAsync(
        CountFeatureCompaniesRequest request
    )
    {
        var _query = new Dictionary<string, object>() { { "feature_id", request.FeatureId }, };
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/feature-companies/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountFeatureCompaniesResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<ListFeatureUsageResponse> ListFeatureUsageAsync(
        ListFeatureUsageRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.CompanyKeys != null)
        {
            _query["company_keys"] = request.CompanyKeys;
        }
        if (request.FeatureIds != null)
        {
            _query["feature_ids"] = request.FeatureIds;
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/feature-usage",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListFeatureUsageResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CountFeatureUsageResponse> CountFeatureUsageAsync(
        CountFeatureUsageRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.CompanyKeys != null)
        {
            _query["company_keys"] = request.CompanyKeys;
        }
        if (request.FeatureIds != null)
        {
            _query["feature_ids"] = request.FeatureIds;
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/feature-usage/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountFeatureUsageResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<ListFeatureUsersResponse> ListFeatureUsersAsync(
        ListFeatureUsersRequest request
    )
    {
        var _query = new Dictionary<string, object>() { { "feature_id", request.FeatureId }, };
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/feature-users",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListFeatureUsersResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CountFeatureUsersResponse> CountFeatureUsersAsync(
        CountFeatureUsersRequest request
    )
    {
        var _query = new Dictionary<string, object>() { { "feature_id", request.FeatureId }, };
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/feature-users/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountFeatureUsersResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<ListPlanEntitlementsResponse> ListPlanEntitlementsAsync(
        ListPlanEntitlementsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
        }
        if (request.FeatureIds != null)
        {
            _query["feature_ids"] = request.FeatureIds;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.PlanId != null)
        {
            _query["plan_id"] = request.PlanId;
        }
        if (request.PlanIds != null)
        {
            _query["plan_ids"] = request.PlanIds;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/plan-entitlements",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListPlanEntitlementsResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CreatePlanEntitlementResponse> CreatePlanEntitlementAsync(
        CreatePlanEntitlementRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/plan-entitlements",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CreatePlanEntitlementResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<GetPlanEntitlementResponse> GetPlanEntitlementAsync(string planEntitlementId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/plan-entitlements/{planEntitlementId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetPlanEntitlementResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdatePlanEntitlementResponse> UpdatePlanEntitlementAsync(
        string planEntitlementId,
        UpdatePlanEntitlementRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"/plan-entitlements/{planEntitlementId}",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpdatePlanEntitlementResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<DeletePlanEntitlementResponse> DeletePlanEntitlementAsync(
        string planEntitlementId
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"/plan-entitlements/{planEntitlementId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<DeletePlanEntitlementResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<CountPlanEntitlementsResponse> CountPlanEntitlementsAsync(
        CountPlanEntitlementsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
        }
        if (request.FeatureIds != null)
        {
            _query["feature_ids"] = request.FeatureIds;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.PlanId != null)
        {
            _query["plan_id"] = request.PlanId;
        }
        if (request.PlanIds != null)
        {
            _query["plan_ids"] = request.PlanIds;
        }
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit;
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/plan-entitlements/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountPlanEntitlementsResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<GetFeatureUsageByCompanyResponse> GetFeatureUsageByCompanyAsync(
        GetFeatureUsageByCompanyRequest request
    )
    {
        var _query = new Dictionary<string, object>() { { "keys", request.Keys.ToString() }, };
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = "/usage-by-company",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetFeatureUsageByCompanyResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }
}
