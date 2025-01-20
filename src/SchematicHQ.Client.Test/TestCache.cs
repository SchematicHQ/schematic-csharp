using NUnit.Framework;
using System.Data.SqlTypes;

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
                yield return new TestCaseData(new LocalCache<bool>(), true).SetName("TestSetAndGet_Boolean");
                yield return new TestCaseData(new LocalCache<string>(), "test_string").SetName("TestSetAndGet_string");
                yield return new TestCaseData(new LocalCache<List<string>>(), new List<string> { "test_string1", "test_string2" }).SetName("TestSetAndGet_RefType");
            }
        }

        [Test, TestCaseSource(nameof(SeTAndGetTestCases))]
        public void Get_ReturnsSetValue<T>(ICacheProvider<T> cache, T value)
        {
            string key = "test_key";

            cache.Set(key: key, val: value);
            var result = cache.Get(key);

            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void Test_DefaultTTL()
        {
            LocalCache<bool?> cacheProvider = new LocalCache<bool?>(maxItems: 1);
            bool? expectedResult = true;
            var key = "test_key";

            cacheProvider.Set(key: key, val: expectedResult);
            var existingResult = cacheProvider.Get(key);
            Thread.Sleep(LocalCache<bool>.DEFAULT_CACHE_TTL + TimeSpan.FromMilliseconds(5));
            var evictedResult = cacheProvider.Get(key);

            Assert.That(existingResult, Is.EqualTo(expectedResult));
            Assert.That(evictedResult, Is.Null);
        }

        [Test]
        public void Test_DefaultCapacity()
        {
            LocalCache<int?> cacheProvider = new LocalCache<int?>(ttl: TimeSpan.FromMinutes(10));
            int? expectedResult = -1;
            var key = "test_key";

            cacheProvider.Set(key: key, val: expectedResult);
            foreach (int i in Enumerable.Range(1, LocalCache<bool>.DEFAULT_CACHE_CAPACITY - 1))
            {
                cacheProvider.Set(key: i.ToString(), val: i);

                Assert.That(cacheProvider.Get(key: key), Is.EqualTo(expectedResult));
            }
            cacheProvider.Set(key: "new_key", val: -2);
            var evictedResult = cacheProvider.Get(1.ToString());

            Assert.That(cacheProvider.Get(key: key), Is.EqualTo(expectedResult));
            Assert.That(cacheProvider.Get(key: "new_key"), Is.EqualTo(-2));
            Assert.That(evictedResult, Is.Null);
        }

        [Test]
        public void Test_NotExistentKeyReturnsDefaultValue()
        {
            LocalCache<INullable> cacheProvider = new LocalCache<INullable>(maxItems: 1);
            Assert.That(cacheProvider.Get("non_existent_key"), Is.Null);
        }

        [Test]
        public void Test_ConcurrentAccess()
        {
            int numberOfThreads = 50;
            int cacheCapacity = 30;
            LocalCache<int?> cacheProvider = new LocalCache<int?>(maxItems: cacheCapacity, ttl: TimeSpan.FromHours(5));
            var tasks = new List<Task>();
            var countdownEvent = new CountdownEvent(1);

            for (int t = 0; t < numberOfThreads; t++)
            {
                int start = t * cacheCapacity + 1;
                int end = start + cacheCapacity - 1;

                tasks.Add(Task.Run(() =>
                {
                    countdownEvent.Wait();
                    for (int i = start; i <= end; i++)
                    {
                        cacheProvider.Set(i.ToString(), i);
                    }
                }));
            }

            countdownEvent.Signal();
            Task.WaitAll(tasks.ToArray());

            List<int> cacheHitsIndices = new List<int>();

            for (int i = 1; i <= numberOfThreads * cacheCapacity; i++)
            {
                if (cacheProvider.Get(i.ToString()) == i)
                {
                    cacheHitsIndices.Add(i);
                }
            }

            Assert.That(cacheHitsIndices.Count, Is.EqualTo(cacheCapacity));
            Assert.That(
                cacheHitsIndices[cacheCapacity - 1] - cacheHitsIndices[0] + 1,
                Is.Not.EqualTo(cacheCapacity)
            );
        }

        [Test]
        public void Test_TTLOverride()
        {
            LocalCache<int?> cacheProvider = new LocalCache<int?>(maxItems: 1000, ttl: TimeSpan.FromHours(5));
            var tasks = new List<Task>();
            var countdownEvent = new CountdownEvent(1);
            string key = "test_key";
            int expectedValue = 5;
            TimeSpan ttlOverride = TimeSpan.FromSeconds(3);

            tasks.Add(Task.Run(() =>
            {
                countdownEvent.Wait();
                cacheProvider.Set(key: key, val: expectedValue, ttlOverride: ttlOverride);
            }));
            tasks.Add(Task.Run(() =>
            {
                countdownEvent.Wait();
                Thread.Sleep(1000);
                Assert.That(cacheProvider.Get(key), Is.EqualTo(expectedValue));
            }));
            tasks.Add(Task.Run(() =>
            {
                countdownEvent.Wait();
                Thread.Sleep(ttlOverride + TimeSpan.FromMilliseconds(1));
                Assert.That(cacheProvider.Get(key), Is.Null);
            }));

            countdownEvent.Signal();
            Task.WaitAll(tasks.ToArray());
        }

        [Test]
        public void Test_EvictionByLastAccessed()
        {
            LocalCache<int?> cacheProvider = new LocalCache<int?>(maxItems: 10, ttl: TimeSpan.FromHours(5));
            foreach (var i in Enumerable.Range(1, 10))
            {
                cacheProvider.Set(i.ToString(), i);
            }

            foreach (var i in Enumerable.Range(1, 10))
            {
                Assert.That(cacheProvider.Get(i.ToString()), Is.EqualTo(i));
            }

            foreach (var I in Enumerable.Range(1, 10))
            {
                cacheProvider.Set((I + 10).ToString(), -1);
                Assert.That(cacheProvider.Get(I.ToString()), Is.Null);
            }
        }
    }
}
