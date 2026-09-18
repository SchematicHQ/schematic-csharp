using SchematicHQ.Client;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Stands in for the server's three lease calls. The vector scripts one
/// response per operation and the runner installs it just before the call, so
/// the manager sees exactly the server the vector describes.
/// </summary>
public sealed class ScriptedLeaseWireClient : ILeaseWireClient
{
    /// <summary>
    /// The grant the next acquire returns. Null with
    /// <see cref="NextAcquireError"/> unset means the vector scripted no
    /// acquire, and one happening is itself the failure the counts below catch.
    /// </summary>
    public LeaseGrant? NextAcquire { get; set; }

    public string? NextAcquireError { get; set; }

    public LeaseGrant? NextExtend { get; set; }

    public string? NextExtendError { get; set; }

    /// <summary>
    /// Runs while the acquire is in flight, which is how a vector emulates a
    /// sibling pod winning the race for the slot.
    /// </summary>
    public Func<Task>? InstallDuringWire { get; set; }

    public int AcquireCount { get; private set; }

    public double? LastAcquireRequestedAmount { get; private set; }

    public int ExtendCount { get; private set; }

    public double? LastExtendAdditionalAmount { get; private set; }

    public string? LastExtendLeaseId { get; private set; }

    public List<string> ReleasedLeaseIds { get; } = new();

    public async Task<LeaseGrant> AcquireAsync(
        string companyId,
        string creditTypeId,
        double requestedAmount,
        DateTime expiresAt,
        RequestOptions? options = null
    )
    {
        AcquireCount++;
        LastAcquireRequestedAmount = requestedAmount;

        if (InstallDuringWire != null)
        {
            var install = InstallDuringWire;
            InstallDuringWire = null;
            await install().ConfigureAwait(false);
        }

        if (NextAcquireError != null)
        {
            var error = NextAcquireError;
            NextAcquireError = null;
            throw new InvalidOperationException(error);
        }

        var grant =
            NextAcquire
            ?? throw new InvalidOperationException("no acquire was scripted for this operation");
        NextAcquire = null;
        return new LeaseGrant
        {
            LeaseId = grant.LeaseId,
            CompanyId = companyId,
            CreditTypeId = creditTypeId,
            GrantedAmount = grant.GrantedAmount,
            ExpiresAt = grant.ExpiresAt,
        };
    }

    public Task<LeaseGrant> ExtendAsync(
        string leaseId,
        double additionalAmount,
        DateTime expiresAt,
        RequestOptions? options = null
    )
    {
        ExtendCount++;
        LastExtendAdditionalAmount = additionalAmount;
        LastExtendLeaseId = leaseId;

        if (NextExtendError != null)
        {
            var error = NextExtendError;
            NextExtendError = null;
            throw new InvalidOperationException(error);
        }

        var grant =
            NextExtend
            ?? throw new InvalidOperationException("no extend was scripted for this operation");
        NextExtend = null;
        return Task.FromResult(
            new LeaseGrant
            {
                LeaseId = leaseId,
                GrantedAmount = grant.GrantedAmount,
                ExpiresAt = grant.ExpiresAt,
            }
        );
    }

    public Task ReleaseAsync(string leaseId, RequestOptions? options = null)
    {
        ReleasedLeaseIds.Add(leaseId);
        return Task.CompletedTask;
    }
}
