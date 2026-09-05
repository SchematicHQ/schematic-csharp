namespace SchematicHQ.Client;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
[Serializable]
public class PaymentRequiredError(ApiError body, SchematicHQ.Client.RawResponse? rawResponse = null)
    : SchematicApiException("PaymentRequiredError", 402, body, rawResponse: rawResponse)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ApiError Body => body;
}
