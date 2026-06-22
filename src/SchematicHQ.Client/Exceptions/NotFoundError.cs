namespace SchematicHQ.Client;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
[Serializable]
public class NotFoundError(ApiError body, SchematicHQ.Client.RawResponse? rawResponse = null)
    : SchematicApiException("NotFoundError", 404, body, rawResponse: rawResponse)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ApiError Body => body;
}
