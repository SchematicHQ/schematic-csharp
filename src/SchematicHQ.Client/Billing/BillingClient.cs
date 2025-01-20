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

    public async Task<UpsertBillingCouponResponse> UpsertBillingCouponAsync(
        CreateCouponRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "billing/coupons",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertBillingCouponResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
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

    public async Task<ListInvoicesResponse> ListInvoicesAsync(ListInvoicesRequest request)
    {
        var _query = new Dictionary<string, object>()
        {
            { "customer_external_id", request.CustomerExternalId },
        };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.SubscriptionExternalId != null)
        {
            _query["subscription_external_id"] = request.SubscriptionExternalId;
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
                Path = "billing/invoices",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListInvoicesResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertInvoiceResponse> UpsertInvoiceAsync(CreateInvoiceRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "billing/invoices",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertInvoiceResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListMetersResponse> ListMetersAsync(ListMetersRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.DisplayName != null)
        {
            _query["display_name"] = request.DisplayName;
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
                Path = "billing/meter",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListMetersResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertBillingMeterResponse> UpsertBillingMeterAsync(
        CreateMeterRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "billing/meter/upsert",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertBillingMeterResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<ListPaymentMethodsResponse> ListPaymentMethodsAsync(
        ListPaymentMethodsRequest request
    )
    {
        var _query = new Dictionary<string, object>()
        {
            { "customer_external_id", request.CustomerExternalId },
        };
        if (request.CompanyId != null)
        {
            _query["company_id"] = request.CompanyId;
        }
        if (request.SubscriptionExternalId != null)
        {
            _query["subscription_external_id"] = request.SubscriptionExternalId;
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
                Path = "billing/payment-methods",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<ListPaymentMethodsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpsertPaymentMethodResponse> UpsertPaymentMethodAsync(
        CreatePaymentMethodRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "billing/payment-methods",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpsertPaymentMethodResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<SearchBillingPricesResponse> SearchBillingPricesAsync(
        SearchBillingPricesRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
        }
        if (request.Interval != null)
        {
            _query["interval"] = request.Interval;
        }
        if (request.UsageType != null)
        {
            _query["usage_type"] = request.UsageType;
        }
        if (request.Price != null)
        {
            _query["price"] = request.Price.ToString();
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
                Path = "billing/price",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<SearchBillingPricesResponse>(responseBody)!;
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
        if (request.PriceUsageType != null)
        {
            _query["price_usage_type"] = request.PriceUsageType;
        }
        if (request.WithoutLinkedToPlan != null)
        {
            _query["without_linked_to_plan"] = request.WithoutLinkedToPlan.ToString();
        }
        if (request.WithZeroPrice != null)
        {
            _query["with_zero_price"] = request.WithZeroPrice.ToString();
        }
        if (request.WithPricesOnly != null)
        {
            _query["with_prices_only"] = request.WithPricesOnly.ToString();
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

    public async Task<DeleteProductPriceResponse> DeleteProductPriceAsync(string billingId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"billing/product/prices/{billingId}"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<DeleteProductPriceResponse>(responseBody)!;
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
        if (request.PriceUsageType != null)
        {
            _query["price_usage_type"] = request.PriceUsageType;
        }
        if (request.WithoutLinkedToPlan != null)
        {
            _query["without_linked_to_plan"] = request.WithoutLinkedToPlan.ToString();
        }
        if (request.WithZeroPrice != null)
        {
            _query["with_zero_price"] = request.WithZeroPrice.ToString();
        }
        if (request.WithPricesOnly != null)
        {
            _query["with_prices_only"] = request.WithPricesOnly.ToString();
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

    public async Task<CountBillingProductsResponse> CountBillingProductsAsync(
        CountBillingProductsRequest request
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
        if (request.PriceUsageType != null)
        {
            _query["price_usage_type"] = request.PriceUsageType;
        }
        if (request.WithoutLinkedToPlan != null)
        {
            _query["without_linked_to_plan"] = request.WithoutLinkedToPlan.ToString();
        }
        if (request.WithZeroPrice != null)
        {
            _query["with_zero_price"] = request.WithZeroPrice.ToString();
        }
        if (request.WithPricesOnly != null)
        {
            _query["with_prices_only"] = request.WithPricesOnly.ToString();
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
                Path = "billing/products/count",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CountBillingProductsResponse>(responseBody)!;
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
