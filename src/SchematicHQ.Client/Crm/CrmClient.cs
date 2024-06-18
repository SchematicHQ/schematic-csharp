using System.Text.Json;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CrmClient
{
    private RawClient _client;

    public CrmClient(RawClient client)
    {
        _client = client;
    }

    public async Task<UpsertDealLineItemAssociationResponse> UpsertDealLineItemAssociationAsync(
        CreateCrmDealLineItemAssociationRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/crm/associations/deal-line-item",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpsertDealLineItemAssociationResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertLineItemResponse> UpsertLineItemAsync(
        CreateCrmLineItemRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/crm/deal-line-item/upsert",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpsertLineItemResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertCrmDealResponse> UpsertCrmDealAsync(CreateCrmDealRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/crm/deals/upsert",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpsertCrmDealResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<ListCrmProductsResponse> ListCrmProductsAsync(ListCrmProductsRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.Name != null)
        {
            _query["name"] = request.Name;
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
                Path = "/crm/products",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListCrmProductsResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertCrmProductResponse> UpsertCrmProductAsync(
        CreateCrmProductRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/crm/products/upsert",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpsertCrmProductResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }
}
