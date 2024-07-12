using System.Net.Http;
using System.Text.Json;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public class FeaturesClient
{
    private RawClient _client;

    public FeaturesClient(RawClient client)
    {
        _client = client;
    }

    public async Task<CountAudienceCompaniesResponse> CountAudienceCompaniesAsync(
        AudienceRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "audience/count-companies",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountAudienceCompaniesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CountAudienceUsersResponse> CountAudienceUsersAsync(
        AudienceRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "audience/count-users",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountAudienceUsersResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListAudienceCompaniesResponse> ListAudienceCompaniesAsync(
        AudienceRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "audience/get-companies",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListAudienceCompaniesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListAudienceUsersResponse> ListAudienceUsersAsync(AudienceRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "audience/get-users",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListAudienceUsersResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListFeaturesResponse> ListFeaturesAsync(ListFeaturesRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
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
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = "features",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListFeaturesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CreateFeatureResponse> CreateFeatureAsync(CreateFeatureRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "features",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CreateFeatureResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetFeatureResponse> GetFeatureAsync(string featureId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = $"features/{featureId}" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetFeatureResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdateFeatureResponse> UpdateFeatureAsync(
        string featureId,
        UpdateFeatureRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"features/{featureId}",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpdateFeatureResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<DeleteFeatureResponse> DeleteFeatureAsync(string featureId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"features/{featureId}"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<DeleteFeatureResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CountFeaturesResponse> CountFeaturesAsync(CountFeaturesRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
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
            _query["limit"] = request.Limit.ToString();
        }
        if (request.Offset != null)
        {
            _query["offset"] = request.Offset.ToString();
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = "features/count",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountFeaturesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListFlagChecksResponse> ListFlagChecksAsync(ListFlagChecksRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.FlagId != null)
        {
            _query["flag_id"] = request.FlagId;
        }
        if (request.FlagIds != null)
        {
            _query["flag_ids"] = request.FlagIds;
        }
        if (request.Id != null)
        {
            _query["id"] = request.Id;
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
                Method = HttpMethod.Get,
                Path = "flag-checks",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListFlagChecksResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetFlagCheckResponse> GetFlagCheckAsync(string flagCheckId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"flag-checks/{flagCheckId}"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetFlagCheckResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CountFlagChecksResponse> CountFlagChecksAsync(CountFlagChecksRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.FlagId != null)
        {
            _query["flag_id"] = request.FlagId;
        }
        if (request.FlagIds != null)
        {
            _query["flag_ids"] = request.FlagIds;
        }
        if (request.Id != null)
        {
            _query["id"] = request.Id;
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
                Method = HttpMethod.Get,
                Path = "flag-checks/count",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountFlagChecksResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetLatestFlagChecksResponse> GetLatestFlagChecksAsync(
        GetLatestFlagChecksRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.FlagId != null)
        {
            _query["flag_id"] = request.FlagId;
        }
        if (request.FlagIds != null)
        {
            _query["flag_ids"] = request.FlagIds;
        }
        if (request.Id != null)
        {
            _query["id"] = request.Id;
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
                Method = HttpMethod.Get,
                Path = "flag-checks/latest",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetLatestFlagChecksResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListFlagsResponse> ListFlagsAsync(ListFlagsRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
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
                Method = HttpMethod.Get,
                Path = "flags",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListFlagsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CreateFlagResponse> CreateFlagAsync(CreateFlagRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "flags",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CreateFlagResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetFlagResponse> GetFlagAsync(string flagId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = $"flags/{flagId}" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetFlagResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdateFlagResponse> UpdateFlagAsync(
        string flagId,
        CreateFlagRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"flags/{flagId}",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpdateFlagResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<DeleteFlagResponse> DeleteFlagAsync(string flagId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Delete, Path = $"flags/{flagId}" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<DeleteFlagResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdateFlagRulesResponse> UpdateFlagRulesAsync(
        string flagId,
        UpdateFlagRulesRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"flags/{flagId}/rules",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpdateFlagRulesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CheckFlagResponse> CheckFlagAsync(string key, CheckFlagRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = $"flags/{key}/check",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CheckFlagResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CheckFlagsResponse> CheckFlagsAsync(CheckFlagRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "flags/check",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CheckFlagsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CountFlagsResponse> CountFlagsAsync(CountFlagsRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.FeatureId != null)
        {
            _query["feature_id"] = request.FeatureId;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
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
                Method = HttpMethod.Get,
                Path = "flags/count",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountFlagsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
