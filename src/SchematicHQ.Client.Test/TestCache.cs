using NUnit.Framework;
using System;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Test
{
    [TestFixture]
    public class TestCache
    {
        [Test]
        public void TestCacheGetAndSet()
        {
            var cache = new LocalCache<string>(maxItems: 1000, ttl: 5000);

            cache.Set("key1", "value1");
            Assert.AreEqual("value1", cache.Get("key1"));

            cache.Set("key2", "value2", ttlOverride: 1);
            Assert.AreEqual("value2", cache.Get("key2"));

            // Wait for the TTL to expire
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();
            Assert.IsNull(cache.Get("key2"));
        }

        [Test]
        public void TestCacheEviction()
        {
            var cache = new LocalCache<string>(maxItems: 2, ttl: 5000);

            cache.Set("key1", "a");
            cache.Set("key2", "b");

            // Access key1, making it more recently used than key2
            Assert.AreEqual("a", cache.Get("key1"));

            // Adding a new key should evict the least recently used key,
            // which will now be key2
            cache.Set("key3", "c");

            Assert.IsNull(cache.Get("key2"));
            Assert.AreEqual("a", cache.Get("key1"));
            Assert.AreEqual("c", cache.Get("key3"));
        }

        [Test]
        public void TestCacheExpiration()
        {
            var cache = new LocalCache<string>(maxItems: 1000, ttl: 1000);

            cache.Set("key1", "value1");
            Assert.AreEqual("value1", cache.Get("key1"));

            // Wait for the TTL to expire
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();
            Assert.IsNull(cache.Get("key1"));
        }
    }
}
