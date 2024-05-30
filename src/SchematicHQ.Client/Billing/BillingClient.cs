using System.Text.Json;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class BillingClient
{
    private RawClient _client;

    public BillingClient(RawClient client)
    {
        _client = client;
    }

    public async Task<UpsertBillingProductResponse> UpsertBillingProductAsync(
        CreateBillingProductRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/billing/product/upsert",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpsertBillingProductResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<ListProductsResponse> ListProductsAsync(ListProductsRequest request)
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
                Path = "/billing/products",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListProductsResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertBillingSubscriptionResponse> UpsertBillingSubscriptionAsync(
        CreateBillingSubscriptionsRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/billing/subscription/upsert",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpsertBillingSubscriptionResponse>(responseBody);
        }
        throw new Exception(responseBody);
    }
}
