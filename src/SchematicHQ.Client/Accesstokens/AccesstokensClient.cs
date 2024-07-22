using System.Net.Http;
using System.Text.Json;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public class AccesstokensClient
{
    private RawClient _client;

    public AccesstokensClient(RawClient client)
    {
        _client = client;
    }

    public async Task<IssueTemporaryAccessTokenResponse> IssueTemporaryAccessTokenAsync(
        IssueTemporaryAccessTokenRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "temporary-access-tokens",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<IssueTemporaryAccessTokenResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
