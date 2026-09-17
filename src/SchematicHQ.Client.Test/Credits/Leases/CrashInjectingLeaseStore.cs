using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Passes every call through to the wrapped store, except that one refund can
/// be armed to throw. That is how the conformance suite reproduces crash window
/// 2: the claim inside a consume has landed, the refund that follows it has
/// not.
/// </summary>
public sealed class CrashInjectingLeaseStore : ILeaseStore
{
    private readonly ILeaseStore _inner;

    public CrashInjectingLeaseStore(ILeaseStore inner)
    {
        _inner = inner;
    }

    /// <summary>
    /// The store underneath. The lease manager is handed this rather than the
    /// decorator, so its "release on close only for a per-process store" guard
    /// still keys off the real backend's type.
    /// </summary>
    public ILeaseStore Inner => _inner;

    /// <summary>
    /// Arms the next refund to throw. One shot: it disarms itself as it fires.
    /// </summary>
    public bool FailNextRefund { get; set; }

    public Task<LeaseState?> GetAsync(string companyId, string creditTypeId) =>
        _inner.GetAsync(companyId, creditTypeId);

    public Task<bool> ReplaceAsync(LeaseGrant grant) => _inner.ReplaceAsync(grant);

    public Task<ReserveResult?> TryReserveAsync(
        string companyId,
        string creditTypeId,
        double credits
    ) => _inner.TryReserveAsync(companyId, creditTypeId, credits);

    public Task RefundAsync(
        string companyId,
        string creditTypeId,
        double credits,
        string? pinLeaseId = null
    )
    {
        if (FailNextRefund)
        {
            FailNextRefund = false;
            throw new InvalidOperationException("injected crash before refund");
        }
        return _inner.RefundAsync(companyId, creditTypeId, credits, pinLeaseId);
    }

    public Task ExtendAsync(
        string companyId,
        string creditTypeId,
        double grantedTotal,
        DateTime? newExpiresAt = null,
        string? pinLeaseId = null
    ) => _inner.ExtendAsync(companyId, creditTypeId, grantedTotal, newExpiresAt, pinLeaseId);

    public Task DropAsync(string companyId, string creditTypeId) =>
        _inner.DropAsync(companyId, creditTypeId);
}
