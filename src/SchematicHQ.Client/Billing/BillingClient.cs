using System.Net.Http;
using System.Text.Json;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public class BillingClient
{
    private RawClient _client;

    public BillingClient(RawClient client)
    {
        _client = client;
    }

    public async Task<UpsertBillingCustomerResponse> UpsertBillingCustomerAsync(
        CreateBillingCustomerRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "billing/customer/upsert",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertBillingCustomerResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListCustomersResponse> ListCustomersAsync(ListCustomersRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Name != null)
        {
            _query["name"] = request.Name;
        }
        if (request.FailedToImport != null)
        {
            _query["failed_to_import"] = request.FailedToImport.ToString();
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
                Path = "billing/customers",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListCustomersResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CountCustomersResponse> CountCustomersAsync(CountCustomersRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Name != null)
        {
            _query["name"] = request.Name;
        }
        if (request.FailedToImport != null)
        {
            _query["failed_to_import"] = request.FailedToImport.ToString();
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
                Path = "billing/customers/count",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountCustomersResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<GetBillingPlanGroupByAccountIdResponse> GetBillingPlanGroupByAccountIdAsync()
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = "billing/plan-group" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetBillingPlanGroupByAccountIdResponse>(
                responseBody
            )!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CreateBillingPlanGroupResponse> CreateBillingPlanGroupAsync(
        CreateBillingPlanGroupRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "billing/plan-group",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CreateBillingPlanGroupResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdateBillingPlanGroupResponse> UpdateBillingPlanGroupAsync(
        string billingId,
        UpdateBillingPlanGroupRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"billing/plan-group/{billingId}",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpdateBillingPlanGroupResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertBillingPriceResponse> UpsertBillingPriceAsync(
        CreateBillingPriceRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "billing/price/upsert",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertBillingPriceResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListProductPricesResponse> ListProductPricesAsync(
        ListProductPricesRequest request
    )
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
                Path = "billing/product/prices",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListProductPricesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertBillingProductResponse> UpsertBillingProductAsync(
        CreateBillingProductRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "billing/product/upsert",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertBillingProductResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListBillingProductsResponse> ListBillingProductsAsync(
        ListBillingProductsRequest request
    )
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
                Path = "billing/products",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListBillingProductsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertBillingSubscriptionResponse> UpsertBillingSubscriptionAsync(
        CreateBillingSubscriptionsRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "billing/subscription/upsert",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertBillingSubscriptionResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
