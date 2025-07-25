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

    /// <example><code>
    /// await client.Accesstokens.IssueTemporaryAccessTokenAsync(
    ///     new IssueTemporaryAccessTokenRequestBody
    ///     {
    ///         Lookup = new Dictionary&lt;string, string&gt;() { { "key", "value" } },
    ///         ResourceType = "company",
    ///     }
    /// );
    /// </code></example>
    public async Task<IssueTemporaryAccessTokenResponse> IssueTemporaryAccessTokenAsync(
        IssueTemporaryAccessTokenRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
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
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<IssueTemporaryAccessTokenResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new SchematicException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
                    case 404:
                        throw new NotFoundError(JsonUtils.Deserialize<ApiError>(responseBody));
                    case 500:
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ApiError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }
}
