using System.Net.Http;
using System.Text.Json;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public class CompaniesClient
{
    private RawClient _client;

    public CompaniesClient(RawClient client)
    {
        _client = client;
    }

    public async Task<ListCompaniesResponse> ListCompaniesAsync(ListCompaniesRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.PlanId != null)
        {
            _query["plan_id"] = request.PlanId;
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithoutFeatureOverrideFor != null)
        {
            _query["without_feature_override_for"] = request.WithoutFeatureOverrideFor;
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
                Path = "companies",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListCompaniesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertCompanyResponse> UpsertCompanyAsync(UpsertCompanyRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "companies",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertCompanyResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetCompanyResponse> GetCompanyAsync(string companyId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"companies/{companyId}"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetCompanyResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<DeleteCompanyResponse> DeleteCompanyAsync(string companyId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"companies/{companyId}"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<DeleteCompanyResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CountCompaniesResponse> CountCompaniesAsync(CountCompaniesRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.PlanId != null)
        {
            _query["plan_id"] = request.PlanId;
        }
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithoutFeatureOverrideFor != null)
        {
            _query["without_feature_override_for"] = request.WithoutFeatureOverrideFor;
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
                Path = "companies/count",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountCompaniesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CreateCompanyResponse> CreateCompanyAsync(UpsertCompanyRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "companies/create",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CreateCompanyResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<DeleteCompanyByKeysResponse> DeleteCompanyByKeysAsync(KeysRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "companies/delete",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<DeleteCompanyByKeysResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<LookupCompanyResponse> LookupCompanyAsync(LookupCompanyRequest request)
    {
        var _query = new Dictionary<string, object>() { { "keys", request.Keys.ToString() }, };
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = "companies/lookup",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<LookupCompanyResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetActiveDealsResponse> GetActiveDealsAsync(GetActiveDealsRequest request)
    {
        var _query = new Dictionary<string, object>()
        {
            { "company_id", request.CompanyId },
            { "deal_stage", request.DealStage },
        };
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
                Path = "company-crm-deals",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetActiveDealsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListCompanyMembershipsResponse> ListCompanyMembershipsAsync(
        ListCompanyMembershipsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.UserId != null)
        {
            _query["user_id"] = request.UserId;
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
                Path = "company-memberships",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListCompanyMembershipsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetOrCreateCompanyMembershipResponse> GetOrCreateCompanyMembershipAsync(
        GetOrCreateCompanyMembershipRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "company-memberships",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetOrCreateCompanyMembershipResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<DeleteCompanyMembershipResponse> DeleteCompanyMembershipAsync(
        string companyMembershipId
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"company-memberships/{companyMembershipId}"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<DeleteCompanyMembershipResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetActiveCompanySubscriptionResponse> GetActiveCompanySubscriptionAsync(
        GetActiveCompanySubscriptionRequest request
    )
    {
        var _query = new Dictionary<string, object>() { { "company_id", request.CompanyId }, };
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
                Path = "company-subscriptions",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetActiveCompanySubscriptionResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertCompanyTraitResponse> UpsertCompanyTraitAsync(
        UpsertTraitRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "company-traits",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertCompanyTraitResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListEntityKeyDefinitionsResponse> ListEntityKeyDefinitionsAsync(
        ListEntityKeyDefinitionsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.EntityType != null)
        {
            _query["entity_type"] = JsonSerializer.Serialize(request.EntityType.Value);
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
                Path = "entity-key-definitions",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListEntityKeyDefinitionsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CountEntityKeyDefinitionsResponse> CountEntityKeyDefinitionsAsync(
        CountEntityKeyDefinitionsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.EntityType != null)
        {
            _query["entity_type"] = JsonSerializer.Serialize(request.EntityType.Value);
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
                Path = "entity-key-definitions/count",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountEntityKeyDefinitionsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListEntityTraitDefinitionsResponse> ListEntityTraitDefinitionsAsync(
        ListEntityTraitDefinitionsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.EntityType != null)
        {
            _query["entity_type"] = JsonSerializer.Serialize(request.EntityType.Value);
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.TraitType != null)
        {
            _query["trait_type"] = JsonSerializer.Serialize(request.TraitType.Value);
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
                Path = "entity-trait-definitions",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListEntityTraitDefinitionsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetOrCreateEntityTraitDefinitionResponse> GetOrCreateEntityTraitDefinitionAsync(
        CreateEntityTraitDefinitionRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "entity-trait-definitions",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetOrCreateEntityTraitDefinitionResponse>(
                responseBody
            )!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetEntityTraitDefinitionResponse> GetEntityTraitDefinitionAsync(
        string entityTraitDefinitionId
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"entity-trait-definitions/{entityTraitDefinitionId}"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetEntityTraitDefinitionResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdateEntityTraitDefinitionResponse> UpdateEntityTraitDefinitionAsync(
        string entityTraitDefinitionId,
        UpdateEntityTraitDefinitionRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"entity-trait-definitions/{entityTraitDefinitionId}",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpdateEntityTraitDefinitionResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CountEntityTraitDefinitionsResponse> CountEntityTraitDefinitionsAsync(
        CountEntityTraitDefinitionsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.EntityType != null)
        {
            _query["entity_type"] = JsonSerializer.Serialize(request.EntityType.Value);
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.TraitType != null)
        {
            _query["trait_type"] = JsonSerializer.Serialize(request.TraitType.Value);
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
                Path = "entity-trait-definitions/count",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountEntityTraitDefinitionsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetEntityTraitValuesResponse> GetEntityTraitValuesAsync(
        GetEntityTraitValuesRequest request
    )
    {
        var _query = new Dictionary<string, object>()
        {
            { "definition_id", request.DefinitionId },
        };
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
                Path = "entity-trait-values",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetEntityTraitValuesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertUserTraitResponse> UpsertUserTraitAsync(UpsertTraitRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "user-traits",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertUserTraitResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListUsersResponse> ListUsersAsync(ListUsersRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
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
                Method = HttpMethod.Get,
                Path = "users",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListUsersResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertUserResponse> UpsertUserAsync(UpsertUserRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "users",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertUserResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetUserResponse> GetUserAsync(string userId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = $"users/{userId}" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetUserResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<DeleteUserResponse> DeleteUserAsync(string userId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Delete, Path = $"users/{userId}" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<DeleteUserResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CountUsersResponse> CountUsersAsync(CountUsersRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
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
                Method = HttpMethod.Get,
                Path = "users/count",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountUsersResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CreateUserResponse> CreateUserAsync(UpsertUserRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "users/create",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CreateUserResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<DeleteUserByKeysResponse> DeleteUserByKeysAsync(KeysRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "users/delete",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<DeleteUserByKeysResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<LookupUserResponse> LookupUserAsync(LookupUserRequest request)
    {
        var _query = new Dictionary<string, object>() { { "keys", request.Keys.ToString() }, };
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = "users/lookup",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<LookupUserResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
