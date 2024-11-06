using System;
using System.Net.Http;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public partial class SchematicApi
{
    private RawClient _client;

    public SchematicApi(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        _client = new RawClient(
            new Dictionary<string, string>()
            {
                { "X-Schematic-Api-Key", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "SchematicHQ.Client" },
                { "X-Fern-SDK-Version", "1.0.3" },
            },
            new Dictionary<string, Func<string>>() { },
            clientOptions ?? new ClientOptions()
        );
        Accounts = new AccountsClient(_client);
        Features = new FeaturesClient(_client);
        Billing = new BillingClient(_client);
        Companies = new CompaniesClient(_client);
        Entitlements = new EntitlementsClient(_client);
        Components = new ComponentsClient(_client);
        Crm = new CrmClient(_client);
        Events = new EventsClient(_client);
        Plans = new PlansClient(_client);
        Plangroups = new PlangroupsClient(_client);
        Accesstokens = new AccesstokensClient(_client);
        Webhooks = new WebhooksClient(_client);
    }

    public AccountsClient Accounts { get; init; }

    public FeaturesClient Features { get; init; }

    public BillingClient Billing { get; init; }

    public CompaniesClient Companies { get; init; }

    public EntitlementsClient Entitlements { get; init; }

    public ComponentsClient Components { get; init; }

    public CrmClient Crm { get; init; }

    public EventsClient Events { get; init; }

    public PlansClient Plans { get; init; }

    public PlangroupsClient Plangroups { get; init; }

    public AccesstokensClient Accesstokens { get; init; }

    public WebhooksClient Webhooks { get; init; }

    public async Task GetCompanyPlansAsync(RequestOptions? options = null)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "company-plans",
                Options = options
            }
        );
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
        }
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        throw new SchematicApiApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            JsonUtils.Deserialize<object>(responseBody)
        );
    }
}
