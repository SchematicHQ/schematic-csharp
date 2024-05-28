using System.Text.Json;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class AccountsClient
{
    private RawClient _client;

    public AccountsClient(RawClient client)
    {
        _client = client;
    }

    public async Task<ListApiKeysResponse> ListApiKeysAsync(ListApiKeysRequest request)
    {
        var _query = new Dictionary<string, object>()
        {
            { "require_environment", request.RequireEnvironment.ToString() },
        };
        if (request.EnvironmentId != null)
        {
            _query["environment_id"] = request.EnvironmentId;
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
                Path = "/api-keys",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListApiKeysResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<CreateApiKeyResponse> CreateApiKeyAsync(CreateApiKeyRequestBody request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/api-keys",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CreateApiKeyResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<GetApiKeyResponse> GetApiKeyAsync(string apiKeyId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest { Method = HttpMethod.Get, Path = $"/api-keys/{apiKeyId}" }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetApiKeyResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<UpdateApiKeyResponse> UpdateApiKeyAsync(
        string apiKeyId,
        UpdateApiKeyRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"/api-keys/{apiKeyId}",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpdateApiKeyResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<DeleteApiKeyResponse> DeleteApiKeyAsync(string apiKeyId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest { Method = HttpMethod.Delete, Path = $"/api-keys/{apiKeyId}" }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<DeleteApiKeyResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<CountApiKeysResponse> CountApiKeysAsync(CountApiKeysRequest request)
    {
        var _query = new Dictionary<string, object>()
        {
            { "require_environment", request.RequireEnvironment.ToString() },
        };
        if (request.EnvironmentId != null)
        {
            _query["environment_id"] = request.EnvironmentId;
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
                Path = "/api-keys/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountApiKeysResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<ListApiRequestsResponse> ListApiRequestsAsync(ListApiRequestsRequest request)
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.RequestType != null)
        {
            _query["request_type"] = request.RequestType;
        }
        if (request.EnvironmentId != null)
        {
            _query["environment_id"] = request.EnvironmentId;
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
                Path = "/api-requests",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListApiRequestsResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<GetApiRequestResponse> GetApiRequestAsync(string apiRequestId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/api-requests/{apiRequestId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetApiRequestResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<CountApiRequestsResponse> CountApiRequestsAsync(
        CountApiRequestsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Q != null)
        {
            _query["q"] = request.Q;
        }
        if (request.RequestType != null)
        {
            _query["request_type"] = request.RequestType;
        }
        if (request.EnvironmentId != null)
        {
            _query["environment_id"] = request.EnvironmentId;
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
                Path = "/api-requests/count",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CountApiRequestsResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<ListEnvironmentsResponse> ListEnvironmentsAsync(
        ListEnvironmentsRequest request
    )
    {
        var _query = new Dictionary<string, object>() { };
        if (request.Ids != null)
        {
            _query["ids"] = request.Ids;
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
                Path = "/environments",
                Query = _query
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<ListEnvironmentsResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<CreateEnvironmentResponse> CreateEnvironmentAsync(
        CreateEnvironmentRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Post,
                Path = "/environments",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<CreateEnvironmentResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<GetEnvironmentResponse> GetEnvironmentAsync(string environmentId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"/environments/{environmentId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<GetEnvironmentResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<UpdateEnvironmentResponse> UpdateEnvironmentAsync(
        string environmentId,
        UpdateEnvironmentRequestBody request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"/environments/{environmentId}",
                Body = request
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<UpdateEnvironmentResponse>(responseBody);
        }
        throw new Exception();
    }

    public async Task<DeleteEnvironmentResponse> DeleteEnvironmentAsync(string environmentId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.ApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"/environments/{environmentId}"
            }
        );
        string responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode >= 200 && response.StatusCode < 400)
        {
            return JsonSerializer.Deserialize<DeleteEnvironmentResponse>(responseBody);
        }
        throw new Exception();
    }
}
