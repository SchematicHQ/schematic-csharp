using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class NotFoundError(ApiError body) : SchematicApiApiException("NotFoundError", 404, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ApiError Body { get; } = body;
}
