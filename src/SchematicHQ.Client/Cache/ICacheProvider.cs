
namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// Interface for cache providers used by SchematicHQ client
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Get a value from the cache by key
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>The cached value or default if not found or expired</returns>
        ValueTask<T?> Get<T>(string key);

        /// <summary>
        /// Set a value in the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="val">Value to cache</param>
        /// <param name="ttlOverride">Optional time-to-live override</param>
        ValueTask Set<T>(string key, T val, TimeSpan? ttlOverride = null);

        /// <summary>
        /// Delete a key from the cache
        /// </summary>
        /// <param name="key">Cache key to delete</param>
        /// <returns>True if the key was deleted, false otherwise</returns>
        ValueTask<bool> Delete(string key);

        /// <summary>
        /// Delete all keys not present in the provided enumeration
        /// </summary>
        /// <param name="keys">Keys to keep</param>
        /// <param name="scanPattern">
        /// Optional glob (appended to the provider's key prefix) restricting which existing keys
        /// are considered for deletion. When null, every key under the provider's prefix is scanned,
        /// which can wipe sibling caches that share the same prefix and Redis DB. Callers should pass
        /// the narrowest pattern they own (e.g. "schematic:flags:*") to scope the scan.
        /// </param>
        ValueTask DeleteMissing(IEnumerable<string> keys, string? scanPattern = null);
    }
}
