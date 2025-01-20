using System.Net.Http;
using System.Text.Json;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public class CheckoutClient
{
    private RawClient _client;

    public CheckoutClient(RawClient client)
    {
        _client = client;
    }

    public async Task<CheckoutInternalResponse> InternalAsync(
        ChangeSubscriptionInternalRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "checkout-internal",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CheckoutInternalResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<PreviewCheckoutInternalResponse> PreviewCheckoutInternalAsync(
        ChangeSubscriptionInternalRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "checkout-internal/preview",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<PreviewCheckoutInternalResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdateCustomerSubscriptionTrialEndResponse> UpdateCustomerSubscriptionTrialEndAsync(
        string subscriptionId,
        UpdateTrialEndRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"subscription/{subscriptionId}/edit-trial-end",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpdateCustomerSubscriptionTrialEndResponse>(
                responseBody
            )!;
        }
        throw new Exception(responseBody);
    }
}
