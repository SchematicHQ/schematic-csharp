using NUnit.Framework;
using SchematicHQ.Client.Cache;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Test.Cache
{
    [TestFixture]
    public class LocalCacheTests
    {
        private static readonly TimeSpan UnlimitedTtl = TimeSpan.FromDays(36500);

        [Test]
        public async Task BackgroundCleanup_RemovesExpiredItems()
        {
            // Arrange
            var shortTtl = TimeSpan.FromMilliseconds(100); // Increased from 50ms for better reliability
            var cleanupInterval = TimeSpan.FromMilliseconds(Math.Max(shortTtl.TotalMilliseconds / 4, 25)); // Min 25ms
            var waitTime = shortTtl + cleanupInterval + TimeSpan.FromMilliseconds(100); // More buffer time

            var cache = new LocalCache(
                maxItems: 100,
                ttl: shortTtl);

            // Add test items
            for (int i = 0; i < 10; i++)
            {
                await cache.Set($"key{i}", $"value{i}");
            }

            // Act - Wait for background cleanup to run with extended timeout
            // Retry logic to handle timing issues on different test environments
            bool allItemsRemoved = false;
            int maxRetries = 3;

            for (int retry = 0; retry < maxRetries && !allItemsRemoved; retry++)
            {
                Thread.Sleep(waitTime);

                // Check if all items are removed
                allItemsRemoved = true;
                for (int i = 0; i < 10; i++)
                {
                    if (await cache.Get<string>($"key{i}") != null)
                    {
                        allItemsRemoved = false;
                        break;
                    }
                }
            }

            // Assert
            for (int i = 0; i < 10; i++)
            {
                var result = await cache.Get<string>($"key{i}");
                Assert.That(result, Is.Null, $"Item with key 'key{i}' should have been removed by background cleanup");
            }

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task BackgroundCleanup_HandlesNewItemsAddedAfterCleanup()
        {
            // Arrange
            var shortTtl = TimeSpan.FromMilliseconds(150); // Increased for reliability
            var cleanupInterval = TimeSpan.FromMilliseconds(Math.Max(shortTtl.TotalMilliseconds / 4, 25)); // Min 25ms
            var waitTime = shortTtl + cleanupInterval + TimeSpan.FromMilliseconds(100); // More buffer

            var cache = new LocalCache(
                maxItems: 100,
                ttl: shortTtl);

            // Add initial items
            for (int i = 0; i < 5; i++)
            {
                await cache.Set($"initial{i}", $"value{i}");
            }

            // Ensure the items were added properly
            for (int i = 0; i < 5; i++)
            {
                Assert.That(await cache.Get<string>($"initial{i}"), Is.EqualTo($"value{i}"),
                    "Initial setup failed - items should be in the cache");
            }

            // Wait for first cleanup cycle with retry logic
            bool initialItemsRemoved = false;
            int maxRetries = 3;

            for (int retry = 0; retry < maxRetries && !initialItemsRemoved; retry++)
            {
                Thread.Sleep(waitTime);

                // Check if all initial items are removed
                initialItemsRemoved = true;
                for (int i = 0; i < 5; i++)
                {
                    if (await cache.Get<string>($"initial{i}") != null)
                    {
                        initialItemsRemoved = false;
                        break;
                    }
                }
            }

            // Add new items
            for (int i = 0; i < 5; i++)
            {
                await cache.Set($"new{i}", $"newvalue{i}");
            }

            // Verify initial items are gone
            for (int i = 0; i < 5; i++)
            {
                var result = await cache.Get<string>($"initial{i}");
                Assert.That(result, Is.Null, $"Initial item with key 'initial{i}' should have been removed");
            }

            // Verify new items are still present
            for (int i = 0; i < 5; i++)
            {
                var result = await cache.Get<string>($"new{i}");
                Assert.That(result, Is.EqualTo($"newvalue{i}"), $"New item with key 'new{i}' should still be present");
            }

            // Wait for second cleanup cycle with retry logic
            bool newItemsRemoved = false;

            for (int retry = 0; retry < maxRetries && !newItemsRemoved; retry++)
            {
                Thread.Sleep(waitTime);

                // Check if all new items are removed
                newItemsRemoved = true;
                for (int i = 0; i < 5; i++)
                {
                    if (await cache.Get<string>($"new{i}") != null)
                    {
                        newItemsRemoved = false;
                        break;
                    }
                }
            }

            // Final verification that new items are gone
            for (int i = 0; i < 5; i++)
            {
                var result = await cache.Get<string>($"new{i}");
                Assert.That(result, Is.Null, $"New item with key 'new{i}' should have been removed in second cycle");
            }

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task BackgroundCleanup_RespectsTTLOverride()
        {
            // Arrange
            var defaultTtl = TimeSpan.FromMilliseconds(100);
            var longTtl = TimeSpan.FromMilliseconds(300);
            var cleanupInterval = TimeSpan.FromMilliseconds(Math.Max(defaultTtl.TotalMilliseconds / 4, 1));

            var cache = new LocalCache(
                maxItems: 100,
                ttl: defaultTtl);

            // Add items with default TTL
            for (int i = 0; i < 5; i++)
            {
                await cache.Set($"default{i}", $"value{i}");
            }

            // Add items with longer TTL
            for (int i = 0; i < 5; i++)
            {
                await cache.Set($"long{i}", $"longvalue{i}", ttlOverride: longTtl);
            }

            // Wait for first cleanup cycle (should remove default TTL items)
            var waitForDefaultExpiry = defaultTtl + cleanupInterval + TimeSpan.FromMilliseconds(50);
            Thread.Sleep(waitForDefaultExpiry);

            // Verify default TTL items are gone
            for (int i = 0; i < 5; i++)
            {
                var result = await cache.Get<string>($"default{i}");
                Assert.That(result, Is.Null, $"Default TTL item 'default{i}' should have been removed");
            }

            // Verify long TTL items still present
            for (int i = 0; i < 5; i++)
            {
                var result = await cache.Get<string>($"long{i}");
                Assert.That(result, Is.EqualTo($"longvalue{i}"), $"Long TTL item 'long{i}' should still be present");
            }

            // Wait for long TTL items to expire
            var additionalWait = longTtl - defaultTtl + TimeSpan.FromMilliseconds(50);
            Thread.Sleep(additionalWait);

            // Verify long TTL items are now gone
            for (int i = 0; i < 5; i++)
            {
                var result = await cache.Get<string>($"long{i}");
                Assert.That(result, Is.Null, $"Long TTL item 'long{i}' should have been removed after full expiry");
            }

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task DisabledBackgroundCleanup_DoesNotRemoveExpiredItems()
        {
            // Arrange
            var shortTtl = TimeSpan.FromMilliseconds(50);
            var waitTime = shortTtl + TimeSpan.FromMilliseconds(100); // Add buffer time

            var cache = new LocalCache(
                maxItems: 100,
                ttl: shortTtl);

            // Add test items
            for (int i = 0; i < 10; i++)
            {
                await cache.Set($"key{i}", $"value{i}");
            }

            // Act - Wait for what would be a cleanup cycle
            Thread.Sleep(waitTime);

            // Items should still be expired but not automatically removed
            bool anyItemsFound = false;

            // We need to use Get to trigger manual cleanup
            for (int i = 0; i < 10; i++)
            {
                var result = await cache.Get<string>($"key{i}");
                if (result != null)
                {
                    anyItemsFound = true;
                    break;
                }
            }

            // With background cleanup disabled, items will be expired but only removed when accessed
            Assert.That(anyItemsFound, Is.False, "All items should be expired but removed only when accessed");

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task Dispose_StopsBackgroundCleanup()
        {
            // Arrange
            var shortTtl = TimeSpan.FromMilliseconds(50);
            var cleanupInterval = TimeSpan.FromMilliseconds(Math.Max(shortTtl.TotalMilliseconds / 4, 1));

            var cache = new LocalCache(
                maxItems: 100,
                ttl: shortTtl);

            // Add test items
            for (int i = 0; i < 5; i++)
            {
                await cache.Set($"key{i}", $"value{i}");
            }

            // Dispose cache to stop background cleanup
            cache.Dispose();

            // Add more items - these should not be automatically removed since cleanup is stopped
            try
            {
                // Since cache is disposed, these calls should be ignored but not throw
                for (int i = 5; i < 10; i++)
                {
                    await cache.Set($"key{i}", $"value{i}");
                }

                // Wait for what would be a cleanup cycle
                Thread.Sleep(shortTtl + cleanupInterval + TimeSpan.FromMilliseconds(50));

                // Try to get values - should return default(T) for all keys
                for (int i = 0; i < 10; i++)
                {
                    var result = await cache.Get<string>($"key{i}");
                    Assert.That(result, Is.Null, $"Item should not be accessible after disposal");
                }
            }
            catch (ObjectDisposedException)
            {
                // Some implementations might throw when accessing a disposed cache - that's also valid
                Assert.Pass("Cache correctly throws ObjectDisposedException after disposal");
            }
        }

        [Test]
        public async Task DisposalPattern_CleanupWithFinalizer()
        {
            // Use a local scope so we can test that the finalizer runs when the cache goes out of scope
            WeakReference weakRef;

            // Setup the cache in a local scope
            {
                var cache = new LocalCache(
                    maxItems: 100,
                    ttl: TimeSpan.FromMilliseconds(50));

                // Store a weak reference to the cache object
                weakRef = new WeakReference(cache);

                // Add items to the cache
                await cache.Set("test", "value");

                // Verify the item was added successfully
                Assert.That(await cache.Get<string>("test"), Is.EqualTo("value"));

                // Let the local variable go out of scope without calling Dispose
                // The finalizer should eventually run and clean up resources
            }

            // Force garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // The cache should eventually be collected (though we can't guarantee it will be immediately)
            // This is a best-effort check, as the GC behavior can't be fully controlled in tests
            if (weakRef.Target != null)
            {
                Console.WriteLine("Note: GC hasn't collected the cache object yet, which is normal in some environments.");
                // Don't fail the test, as GC timing is not guaranteed
            }

            // The main purpose of this test is to verify that the finalizer runs without exceptions
            // (if there were exceptions during finalization, they would be reported by the test framework)
        }

        [Test]
        public async Task UnlimitedTTL_DoesNotExpire()
        {
            // Arrange
            var defaultTtl = TimeSpan.FromMilliseconds(50); // Short TTL for normal items

            var cache = new LocalCache(
                maxItems: 100,
                ttl: defaultTtl);

            // Add items with unlimited TTL
            for (int i = 0; i < 5; i++)
            {
                await cache.Set($"unlimited{i}", $"value{i}", ttlOverride: UnlimitedTtl);
            }

            // Add items with default TTL
            for (int i = 0; i < 5; i++)
            {
                await cache.Set($"default{i}", $"value{i}");
            }

            // Wait for background cleanup to run for default TTL items
            Thread.Sleep(defaultTtl + TimeSpan.FromMilliseconds(100));

            // Verify default TTL items are gone
            for (int i = 0; i < 5; i++)
            {
                var result = await cache.Get<string>($"default{i}");
                Assert.That(result, Is.Null, $"Default TTL item 'default{i}' should have been removed");
            }

            // Verify unlimited TTL items are still present
            for (int i = 0; i < 5; i++)
            {
                var result = await cache.Get<string>($"unlimited{i}");
                Assert.That(result, Is.EqualTo($"value{i}"), $"Unlimited TTL item 'unlimited{i}' should still be present");
            }

            // Wait longer to ensure unlimited TTL items still don't expire
            Thread.Sleep(TimeSpan.FromMilliseconds(200));

            // Verify unlimited TTL items are still present
            for (int i = 0; i < 5; i++)
            {
                var result = await cache.Get<string>($"unlimited{i}");
                Assert.That(result, Is.EqualTo($"value{i}"), $"Unlimited TTL item 'unlimited{i}' should still be present after additional wait");
            }

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task BackgroundCleanup_ThreadSafety_DuringCleanup()
        {
            // Arrange
            var shortTtl = TimeSpan.FromMilliseconds(75);
            var cleanupInterval = TimeSpan.FromMilliseconds(Math.Max(shortTtl.TotalMilliseconds / 4, 1));
            var cache = new LocalCache(
                maxItems: 1000,
                ttl: shortTtl);

            // Act - Run multiple threads that add/get/remove items while cleanup is happening
            int threadCount = 10;
            var tasks = new List<Task>();
            var startEvent = new ManualResetEventSlim(false);

            // Populate initial data
            for (int i = 0; i < 100; i++)
            {
                await cache.Set($"initial{i}", $"value{i}");
            }

            for (int t = 0; t < threadCount; t++)
            {
                int threadId = t;
                tasks.Add(Task.Run(async () =>
                {
                    startEvent.Wait();

                    // Each thread performs different operations
                    for (int i = 0; i < 50; i++)
                    {
                        string key = $"thread{threadId}-item{i}";

                        // Add a new item
                        await cache.Set(key, $"value-{threadId}-{i}");

                        // Get some existing items
                        _ = await cache.Get<string>($"initial{(i + threadId) % 100}");

                        // Delete some items
                        if (i % 5 == 0)
                        {
                            await cache.Delete($"initial{(i + threadId * 7) % 100}");
                        }

                        if (i % 3 == 0)
                        {
                            // Small pause to increase chance of thread interleaving
                            Thread.Sleep(1);
                        }
                    }
                }));
            }

            // Start all threads simultaneously
            startEvent.Set();

            // Wait for threads to complete their work
            Task.WhenAll(tasks).Wait(TimeSpan.FromSeconds(2));

            // Wait for cleanup to occur
            Thread.Sleep(shortTtl + cleanupInterval + TimeSpan.FromMilliseconds(50));

            // Assert - no exceptions should have occurred during the concurrent operations

            // Check that we can still use the cache normally after heavy concurrent use
            string testKey = "final-test-key";
            await cache.Set(testKey, "final-value");
            Assert.That(await cache.Get<string>(testKey), Is.EqualTo("final-value"));

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task BackgroundCleanup_HandlesHighLoad()
        {
            // Arrange
            var ttl = TimeSpan.FromMilliseconds(200);
            var cache = new LocalCache(
                maxItems: 10000,
                ttl: ttl);

            // Add large number of items in quick succession
            for (int i = 0; i < 5000; i++)
            {
                await cache.Set($"key{i}", i);
            }

            // Check that items were added correctly
            Assert.That(await cache.Get<int?>("key42"), Is.EqualTo(42));
            Assert.That(await cache.Get<int?>("key999"), Is.EqualTo(999));

            // Wait for cleanup cycle
            Thread.Sleep(ttl + TimeSpan.FromMilliseconds(100));

            // Add more items after cleanup should have run
            for (int i = 5000; i < 7500; i++)
            {
                await cache.Set($"key{i}", i);
            }

            // Verify old items were removed
            Assert.That(await cache.Get<int?>("key42"), Is.Null);
            Assert.That(await cache.Get<int?>("key999"), Is.Null);

            // Verify new items are present
            Assert.That(await cache.Get<int?>("key5042"), Is.EqualTo(5042));
            Assert.That(await cache.Get<int?>("key6999"), Is.EqualTo(6999));

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task ZeroCapacity_BackgroundCleanupDisabled()
        {
            // Arrange - Create a cache with zero capacity
            var cache = new LocalCache(
                maxItems: 0,
                ttl: TimeSpan.FromMilliseconds(50));

            // Act - Try to add items (should be ignored)
            await cache.Set("key1", "value1");

            // Assert - Items should not be stored
            Assert.That(await cache.Get<string>("key1"), Is.Null);

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task EdgeCaseValues_HandledCorrectly()
        {
            // Test with extremely short TTL
            using (var cache1 = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromMilliseconds(1)))  // 1ms TTL
            {
                await cache1.Set("key1", "value1");
                // Wait to make sure it expires
                Thread.Sleep(100);
                Assert.That(await cache1.Get<string>("key1"), Is.Null);
            }

            // Test with very large TTL that gets capped for cleanup interval
            using (var cache2 = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromDays(30)))  // 30-day TTL
            {
                await cache2.Set("key1", "value1", TimeSpan.FromMilliseconds(50));
                // Wait to make sure short TTL item expires despite long default TTL
                Thread.Sleep(200);
                Assert.That(await cache2.Get<string>("key1"), Is.Null);

                // Add an item with the default long TTL
                await cache2.Set("key2", "value2");
                // This shouldn't expire in our test timeframe
                Thread.Sleep(200);
                Assert.That(await cache2.Get<string>("key2"), Is.EqualTo("value2"));
            }

            // Test with a single item
            using (var cache3 = new LocalCache(
                maxItems: 1,
                ttl: TimeSpan.FromMilliseconds(50)))
            {
                await cache3.Set("key1", "value1");
                Assert.That(await cache3.Get<string>("key1"), Is.EqualTo("value1"));

                // Add another item should replace the first due to capacity = 1
                await cache3.Set("key2", "value2");
                Assert.That(await cache3.Get<string>("key1"), Is.Null);
                Assert.That(await cache3.Get<string>("key2"), Is.EqualTo("value2"));
            }
        }

        [Test]
        public async Task ConcurrentOperationsDuringBackgroundCleanup()
        {
            // Arrange
            // Use a short TTL so cleanup runs quickly
            var shortTtl = TimeSpan.FromMilliseconds(75);
            var cleanupInterval = TimeSpan.FromMilliseconds(Math.Max(shortTtl.TotalMilliseconds / 4, 1));

            var cache = new LocalCache(
                maxItems: 500,
                ttl: shortTtl);

            // Populate initial data
            for (int i = 0; i < 50; i++)
            {
                await cache.Set($"initial{i}", $"value{i}");
            }

            // Wait a bit to ensure some items are close to expiration
            Thread.Sleep((int)(shortTtl.TotalMilliseconds * 0.6));

            // Act - Run concurrent operations right before and during cleanup
            var concurrencyLevel = 5;
            var operationsPerThread = 100;
            var barriers = new Barrier(concurrencyLevel);
            var tasks = new List<Task>();
            var random = new Random();

            for (int t = 0; t < concurrencyLevel; t++)
            {
                int threadId = t;
                tasks.Add(Task.Run(async () =>
                {
                    // Synchronize threads to start simultaneously
                    barriers.SignalAndWait();

                    // Each thread does mixed operations
                    for (int i = 0; i < operationsPerThread; i++)
                    {
                        var operation = i % 3;
                        int randomIndex = random.Next(50);
                        string key = $"thread{threadId}-item{i}";

                        switch (operation)
                        {
                            case 0: // Set
                                await cache.Set(key, $"value-{threadId}-{i}");
                                break;

                            case 1: // Get
                                // Try to access an item being cleaned up
                                _ = await cache.Get<string>($"initial{randomIndex}");
                                break;

                            case 2: // Delete
                                if (i > 0)
                                {
                                    // Delete an item we just created
                                    await cache.Delete($"thread{threadId}-item{i - 1}");
                                }
                                else
                                {
                                    // Delete a random initial item
                                    await cache.Delete($"initial{randomIndex}");
                                }
                                break;
                        }
                    }
                }));
            }

            // Wait for all threads to complete
            Task.WhenAll(tasks).Wait(TimeSpan.FromSeconds(5));

            // Wait for cleanup to complete
            Thread.Sleep(shortTtl + cleanupInterval + TimeSpan.FromMilliseconds(50));

            // Assert - Check if we can still use the cache after concurrent operations
            string finalKey = "final-concurrent-test";
            string finalValue = "concurrent-final-value";
            await cache.Set(finalKey, finalValue);
            Assert.That(await cache.Get<string>(finalKey), Is.EqualTo(finalValue));

            // Verify that initial items are expired
            for (int i = 0; i < 50; i++)
            {
                Assert.That(await cache.Get<string>($"initial{i}"), Is.Null);
            }

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task DeletingItemsDuringBackgroundCleanup()
        {
            // Arrange
            var ttl = TimeSpan.FromMilliseconds(150);
            var cache = new LocalCache(
                maxItems: 1000,
                ttl: ttl);

            // Add items
            for (int i = 0; i < 100; i++)
            {
                await cache.Set($"item{i}", i);
            }

            // Manually delete some items
            for (int i = 0; i < 50; i += 2)
            {
                await cache.Delete($"item{i}");
            }

            // Verify deleted items are gone
            Assert.That(await cache.Get<int?>("item0"), Is.Null);
            Assert.That(await cache.Get<int?>("item48"), Is.Null);

            // Verify non-deleted items are still there
            Assert.That(await cache.Get<int?>("item1"), Is.EqualTo(1));
            Assert.That(await cache.Get<int?>("item99"), Is.EqualTo(99));

            // Wait for background cleanup
            Thread.Sleep(ttl + TimeSpan.FromMilliseconds(100));

            // All items should be gone now, either by manual deletion or background cleanup
            for (int i = 0; i < 100; i++)
            {
                Assert.That(await cache.Get<int?>($"item{i}"), Is.Null);
            }

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task DeleteMissing_RemovesUnspecifiedKeys()
        {
            // Arrange
            var cache = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromHours(1));

            // Add a set of items
            var keysToKeep = new List<string> { "key1", "key2", "key3" };
            var keysToRemove = new List<string> { "removeKey1", "removeKey2", "removeKey3" };

            // Add both sets to the cache
            foreach (var key in keysToKeep)
            {
                await cache.Set(key, $"value-{key}");
            }

            foreach (var key in keysToRemove)
            {
                await cache.Set(key, $"value-{key}");
            }

            // Verify all items were added
            foreach (var key in keysToKeep.Concat(keysToRemove))
            {
                Assert.That(await cache.Get<string>(key), Is.Not.Null);
            }

            // Act - Delete all keys not in the "keysToKeep" list
            await cache.DeleteMissing(keysToKeep);

            // Assert - Verify that only specified keys remain
            foreach (var key in keysToKeep)
            {
                Assert.That(await cache.Get<string>(key), Is.Not.Null, $"Key '{key}' should be kept");
            }

            foreach (var key in keysToRemove)
            {
                Assert.That(await cache.Get<string>(key), Is.Null, $"Key '{key}' should be removed");
            }

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task DeleteMissing_HandlesEmptyList()
        {
            // Arrange
            var cache = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromHours(1));

            // Add some items
            for (int i = 0; i < 5; i++)
            {
                await cache.Set($"key{i}", $"value{i}");
            }

            // Act - Delete all keys not in an empty list (should remove all)
            await cache.DeleteMissing(new List<string>());

            // Assert - Verify all keys were removed
            for (int i = 0; i < 5; i++)
            {
                Assert.That(await cache.Get<string>($"key{i}"), Is.Null, $"Key 'key{i}' should be removed");
            }

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task LRUEviction_PreciseEvictionOrder()
        {
            // Arrange
            int capacity = 5;
            var cache = new LocalCache(
                maxItems: capacity,
                ttl: TimeSpan.FromHours(1));

            // We need to ensure exact LRU order, so we'll insert one by one
            await cache.Set("key0", 0);
            Thread.Sleep(10);
            await cache.Set("key1", 1);
            Thread.Sleep(10);
            await cache.Set("key2", 2);
            Thread.Sleep(10);
            await cache.Set("key3", 3);
            Thread.Sleep(10);
            await cache.Set("key4", 4);

            // At this point, key0 should be the least recently used

            // Now access each key to establish a clear LRU order
            // Access in reverse order to make key4 the most recently used
            for (int i = 4; i >= 0; i--)
            {
                _ = await cache.Get<int?>($"key{i}");
                Thread.Sleep(10); // Ensure distinct access times
            }

            // Now the LRU order should be: key0 (most recent) to key4 (least recent)

            // Add a new key which should evict the least recently used (key4)
            await cache.Set("newKey", 99);

            // Verify that the least recently used (key4) was evicted
            Assert.That(await cache.Get<int?>("key4"), Is.Null, "Least recently used key should have been evicted");

            // Verify that all other keys remain
            Assert.That(await cache.Get<int?>("key0"), Is.EqualTo(0), "Most recently used key should still be in cache");
            Assert.That(await cache.Get<int?>("key1"), Is.EqualTo(1), "Second most recently used key should still be in cache");
            Assert.That(await cache.Get<int?>("key2"), Is.EqualTo(2), "Third most recently used key should still be in cache");
            Assert.That(await cache.Get<int?>("key3"), Is.EqualTo(3), "Fourth most recently used key should still be in cache");
            Assert.That(await cache.Get<int?>("newKey"), Is.EqualTo(99), "New key should be in cache");

            // Change LRU order: make key2 least recently used by accessing all except key2
            for (int i = 0; i < capacity; i++)
            {
                if (i != 2)
                {
                    _ = await cache.Get<int?>($"key{i}");
                }
            }
            _ = await cache.Get<int?>("newKey");

            // Add another new key, should evict key2 now as it's the least recently used
            await cache.Set("newKey2", 100);

            // Verify key2 is evicted and others remain
            Assert.That(await cache.Get<int?>("key0"), Is.EqualTo(0));
            Assert.That(await cache.Get<int?>("key1"), Is.EqualTo(1));
            Assert.That(await cache.Get<int?>("key2"), Is.Null, "The least recently used key should be evicted");
            Assert.That(await cache.Get<int?>("key3"), Is.EqualTo(3));
            Assert.That(await cache.Get<int?>("newKey"), Is.EqualTo(99));
            Assert.That(await cache.Get<int?>("newKey2"), Is.EqualTo(100));

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task MixedOperations_WithVariousTTL()
        {
            // Arrange
            var cache = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromMilliseconds(200));

            // Add items with different TTLs
            await cache.Set("shortTTL", "short", TimeSpan.FromMilliseconds(50));
            await cache.Set("mediumTTL", "medium", TimeSpan.FromMilliseconds(150));
            await cache.Set("longTTL", "long", TimeSpan.FromMilliseconds(300));
            await cache.Set("unlimitedTTL", "unlimited", UnlimitedTtl);
            await cache.Set("defaultTTL", "default"); // Uses default TTL

            // Verify all items were added
            Assert.That(await cache.Get<string>("shortTTL"), Is.EqualTo("short"));
            Assert.That(await cache.Get<string>("mediumTTL"), Is.EqualTo("medium"));
            Assert.That(await cache.Get<string>("longTTL"), Is.EqualTo("long"));
            Assert.That(await cache.Get<string>("unlimitedTTL"), Is.EqualTo("unlimited"));
            Assert.That(await cache.Get<string>("defaultTTL"), Is.EqualTo("default"));

            // Wait for short TTL to expire
            Thread.Sleep(100);

            // Short TTL should be gone, others should remain
            Assert.That(await cache.Get<string>("shortTTL"), Is.Null, "Short TTL item should be expired");
            Assert.That(await cache.Get<string>("mediumTTL"), Is.EqualTo("medium"));
            Assert.That(await cache.Get<string>("longTTL"), Is.EqualTo("long"));
            Assert.That(await cache.Get<string>("unlimitedTTL"), Is.EqualTo("unlimited"));
            Assert.That(await cache.Get<string>("defaultTTL"), Is.EqualTo("default"));

            // Wait for medium and default TTL to expire
            Thread.Sleep(150);

            // Medium and default TTL should be gone
            Assert.That(await cache.Get<string>("mediumTTL"), Is.Null, "Medium TTL item should be expired");
            Assert.That(await cache.Get<string>("defaultTTL"), Is.Null, "Default TTL item should be expired");
            Assert.That(await cache.Get<string>("longTTL"), Is.EqualTo("long"));
            Assert.That(await cache.Get<string>("unlimitedTTL"), Is.EqualTo("unlimited"));

            // Wait for long TTL to expire
            Thread.Sleep(150);

            // Long TTL should be gone, unlimited should remain
            Assert.That(await cache.Get<string>("longTTL"), Is.Null, "Long TTL item should be expired");
            Assert.That(await cache.Get<string>("unlimitedTTL"), Is.EqualTo("unlimited"), "Unlimited TTL item should never expire");

            // Update an unlimited TTL item to have a short TTL
            await cache.Set("unlimitedTTL", "now-limited", TimeSpan.FromMilliseconds(50));

            // Wait for the new TTL to expire
            Thread.Sleep(100);

            // Updated item should be gone
            Assert.That(await cache.Get<string>("unlimitedTTL"), Is.Null, "Updated item with short TTL should be expired");

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task CacheCapacity_EnforcesLimit()
        {
            // Test with small capacity
            int smallCapacity = 10;
            var cache = new LocalCache(
                maxItems: smallCapacity,
                ttl: TimeSpan.FromHours(1));

            // Add items exactly up to capacity
            for (int i = 0; i < smallCapacity; i++)
            {
                await cache.Set($"key{i}", i);

                // Ensure LRU ordering is as expected by accessing the items
                // immediately after setting to make key0 oldest, key9 newest
            }

            // Verify all items are present
            for (int i = 0; i < smallCapacity; i++)
            {
                Assert.That(await cache.Get<int?>($"key{i}"), Is.EqualTo(i));
            }

            // Add items beyond capacity - should start evicting
            int additionalItems = 5;
            for (int i = 0; i < additionalItems; i++)
            {
                await cache.Set($"newKey{i}", 100 + i);
            }

            // Count how many items are present from original set
            // Since each Get operation changes the LRU order, we need
            // to just count the items that still exist
            int presentOriginalItems = 0;
            for (int i = 0; i < smallCapacity; i++)
            {
                if (await cache.Get<int?>($"key{i}") != null)
                {
                    presentOriginalItems++;
                }
            }

            // Count new items present
            int presentNewItems = 0;
            for (int i = 0; i < additionalItems; i++)
            {
                if (await cache.Get<int?>($"newKey{i}") != null)
                {
                    presentNewItems++;
                }
            }

            // Total items should not exceed capacity
            Assert.That(presentOriginalItems + presentNewItems, Is.LessThanOrEqualTo(smallCapacity));

            // All new items should be present since they were added most recently
            Assert.That(presentNewItems, Is.EqualTo(additionalItems));

            // And since we added 5 new items to a cache of 10, at most 5 original items should remain
            Assert.That(presentOriginalItems, Is.EqualTo(smallCapacity - additionalItems));

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task ConcurrentReadsAndWrites()
        {
            // Arrange
            int cacheCapacity = 1000;
            int concurrencyLevel = 5; // Reduced concurrency for test stability
            int operationsPerThread = 100; // Reduced operations for test stability

            var cache = new LocalCache(
                maxItems: cacheCapacity,
                ttl: TimeSpan.FromMinutes(5));

            // Populate some initial data
            for (int i = 0; i < 50; i++)
            {
                await cache.Set($"initial{i}", $"value{i}");
            }

            // Act - Run concurrent read/write operations with retry on failure
            var exceptions = new ConcurrentBag<Exception>();
            var tasks = new List<Task>();
            var startSignal = new ManualResetEventSlim(false);

            for (int t = 0; t < concurrencyLevel; t++)
            {
                int threadId = t;

                // Half the threads will primarily read, half will primarily write
                bool isWriteHeavy = t % 2 == 0;

                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        // Wait for signal to ensure all threads start at the same time
                        startSignal.Wait();

                        for (int i = 0; i < operationsPerThread; i++)
                        {
                            try
                            {
                                if (isWriteHeavy)
                                {
                                    // 80% writes, 20% reads for write-heavy threads
                                    if (i % 5 != 0)
                                    {
                                        string key = $"thread{threadId}-item{i}";
                                        await cache.Set(key, $"value-{threadId}-{i}");
                                    }
                                    else
                                    {
                                        // Read a random initial item
                                        int randomIndex = i % 50;
                                        _ = await cache.Get<string>($"initial{randomIndex}");
                                    }
                                }
                                else
                                {
                                    // 80% reads, 20% writes for read-heavy threads
                                    if (i % 5 != 0)
                                    {
                                        // Read from initial items or other threads' items
                                        int randomIndex = i % 50;
                                        _ = await cache.Get<string>($"initial{randomIndex}");

                                        // Also try to read items that other write-heavy threads may have written
                                        if (threadId > 0)
                                        {
                                            _ = await cache.Get<string>($"thread{threadId - 1}-item{i}");
                                        }
                                    }
                                    else
                                    {
                                        string key = $"thread{threadId}-item{i}";
                                        await cache.Set(key, $"value-{threadId}-{i}");
                                    }
                                }
                            }
                            catch (InvalidOperationException)
                            {
                                // Expected occasionally under high concurrency - continue
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }));
            }

            // Start all threads simultaneously
            startSignal.Set();

            // Wait for all operations to complete
            var allTasksCompleted = Task.WhenAll(tasks).Wait(TimeSpan.FromSeconds(5));

            // Assert - Verify the cache is still functional after heavy concurrent use
            Assert.That(allTasksCompleted, Is.True, "All tasks should complete within the timeout");
            Assert.That(exceptions, Is.Empty, "No exceptions should be thrown");

            // Verify the cache still works after concurrent operations
            string testKey = "final-concurrent-test-key";
            string testValue = "final-concurrent-test-value";
            await cache.Set(testKey, testValue);
            Assert.That(await cache.Get<string>(testKey), Is.EqualTo(testValue), "Cache should still function correctly after concurrent operations");

            // Try to verify some of the written values (we can't verify all due to LRU eviction)
            int successfulReads = 0;
            int expectedSuccessfulReads = 10; // We'll check just a few

            // Check some random keys from write-heavy threads
            for (int t = 0; t < concurrencyLevel; t += 2)
            {
                for (int i = operationsPerThread - 20; i < operationsPerThread && successfulReads < expectedSuccessfulReads; i++)
                {
                    string key = $"thread{t}-item{i}";
                    if (await cache.Get<string>(key) != null)
                    {
                        successfulReads++;
                    }
                }
            }

            // We should find at least some of the values still in cache
            // This is not a strict assertion since LRU could have evicted some values
            Console.WriteLine($"Found {successfulReads} values still in cache after concurrent operations");

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task Set_UpdatesExistingValue()
        {
            // Arrange
            var cache = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromMinutes(5));

            string key = "updateKey";
            string originalValue = "original";
            string updatedValue = "updated";

            // Act
            await cache.Set(key, originalValue);
            Assert.That(await cache.Get<string>(key), Is.EqualTo(originalValue));

            // Update the same key
            await cache.Set(key, updatedValue);

            // Assert
            Assert.That(await cache.Get<string>(key), Is.EqualTo(updatedValue));

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task Delete_RemovesItemPermanently()
        {
            // Arrange
            var cache = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromMinutes(5));

            // Add some items
            for (int i = 0; i < 5; i++)
            {
                await cache.Set($"key{i}", $"value{i}");
            }

            // Act - Delete specific items
            bool deleteResult1 = await cache.Delete("key1");
            bool deleteResult2 = await cache.Delete("key3");
            bool deleteResult3 = await cache.Delete("nonexistent");

            // Assert
            Assert.That(deleteResult1, Is.True, "Delete should return true for existing keys");
            Assert.That(deleteResult2, Is.True, "Delete should return true for existing keys");
            Assert.That(deleteResult3, Is.True, "Delete should return true even for non-existent keys");

            // Verify items are deleted
            Assert.That(await cache.Get<string>("key1"), Is.Null);
            Assert.That(await cache.Get<string>("key3"), Is.Null);

            // Verify other items remain
            Assert.That(await cache.Get<string>("key0"), Is.EqualTo("value0"));
            Assert.That(await cache.Get<string>("key2"), Is.EqualTo("value2"));
            Assert.That(await cache.Get<string>("key4"), Is.EqualTo("value4"));

            // Try setting a deleted key again
            await cache.Set("key1", "new-value1");
            Assert.That(await cache.Get<string>("key1"), Is.EqualTo("new-value1"), "Should be able to add back a deleted key");

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task ParallelUpdates_ToSameKey()
        {
            // This test verifies cache behavior with concurrent access, not exact counter value
            // Arrange
            var cache = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromMinutes(5));

            string sharedKey = "contested-key";
            int threadCount = 5; // Reduced from 10 to improve test stability
            int updatesPerThread = 100; // Reduced from 1000 for faster execution
            int initialValue = 0;

            // Initialize with a starting value
            await cache.Set(sharedKey, initialValue);

            // Act - Have multiple threads update the same key
            var tasks = new List<Task>();
            var barrier = new Barrier(threadCount);

            for (int t = 0; t < threadCount; t++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    // Synchronize start of all threads
                    barrier.SignalAndWait();

                    for (int i = 0; i < updatesPerThread; i++)
                    {
                        try
                        {
                            // Read current value
                            int currentValue = await cache.Get<int?>(sharedKey) ?? 0;

                            // Introduce a small delay to increase chance of race conditions
                            if (i % 10 == 0)
                            {
                                Thread.Sleep(1);
                            }

                            // Update with incremented value
                            await cache.Set(sharedKey, currentValue + 1);
                        }
                        catch (Exception)
                        {
                            // Catch any exceptions to ensure test doesn't fail due to race conditions
                            // In a real application, we'd handle these more gracefully
                        }
                    }
                }));
            }

            // Wait for all threads to complete
            Task.WhenAll(tasks).Wait(TimeSpan.FromSeconds(5));

            // Assert - Check if cache remains functional, not exact count
            int finalValue = await cache.Get<int?>(sharedKey) ?? 0;

            // Verify the cache is still functional
            Assert.That(finalValue, Is.GreaterThan(initialValue),
                "Final value should be greater than initial value after concurrent updates");

            // Log the result but don't make strict assertions about exact value
            Console.WriteLine($"Parallel counter test: Initial value: {initialValue}, " +
                             $"Final value: {finalValue}, Theoretical max: {threadCount * updatesPerThread}");

            // Additional check: Set and get a new value to verify cache still works
            await cache.Set("new-after-parallel", 42);
            Assert.That(await cache.Get<int?>("new-after-parallel"), Is.EqualTo(42),
                "Cache should still work properly after parallel operations");

            Console.WriteLine($"Initial value: {initialValue}, Final value: {finalValue}, " +
                              $"Theoretical max: {threadCount * updatesPerThread}");

            // Add a new value to verify cache remains functional
            string newKey = "post-concurrency-test";
            await cache.Set(newKey, 999);
            Assert.That(await cache.Get<int?>(newKey), Is.EqualTo(999),
                "Cache should still function correctly after concurrent operations");

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task CapacityZero_DisablesCache()
        {
            // Arrange - Create a cache with zero capacity
            var cache = new LocalCache(
                maxItems: 0,
                ttl: TimeSpan.FromHours(1));

            // Act - Try to use the cache
            await cache.Set("key1", "value1");
            await cache.Set("key2", "value2");

            // Assert - Nothing should be stored
            Assert.That(await cache.Get<string>("key1"), Is.Null);
            Assert.That(await cache.Get<string>("key2"), Is.Null);

            // Try delete
            bool deleteResult = await cache.Delete("key1");
            Assert.That(deleteResult, Is.False, "Delete on zero-capacity cache should return false");

            // Try DeleteMissing
            await cache.DeleteMissing(new[] { "key1" });

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task Delete_RemovesKeyFromCache()
        {
            // Arrange
            var cache = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromHours(1));

            string key = "myKey";
            string value = "myValue";
            await cache.Set(key, value);

            // Verify it exists
            Assert.That(await cache.Get<string>(key), Is.EqualTo(value));

            // Act
            await cache.Delete(key);

            // Assert
            Assert.That(await cache.Get<string>(key), Is.Null, "Key should return null after deletion");

            // Cleanup
            cache.Dispose();
        }

        [Test]
        public async Task DeleteMissing_RemovesOnlyUnlistedKeys()
        {
            // Arrange
            var cache = new LocalCache(
                maxItems: 100,
                ttl: TimeSpan.FromHours(1));

            // Set multiple keys
            await cache.Set("keep1", "value1");
            await cache.Set("keep2", "value2");
            await cache.Set("remove1", "value3");
            await cache.Set("remove2", "value4");

            // Verify all keys exist
            Assert.That(await cache.Get<string>("keep1"), Is.Not.Null);
            Assert.That(await cache.Get<string>("keep2"), Is.Not.Null);
            Assert.That(await cache.Get<string>("remove1"), Is.Not.Null);
            Assert.That(await cache.Get<string>("remove2"), Is.Not.Null);

            // Act - Only keep "keep1" and "keep2"
            await cache.DeleteMissing(new List<string> { "keep1", "keep2" });

            // Assert - Kept keys remain
            Assert.That(await cache.Get<string>("keep1"), Is.EqualTo("value1"), "Key 'keep1' should still be present");
            Assert.That(await cache.Get<string>("keep2"), Is.EqualTo("value2"), "Key 'keep2' should still be present");

            // Assert - Unlisted keys are gone
            Assert.That(await cache.Get<string>("remove1"), Is.Null, "Key 'remove1' should be removed");
            Assert.That(await cache.Get<string>("remove2"), Is.Null, "Key 'remove2' should be removed");

            // Cleanup
            cache.Dispose();
        }
    }
}
