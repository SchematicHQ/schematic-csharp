using System.Net.Http;
using System.Threading;
using global::System.Threading.Tasks;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public partial class SchematicApi
{
    private readonly RawClient _client;

    public SchematicApi(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Schematic-Api-Key", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "SchematicHQ.Client" },
                { "X-Fern-SDK-Version", Version.Current },
                { "User-Agent", "SchematicHQ.Client/AUTO" },
            }
        );
        clientOptions ??= new ClientOptions();
        foreach (var header in defaultHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        _client = new RawClient(clientOptions);
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
        Plangroups = new PlangroupsClient(_client);
        Accesstokens = new AccesstokensClient(_client);
        Webhooks = new WebhooksClient(_client);
    }

    public AccountsClient Accounts { get; }

    public BillingClient Billing { get; }

    public CreditsClient Credits { get; }

    public CheckoutClient Checkout { get; }

    public CompaniesClient Companies { get; }

    public EntitlementsClient Entitlements { get; }

    public PlansClient Plans { get; }

    public ComponentsClient Components { get; }

    public DataexportsClient Dataexports { get; }

    public EventsClient Events { get; }

    public FeaturesClient Features { get; }

    public PlangroupsClient Plangroups { get; }

    public AccesstokensClient Accesstokens { get; }

    public WebhooksClient Webhooks { get; }

    /// <example><code>
    /// await client.PutPlanAudiencesPlanAudienceIdAsync("plan_audience_id");
    /// </code></example>
    public async global::System.Threading.Tasks.Task PutPlanAudiencesPlanAudienceIdAsync(
        string planAudienceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Put,
                    Path = string.Format(
                        "plan-audiences/{0}",
                        ValueConvert.ToPathParameterString(planAudienceId)
                    ),
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
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
    public async global::System.Threading.Tasks.Task DeletePlanAudiencesPlanAudienceIdAsync(
        string planAudienceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "plan-audiences/{0}",
                        ValueConvert.ToPathParameterString(planAudienceId)
                    ),
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
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            throw new SchematicApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }
}
