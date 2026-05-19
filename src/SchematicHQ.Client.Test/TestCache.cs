using NUnit.Framework;
using System.Data.SqlTypes;
using SchematicHQ.Client.Cache;

#nullable enable

namespace SchematicHQ.Client.Test
{
    [TestFixture]
    public class TestCache
    {
        static IEnumerable<TestCaseData> SeTAndGetTestCases
        {
            get
            {
                yield return new TestCaseData(new LocalCache(), true).SetName("TestSetAndGet_Boolean");
                yield return new TestCaseData(new LocalCache(), "test_string").SetName("TestSetAndGet_string");
                yield return new TestCaseData(new LocalCache(), new List<string> { "test_string1", "test_string2" }).SetName("TestSetAndGet_RefType");
            }
        }

        [Test, TestCaseSource(nameof(SeTAndGetTestCases))]
        public async Task Get_ReturnsSetValue<T>(ICacheProvider cache, T value)
        {
            string key = "test_key";

            await cache.Set(key: key, val: value);
            var result = await cache.Get<T>(key);

            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public async Task Test_DefaultTTL()
        {
            LocalCache cacheProvider = new LocalCache(maxItems: 1);
            bool? expectedResult = true;
            var key = "test_key";
            var defaultTtl = TimeSpan.FromMilliseconds(5000);

            await cacheProvider.Set(key: key, val: expectedResult);
            var existingResult = await cacheProvider.Get<bool?>(key);
            Thread.Sleep(defaultTtl + TimeSpan.FromMilliseconds(5));
            var evictedResult = await cacheProvider.Get<bool?>(key);

            Assert.That(existingResult, Is.EqualTo(expectedResult));
            Assert.That(evictedResult, Is.Null);
        }

        [Test]
        public async Task Test_DefaultCapacity()
        {
            LocalCache cacheProvider = new LocalCache(ttl: TimeSpan.FromMinutes(10));
            int? expectedResult = -1;
            var key = "test_key";

            await cacheProvider.Set(key: key, val: expectedResult);
            foreach (int i in Enumerable.Range(1, LocalCache.DEFAULT_CACHE_CAPACITY - 1))
            {
                await cacheProvider.Set(key: i.ToString(), val: i);

                Assert.That(await cacheProvider.Get<int?>(key: key), Is.EqualTo(expectedResult));
            }
            await cacheProvider.Set(key: "new_key", val: -2);
            var evictedResult = await cacheProvider.Get<int?>(1.ToString());

            Assert.That(await cacheProvider.Get<int?>(key: key), Is.EqualTo(expectedResult));
            Assert.That(await cacheProvider.Get<int?>(key: "new_key"), Is.EqualTo(-2));
            Assert.That(evictedResult, Is.Null);
        }

        [Test]
        public async Task Test_NotExistentKeyReturnsDefaultValue()
        {
            LocalCache cacheProvider = new LocalCache(maxItems: 1);
            Assert.That(await cacheProvider.Get<INullable>("non_existent_key"), Is.Null);
        }

        [Test]
        public void Test_ConcurrentAccess()
        {
            int numberOfThreads = 50;
            int cacheCapacity = 30;
            LocalCache cacheProvider = new LocalCache(maxItems: cacheCapacity, ttl: TimeSpan.FromHours(5));
            var tasks = new List<Task>();
            var countdownEvent = new CountdownEvent(1);

            for (int t = 0; t < numberOfThreads; t++)
            {
                int start = t * cacheCapacity + 1;
                int end = start + cacheCapacity - 1;

                tasks.Add(Task.Run(async () =>
                {
                    countdownEvent.Wait();
                    for (int i = start; i <= end; i++)
                    {
                        await cacheProvider.Set(i.ToString(), i);
                    }
                }));
            }

            countdownEvent.Signal();
            Task.WaitAll(tasks.ToArray());

            List<int> cacheHitsIndices = new List<int>();

            for (int i = 1; i <= numberOfThreads * cacheCapacity; i++)
            {
                if (cacheProvider.Get<int?>(i.ToString()).GetAwaiter().GetResult() == i)
                {
                    cacheHitsIndices.Add(i);
                }
            }

            // Verify the cache contains exactly the expected number of items
            Assert.That(cacheHitsIndices.Count, Is.EqualTo(cacheCapacity));
        }

        [Test]
        public void Test_TTLOverride()
        {
            LocalCache cacheProvider = new LocalCache(maxItems: 1000, ttl: TimeSpan.FromHours(5));
            var tasks = new List<Task>();
            var countdownEvent = new CountdownEvent(1);
            string key = "test_key";
            int expectedValue = 5;
            TimeSpan ttlOverride = TimeSpan.FromSeconds(3);

            tasks.Add(Task.Run(async () =>
            {
                countdownEvent.Wait();
                await cacheProvider.Set(key: key, val: expectedValue, ttlOverride: ttlOverride);
            }));
            tasks.Add(Task.Run(async () =>
            {
                countdownEvent.Wait();
                Thread.Sleep(1000);
                Assert.That(await cacheProvider.Get<int?>(key), Is.EqualTo(expectedValue));
            }));
            tasks.Add(Task.Run(async () =>
            {
                countdownEvent.Wait();
                Thread.Sleep(ttlOverride + TimeSpan.FromMilliseconds(1));
                Assert.That(await cacheProvider.Get<int?>(key), Is.Null);
            }));

            countdownEvent.Signal();
            Task.WaitAll(tasks.ToArray());
        }

        [Test]
        public async Task Test_EvictionByLastAccessed()
        {
            LocalCache cacheProvider = new LocalCache(maxItems: 10, ttl: TimeSpan.FromHours(5));
            foreach (var i in Enumerable.Range(1, 10))
            {
                await cacheProvider.Set(i.ToString(), i);
            }

            foreach (var i in Enumerable.Range(1, 10))
            {
                Assert.That(await cacheProvider.Get<int?>(i.ToString()), Is.EqualTo(i));
            }

            foreach (var I in Enumerable.Range(1, 10))
            {
                await cacheProvider.Set((I + 10).ToString(), -1);
                Assert.That(await cacheProvider.Get<int?>(I.ToString()), Is.Null);
            }
        }
    }
}
