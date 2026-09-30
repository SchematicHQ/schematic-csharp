using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Stands in for the server's check-and-reserve and release calls, recording
/// what the flow sent so a test can assert on the request as well as the
/// verdict.
/// </summary>
public sealed class ScriptedServerReservationClient : IServerReservationClient
{
    /// <summary>
    /// What the next check-and-reserve answers with. Unset with
    /// <see cref="NextError"/> unset means the test expected no call.
    /// </summary>
    public CheckAndReserveFlagResponseData? NextResponse { get; set; }

    public Exception? NextError { get; set; }

    public int CheckCount { get; private set; }

    public string? LastFlagKey { get; private set; }

    public CheckAndReserveFlagRequestBody? LastBody { get; private set; }

    public RequestOptions? LastOptions { get; private set; }

    public List<string> ReleasedReservationIds { get; } = new();

    public Task<CheckAndReserveFlagResponseData> CheckAndReserveAsync(
        string flagKey,
        CheckAndReserveFlagRequestBody body,
        RequestOptions? options = null
    )
    {
        CheckCount++;
        LastFlagKey = flagKey;
        LastBody = body;
        LastOptions = options;

        if (NextError != null)
        {
            var error = NextError;
            NextError = null;
            throw error;
        }
        return Task.FromResult(
            NextResponse
                ?? throw new InvalidOperationException("no response was scripted for this check")
        );
    }

    public Task ReleaseReservationAsync(string reservationId)
    {
        ReleasedReservationIds.Add(reservationId);
        return Task.CompletedTask;
    }
}
