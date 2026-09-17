using System;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// The three lease calls the manager makes. Narrow on purpose: it keeps the
/// manager independent of the generated client's request and response models,
/// and lets tests script the server.
/// </summary>
public interface ILeaseWireClient
{
    Task<LeaseGrant> AcquireAsync(
        string companyId,
        string creditTypeId,
        double requestedAmount,
        DateTime expiresAt,
        RequestOptions? options = null
    );

    Task<LeaseGrant> ExtendAsync(
        string leaseId,
        double additionalAmount,
        DateTime expiresAt,
        RequestOptions? options = null
    );

    Task ReleaseAsync(string leaseId, RequestOptions? options = null);
}

/// <summary>
/// Adapts the generated credits client to <see cref="ILeaseWireClient"/>.
/// </summary>
public sealed class ApiLeaseWireClient : ILeaseWireClient
{
    private readonly CreditsClient _credits;

    public ApiLeaseWireClient(CreditsClient credits)
    {
        _credits = credits ?? throw new ArgumentNullException(nameof(credits));
    }

    /// <summary>
    /// Acquire takes the default retry policy: the server hands back the slot's
    /// existing active lease rather than opening a second one, so a retry after
    /// a lost response returns the lease the first attempt created.
    /// </summary>
    public async Task<LeaseGrant> AcquireAsync(
        string companyId,
        string creditTypeId,
        double requestedAmount,
        DateTime expiresAt,
        RequestOptions? options = null
    )
    {
        var response = await _credits
            .AcquireCreditLeaseAsync(
                new AcquireCreditLeaseRequestBody
                {
                    CompanyId = companyId,
                    CreditTypeId = creditTypeId,
                    RequestedAmount = requestedAmount,
                    ExpiresAt = expiresAt,
                },
                options
            )
            .ConfigureAwait(false);
        return FromResponse(response.Data);
    }

    public async Task<LeaseGrant> ExtendAsync(
        string leaseId,
        double additionalAmount,
        DateTime expiresAt,
        RequestOptions? options = null
    )
    {
        // An extend is an increment, so a retry without a key would grant the
        // tranche twice. The key is minted once per extend and the retry loop
        // resends this body, so every attempt of this extend collapses to one
        // grow while a later extend gets its own key.
        var response = await _credits
            .ExtendCreditLeaseAsync(
                leaseId,
                new ExtendCreditLeaseRequestBody
                {
                    AdditionalAmount = additionalAmount,
                    ExpiresAt = expiresAt,
                    IdempotencyKey = Guid.NewGuid().ToString(),
                },
                options
            )
            .ConfigureAwait(false);
        return FromResponse(response.Data);
    }

    public async Task ReleaseAsync(string leaseId, RequestOptions? options = null)
    {
        await _credits.ReleaseCreditLeaseAsync(leaseId, options).ConfigureAwait(false);
    }

    private static LeaseGrant FromResponse(CreditLeaseResponseData data)
    {
        if (data == null)
        {
            throw new InvalidOperationException("credit lease response carried no data");
        }
        return new LeaseGrant
        {
            LeaseId = data.Id,
            CompanyId = data.CompanyId,
            CreditTypeId = data.CreditTypeId,
            GrantedAmount = data.GrantedAmount,
            ExpiresAt = data.ExpiresAt,
        };
    }
}
