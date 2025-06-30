namespace SchematicHQ.Client;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
[Serializable]
public class InternalServerError(ApiError body)
    : SchematicApiException("InternalServerError", 500, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ApiError Body => body;
}
