namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// A cache that can overwrite a value while leaving the key's current expiry alone.
    /// </summary>
    internal interface IKeepTtlCacheProvider
    {
        /// <summary>
        /// Overwrite the value at <paramref name="key"/> and keep its existing expiry. A key
        /// with no expiry keeps none, and a key that did not exist is written with no expiry.
        /// </summary>
        ValueTask SetKeepTtl<T>(string key, T val, CancellationToken token = default) where T : notnull;
    }
}
