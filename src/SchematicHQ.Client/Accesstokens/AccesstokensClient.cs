using System.Net.Http;
using System.Text.Json;
using System.Threading;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class AccesstokensClient
{
    private RawClient _client;

    internal AccesstokensClient(RawClient client)
    {
        _client = client;
    }

    /// <example>
    /// <code>
    /// await client.Accesstokens.IssueTemporaryAccessTokenAsync(
    ///     new IssueTemporaryAccessTokenRequestBody
    ///     {
    ///         Lookup = new Dictionary&lt;string, string&gt;() { { "key", "value" } },
    ///         ResourceType = "resource_type",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<IssueTemporaryAccessTokenResponse> IssueTemporaryAccessTokenAsync(
        IssueTemporaryAccessTokenRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .MakeRequestAsync(
                new RawClient.JsonApiRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "temporary-access-tokens",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<IssueTemporaryAccessTokenResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicApiException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ApiError>(responseBody));
                case 500:
                    throw new InternalServerError(JsonUtils.Deserialize<ApiError>(responseBody));
            }
        }
        catch (JsonException)
        {
            // unable to map error response, throwing generic error
        }
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }
}
