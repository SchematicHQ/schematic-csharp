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
                { "User-Agent", "SchematicHQ.Client/1.4.2" },
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
        Checkout = new CheckoutClient(_client);
        Companies = new CompaniesClient(_client);
        Entitlements = new EntitlementsClient(_client);
        Plans = new PlansClient(_client);
        Components = new ComponentsClient(_client);
        Dataexports = new DataexportsClient(_client);
        Events = new EventsClient(_client);
        Features = new FeaturesClient(_client);
        Planbundle = new PlanbundleClient(_client);
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

    public ICheckoutClient Checkout { get; }

    public ICompaniesClient Companies { get; }

    public IEntitlementsClient Entitlements { get; }

    public IPlansClient Plans { get; }

    public IComponentsClient Components { get; }

    public IDataexportsClient Dataexports { get; }

    public IEventsClient Events { get; }

    public IFeaturesClient Features { get; }

    public IPlanbundleClient Planbundle { get; }

    public IPlangroupsClient Plangroups { get; }

    public IPlanmigrationsClient Planmigrations { get; }

    public IComponentspublicClient Componentspublic { get; }

    public IScheduledcheckoutClient Scheduledcheckout { get; }

    public IAccesstokensClient Accesstokens { get; }

    public IWebhooksClient Webhooks { get; }

    /// <example><code>
    /// await client.PutPlanAudiencesPlanAudienceIdAsync("plan_audience_id");
    /// </code></example>
    public async Task PutPlanAudiencesPlanAudienceIdAsync(
        string planAudienceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
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
                    Method = HttpMethod.Put,
                    Path = string.Format(
                        "plan-audiences/{0}",
                        ValueConvert.ToPathParameterString(planAudienceId)
                    ),
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <example><code>
    /// await client.DeletePlanAudiencesPlanAudienceIdAsync("plan_audience_id");
    /// </code></example>
    public async Task DeletePlanAudiencesPlanAudienceIdAsync(
        string planAudienceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
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
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "plan-audiences/{0}",
                        ValueConvert.ToPathParameterString(planAudienceId)
                    ),
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
        }
        {
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }
}
