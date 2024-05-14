using System.Text.Json;
using Schematic.Client;

namespace Schematic.Client;

public class PlansClient
{
    private RawClient _client;

    public PlansClient(RawClient client)
    {
        _client = client;
    }

    public async Task<GetAudienceResponse> GetAudienceAsync(string planAudienceId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/plan-audiences/{planAudienceId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetAudienceResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<UpdateAudienceResponse> UpdateAudienceAsync(
        string planAudienceId,
        UpdateAudienceRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"/plan-audiences/{planAudienceId}",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpdateAudienceResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<DeleteAudienceResponse> DeleteAudienceAsync(string planAudienceId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"/plan-audiences/{planAudienceId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<DeleteAudienceResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<ListPlansResponse> ListPlansAsync(ListPlansRequest request)
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
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithoutEntitlementFor != null)
        {
            _query["without_entitlement_for"] = request.WithoutEntitlementFor;
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
                Path = "/plans",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListPlansResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<CreatePlanResponse> CreatePlanAsync(CreatePlanRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/plans",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CreatePlanResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<GetPlanResponse> GetPlanAsync(string planId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest { Method = HttpMethod.Get, Path = $"/plans/{planId}" }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetPlanResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<UpdatePlanResponse> UpdatePlanAsync(
        string planId,
        UpdatePlanRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"/plans/{planId}",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpdatePlanResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<DeletePlanResponse> DeletePlanAsync(string planId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest { Method = HttpMethod.Delete, Path = $"/plans/{planId}" }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<DeletePlanResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<CountPlansResponse> CountPlansAsync(CountPlansRequest request)
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
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.WithoutEntitlementFor != null)
        {
            _query["without_entitlement_for"] = request.WithoutEntitlementFor;
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
                Path = "/plans/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountPlansResponse>(responseBody);
        }
        throw new Exception();
    }
}
