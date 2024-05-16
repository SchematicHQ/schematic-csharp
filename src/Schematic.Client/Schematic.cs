using Schematic.Client;

namespace Schematic.Client;

public partial class Schematic
{
    private RawClient _client;

    public Schematic(string apiKey = null, ClientOptions clientOptions = null)
    {
        _client = new RawClient(
            new Dictionary<string, string>()
            {
                { "X-Schematic-Api-Key", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "schematic_fern_api_sdk" },
                { "X-Fern-SDK-Version", "0.0.8" },
            },
            clientOptions ?? new ClientOptions()
        );
        Accounts = new AccountsClient(_client);
        Features = new FeaturesClient(_client);
        Billing = new BillingClient(_client);
        Companies = new CompaniesClient(_client);
        Entitlements = new EntitlementsClient(_client);
        Events = new EventsClient(_client);
        Plans = new PlansClient(_client);
    }

    public AccountsClient Accounts { get; }

    public FeaturesClient Features { get; }

    public BillingClient Billing { get; }

    public CompaniesClient Companies { get; }

    public EntitlementsClient Entitlements { get; }

    public EventsClient Events { get; }

    public PlansClient Plans { get; }

    private string GetFromEnvironmentOrThrow(string env, string message)
    {
        var value = Environment.GetEnvironmentVariable(env);
        if (value == null)
        {
            throw new Exception(message);
        }
        return value;
    }
}
