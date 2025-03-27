using System;

namespace SchematicHQ.Client.Webhooks.WebhookUtils
{
    /// <summary>
    /// Exception thrown when a webhook signature verification fails.
    /// </summary>
    public class WebhookSignatureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookSignatureException"/> class.
        /// </summary>
        public WebhookSignatureException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookSignatureException"/> class with a message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public WebhookSignatureException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookSignatureException"/> class with a message and inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public WebhookSignatureException(string message, Exception innerException) : base(message, innerException) { }
    }
}