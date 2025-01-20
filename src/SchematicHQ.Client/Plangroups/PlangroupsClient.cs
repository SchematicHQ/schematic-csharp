using System.Net.Http;
using System.Text.Json;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public class PlangroupsClient
{
    private RawClient _client;

    public PlangroupsClient(RawClient client)
    {
        _client = client;
    }

    public async Task<GetPlanGroupResponse> GetPlanGroupAsync()
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = "plan-groups" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetPlanGroupResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<CreatePlanGroupResponse> CreatePlanGroupAsync(
        CreatePlanGroupRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "plan-groups",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CreatePlanGroupResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    public async Task<UpdatePlanGroupResponse> UpdatePlanGroupAsync(
        string planGroupId,
        UpdatePlanGroupRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"plan-groups/{planGroupId}",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpdatePlanGroupResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
