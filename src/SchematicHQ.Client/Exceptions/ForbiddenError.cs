using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class ForbiddenError(ApiError body) : SchematicApiApiException("ForbiddenError", 403, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ApiError Body { get; } = body;
}
