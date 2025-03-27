using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SchematicHQ.Client.Webhooks.WebhookUtils
{
    /// <summary>
    /// Utilities for verifying Schematic webhook signatures.
    /// </summary>
    public static class WebhookVerifier
    {
        /// <summary>
        /// Header containing the webhook signature.
        /// </summary>
        public const string WebhookSignatureHeader = "X-Schematic-Webhook-Signature";

        /// <summary>
        /// Header containing the webhook timestamp.
        /// </summary>
        public const string WebhookTimestampHeader = "X-Schematic-Webhook-Timestamp";

        /// <summary>
        /// Verifies the signature of a webhook request.
        /// </summary>
        /// <param name="body">The webhook request body</param>
        /// <param name="headers">Dictionary of HTTP headers</param>
        /// <param name="secret">The webhook secret</param>
        /// <exception cref="WebhookSignatureException">Thrown when the webhook signature is invalid</exception>
        public static void VerifyWebhookSignature(
            string body,
            IReadOnlyDictionary<string, string> headers,
            string secret)
        {
            // Extract the signature and timestamp from headers
            string signature = GetHeaderValue(headers, WebhookSignatureHeader);
            string timestamp = GetHeaderValue(headers, WebhookTimestampHeader);

            // Verify the signature
            VerifySignature(body, signature, timestamp, secret);
        }

        /// <summary>
        /// Verifies the signature of a webhook payload.
        /// </summary>
        /// <param name="body">The webhook payload</param>
        /// <param name="signature">The signature from the webhook request headers</param>
        /// <param name="timestamp">The timestamp from the webhook request headers</param>
        /// <param name="secret">The webhook secret</param>
        /// <exception cref="WebhookSignatureException">Thrown when the webhook signature is invalid</exception>
        public static void VerifySignature(
            string body,
            string signature,
            string timestamp,
            string secret)
        {
            // Check if signature and timestamp are provided
            if (string.IsNullOrEmpty(signature))
            {
                throw new WebhookSignatureException("Missing webhook signature");
            }

            if (string.IsNullOrEmpty(timestamp))
            {
                throw new WebhookSignatureException("Missing webhook timestamp");
            }

            // Compute the expected signature
            string expectedSignature = ComputeHexSignature(body, timestamp, secret);

            // Compare signatures using constant-time comparison to prevent timing attacks
            if (!SecureEquals(FromHexString(expectedSignature), FromHexString(signature)))
            {
                throw new WebhookSignatureException("Invalid signature");
            }
        }

        /// <summary>
        /// Computes a hex-encoded HMAC-SHA256 signature for a webhook payload.
        /// </summary>
        /// <param name="body">The webhook payload</param>
        /// <param name="timestamp">The timestamp</param>
        /// <param name="secret">The webhook secret</param>
        /// <returns>The hex-encoded signature</returns>
        public static string ComputeHexSignature(string body, string timestamp, string secret)
        {
            byte[] signature = ComputeSignature(body, timestamp, secret);
            return BitConverter.ToString(signature).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Computes an HMAC-SHA256 signature for a webhook payload.
        /// </summary>
        /// <param name="body">The webhook payload</param>
        /// <param name="timestamp">The timestamp</param>
        /// <param name="secret">The webhook secret</param>
        /// <returns>The signature bytes</returns>
        public static byte[] ComputeSignature(string body, string timestamp, string secret)
        {
            // Create message by concatenating body and timestamp
            string message = $"{body}+{timestamp}";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);

            // Compute HMAC-SHA256
            using (var hmac = new HMACSHA256(secretBytes))
            {
                return hmac.ComputeHash(messageBytes);
            }
        }

        /// <summary>
        /// Gets a header value from a dictionary of headers.
        /// </summary>
        /// <param name="headers">The dictionary of headers</param>
        /// <param name="headerName">The name of the header to get</param>
        /// <returns>The header value, or an empty string if it doesn't exist</returns>
        private static string GetHeaderValue(IReadOnlyDictionary<string, string> headers, string headerName)
        {
            if (headers == null)
            {
                return string.Empty;
            }

            // Try case-sensitive match
            if (headers.TryGetValue(headerName, out string? value))
            {
                return value ?? string.Empty;
            }

            // Try case-insensitive match
            foreach (var header in headers)
            {
                if (string.Equals(header.Key, headerName, StringComparison.OrdinalIgnoreCase))
                {
                    return header.Value ?? string.Empty;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts a hex string to a byte array.
        /// </summary>
        /// <param name="hex">The hex string</param>
        /// <returns>The byte array</returns>
        /// <exception cref="WebhookSignatureException">Thrown when the hex string is invalid</exception>
        private static byte[] FromHexString(string hex)
        {
            try
            {
                if (string.IsNullOrEmpty(hex))
                {
                    return Array.Empty<byte>();
                }

                int numberChars = hex.Length;
                byte[] bytes = new byte[numberChars / 2];
                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                }
                return bytes;
            }
            catch (Exception ex)
            {
                throw new WebhookSignatureException("Invalid signature format", ex);
            }
        }

        /// <summary>
        /// Compares two byte arrays in constant time to prevent timing attacks.
        /// </summary>
        /// <param name="a">First byte array</param>
        /// <param name="b">Second byte array</param>
        /// <returns>true if the arrays are equal, false otherwise</returns>
        private static bool SecureEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }

            return result == 0;
        }
    }
}