using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SchematicHQ.Client.ContextSignatures
{
    /// <summary>
    /// Signs the context a browser SDK sends with a publishable key, for environments that
    /// require context signatures.
    /// </summary>
    /// <remarks>
    /// A publishable key is public, so the API can't trust the company and user a browser names
    /// with it. When the environment requires signatures, your backend, which holds the secret API
    /// key, signs the logged-in user's own company and user, and the browser SDK sends that
    /// signature in the X-Schematic-Context-Sig header. Sign the context from your own
    /// authenticated session, never from what the browser asks for.
    ///
    /// The signature is <c>iat.exp.sig</c>, where <c>sig</c> is the unpadded base64url
    /// HMAC-SHA256 of <c>canonical(context).iat.exp</c> keyed with the secret API key, and
    /// <c>canonical</c> is the context's JSON with keys sorted by code point and no whitespace.
    /// </remarks>
    public static class ContextSignature
    {
        /// <summary>
        /// Request header the browser SDK sends the signature in.
        /// </summary>
        public const string Header = "X-Schematic-Context-Sig";

        /// <summary>
        /// How long a signature is valid for unless <see cref="ContextSignatureOptions.Lifetime"/>
        /// says otherwise. It matches how long the browser SDK reuses one.
        /// </summary>
        public static readonly TimeSpan DefaultLifetime = TimeSpan.FromMinutes(10);

        /// <summary>
        /// The longest the API accepts a signature as valid for.
        /// </summary>
        public static readonly TimeSpan MaxLifetime = TimeSpan.FromHours(1);

        /// <summary>
        /// Returns the X-Schematic-Context-Sig value for a context.
        /// </summary>
        /// <param name="secret">The secret API key for the environment the browser SDK's publishable key belongs to.</param>
        /// <param name="company">The logged-in user's company keys, or null to leave the company out. An empty dictionary is signed as empty.</param>
        /// <param name="user">The logged-in user's keys, or null to leave the user out. An empty dictionary is signed as empty.</param>
        /// <param name="options">When the signature is valid from, and for how long.</param>
        /// <returns>The signature, valid from now (or <see cref="ContextSignatureOptions.IssuedAt"/>) for the lifetime.</returns>
        /// <exception cref="ContextSignatureException">The secret is missing, the lifetime is out of range, or a key's value is null.</exception>
        public static string Sign(
            string secret,
            IDictionary<string, string>? company,
            IDictionary<string, string>? user,
            ContextSignatureOptions? options = null)
        {
            var lifetime = options?.Lifetime ?? DefaultLifetime;
            if (lifetime < TimeSpan.FromSeconds(1) || lifetime > MaxLifetime || lifetime.Ticks % TimeSpan.TicksPerSecond != 0)
            {
                throw new ContextSignatureException("Lifetime must be a whole number of seconds, from one second to one hour.");
            }

            var iat = (options?.IssuedAt ?? DateTimeOffset.UtcNow).ToUnixTimeSeconds();
            return SignAt(secret, company, user, iat, iat + (long)lifetime.TotalSeconds);
        }

        /// <summary>
        /// Signs a context for an explicit <c>iat</c> and <c>exp</c>, in unix seconds.
        /// </summary>
        internal static string SignAt(
            string secret,
            IDictionary<string, string>? company,
            IDictionary<string, string>? user,
            long iat,
            long exp)
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new ContextSignatureException("A secret API key is required to sign a context.");
            }

            var payload = Canonicalize(company, user) + "." + iat.ToString(CultureInfo.InvariantCulture) + "." +
                          exp.ToString(CultureInfo.InvariantCulture);
            byte[] mac;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                mac = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            }

            var sig = Convert.ToBase64String(mac).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            return iat.ToString(CultureInfo.InvariantCulture) + "." + exp.ToString(CultureInfo.InvariantCulture) + "." + sig;
        }

        /// <summary>
        /// Encodes a context the way the API does before verifying it: keys sorted by code point,
        /// no whitespace, <c>&lt;</c>, <c>&gt;</c>, <c>&amp;</c> and <c>/</c> left literal, U+2028 and
        /// U+2029 escaped, and a lone surrogate read as U+FFFD.
        /// </summary>
        internal static string Canonicalize(IDictionary<string, string>? company, IDictionary<string, string>? user)
        {
            var parts = new List<string>();
            // "company" sorts before "user".
            if (company != null)
            {
                parts.Add("\"company\":" + CanonicalizeKeys(company));
            }
            if (user != null)
            {
                parts.Add("\"user\":" + CanonicalizeKeys(user));
            }
            return "{" + string.Join(",", parts) + "}";
        }

        private static string CanonicalizeKeys(IDictionary<string, string> keys)
        {
            var entries = keys
                .Select(kv =>
                {
                    if (kv.Value == null)
                    {
                        throw new ContextSignatureException($"The value of context key \"{kv.Key}\" can't be null.");
                    }
                    return (Name: ToWellFormed(kv.Key), Value: ToWellFormed(kv.Value));
                })
                .OrderBy(kv => kv.Name, CodePointComparer.Instance)
                .Select(kv => EncodeString(kv.Name) + ":" + EncodeString(kv.Value));
            return "{" + string.Join(",", entries) + "}";
        }

        // The API reads a lone surrogate as U+FFFD, so sign it as one.
        private static string ToWellFormed(string value)
        {
            var sb = new StringBuilder(value.Length);
            for (var i = 0; i < value.Length; i++)
            {
                var c = value[i];
                if (char.IsHighSurrogate(c) && i + 1 < value.Length && char.IsLowSurrogate(value[i + 1]))
                {
                    sb.Append(c).Append(value[i + 1]);
                    i++;
                }
                else if (char.IsSurrogate(c))
                {
                    sb.Append('\uFFFD');
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        // Escapes a string as Go's encoding/json does with HTML escaping off.
        private static string EncodeString(string value)
        {
            var sb = new StringBuilder(value.Length + 2);
            sb.Append('"');
            foreach (var c in value)
            {
                switch (c)
                {
                    case '"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\u2028':
                        sb.Append("\\u2028");
                        break;
                    case '\u2029':
                        sb.Append("\\u2029");
                        break;
                    default:
                        if (c < 0x20)
                        {
                            sb.Append("\\u").Append(((int)c).ToString("x4", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            sb.Append('"');
            return sb.ToString();
        }

        // The API sorts keys by their UTF-8 bytes, which is code point order. Ordinal string
        // comparison uses UTF-16 code units, which puts a character outside the BMP (a surrogate
        // pair) before U+E000 to U+FFFF.
        private sealed class CodePointComparer : IComparer<string>
        {
            public static readonly CodePointComparer Instance = new CodePointComparer();

            public int Compare(string? x, string? y)
            {
                var a = Encoding.UTF8.GetBytes(x ?? string.Empty);
                var b = Encoding.UTF8.GetBytes(y ?? string.Empty);
                var length = Math.Min(a.Length, b.Length);
                for (var i = 0; i < length; i++)
                {
                    if (a[i] != b[i])
                    {
                        return a[i].CompareTo(b[i]);
                    }
                }
                return a.Length.CompareTo(b.Length);
            }
        }
    }
}
