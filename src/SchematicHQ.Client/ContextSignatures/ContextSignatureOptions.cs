using System;

namespace SchematicHQ.Client.ContextSignatures
{
    /// <summary>
    /// Options for <see cref="ContextSignature.Sign"/>.
    /// </summary>
    public class ContextSignatureOptions
    {
        /// <summary>
        /// When the signature is valid from; now when null. Counted in whole seconds.
        /// </summary>
        public DateTimeOffset? IssuedAt { get; set; }

        /// <summary>
        /// How long the signature is valid for, in whole seconds, from one second up to
        /// <see cref="ContextSignature.MaxLifetime"/>; <see cref="ContextSignature.DefaultLifetime"/> when null.
        /// </summary>
        public TimeSpan? Lifetime { get; set; }
    }
}
