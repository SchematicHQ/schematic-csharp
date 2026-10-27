using System;

namespace SchematicHQ.Client.ContextSignatures
{
    /// <summary>
    /// Exception thrown when a context can't be signed.
    /// </summary>
    public class ContextSignatureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextSignatureException"/> class with a message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ContextSignatureException(string message) : base(message) { }
    }
}
