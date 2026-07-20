using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class SchematicApi : ISchematicApi
{
    private readonly RawClient _client;

    public SchematicApi(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        clientOptions ??= new ClientOptions();
        var platformHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "SchematicHQ.Client" },
                { "X-Fern-SDK-Version", Version.Current },
                { "User-Agent", "SchematicHQ.Client/1.5.2" },
            }
        );
        foreach (var header in platformHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        var clientOptionsWithAuth = clientOptions.Clone();
        var authHeaders = new Headers(
            new Dictionary<string, string>() { { "X-Schematic-Api-Key", apiKey ?? "" } }
        );
        foreach (var header in authHeaders)
        {
            clientOptionsWithAuth.Headers[header.Key] = header.Value;
        }
        _client = new RawClient(clientOptionsWithAuth);
        Accounts = new AccountsClient(_client);
        Billing = new BillingClient(_client);
        Credits = new CreditsClient(_client);
        Catalogs = new CatalogsClient(_client);
        Checkout = new CheckoutClient(_client);
        Companies = new CompaniesClient(_client);
        Entitlements = new EntitlementsClient(_client);
        Plans = new PlansClient(_client);
        Components = new ComponentsClient(_client);
        Planbundle = new PlanbundleClient(_client);
        Dataexports = new DataexportsClient(_client);
        Events = new EventsClient(_client);
        Features = new FeaturesClient(_client);
        Insights = new InsightsClient(_client);
        Integrationsapi = new IntegrationsapiClient(_client);
        Plangroups = new PlangroupsClient(_client);
        Planmigrations = new PlanmigrationsClient(_client);
        Componentspublic = new ComponentspublicClient(_client);
        Scheduledcheckout = new ScheduledcheckoutClient(_client);
        Accesstokens = new AccesstokensClient(_client);
        Webhooks = new WebhooksClient(_client);
    }

    public IAccountsClient Accounts { get; }

    public IBillingClient Billing { get; }

    public ICreditsClient Credits { get; }

    public ICatalogsClient Catalogs { get; }

    public ICheckoutClient Checkout { get; }

    public ICompaniesClient Companies { get; }

    public IEntitlementsClient Entitlements { get; }

    public IPlansClient Plans { get; }

    public IComponentsClient Components { get; }

    public IPlanbundleClient Planbundle { get; }

    public IDataexportsClient Dataexports { get; }

    public IEventsClient Events { get; }

    public IFeaturesClient Features { get; }

    public IInsightsClient Insights { get; }

    public IIntegrationsapiClient Integrationsapi { get; }

    public IPlangroupsClient Plangroups { get; }

    public IPlanmigrationsClient Planmigrations { get; }

    public IComponentspublicClient Componentspublic { get; }

    public IScheduledcheckoutClient Scheduledcheckout { get; }

    public IAccesstokensClient Accesstokens { get; }

    public IWebhooksClient Webhooks { get; }

    private async Task<RawResponse> GetCreditLedgerAsyncCore(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new SchematicHQ.Client.Core.QueryStringBuilder.Builder(capacity: 0)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new SchematicHQ.Client.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    Method = HttpMethod.Get,
                    Path = "billing/credits/ledger",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            return new SchematicHQ.Client.RawResponse()
            {
                StatusCode = response.Raw.StatusCode,
                Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
            };
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody,
                rawResponse: new SchematicHQ.Client.RawResponse()
                {
                    StatusCode = response.Raw.StatusCode,
                    Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                    Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                }
            );
        }
    }

    /// <example><code>
    /// await client.GetCreditLedgerAsync();
    /// </code></example>
    public WithRawResponseTask GetCreditLedgerAsync(
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask(GetCreditLedgerAsyncCore(options, cancellationToken));
    }
}
