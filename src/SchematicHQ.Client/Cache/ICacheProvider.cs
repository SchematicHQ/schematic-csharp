#nullable enable

namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// Interface for cache providers used by SchematicHQ client
    /// </summary>
    /// <typeparam name="T">Type of values stored in the cache</typeparam>
    public interface ICacheProvider<T>
    {
        /// <summary>
        /// Get a value from the cache by key
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>The cached value or default if not found or expired</returns>
        T? Get(string key);

        /// <summary>
        /// Set a value in the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="val">Value to cache</param>
        /// <param name="ttlOverride">Optional time-to-live override</param>
        void Set(string key, T val, TimeSpan? ttlOverride = null);

        /// <summary>
        /// Delete a key from the cache
        /// </summary>
        /// <param name="key">Cache key to delete</param>
        /// <returns>True if the key was deleted, false otherwise</returns>
        bool Delete(string key);

        /// <summary>
        /// Delete all keys not present in the provided enumeration
        /// </summary>
        /// <param name="keys">Keys to keep</param>
        void DeleteMissing(IEnumerable<string> keys);
    }
}
