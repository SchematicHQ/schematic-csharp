using Moq;
using NUnit.Framework;

namespace SchematicHQ.Client.Tests
{
    [TestFixture]
    public class EventBufferTests
    {
        private Mock<ISchematicLogger> _mockLogger;
        private EventBuffer<int> _buffer;
        private List<int> _processedItems;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ISchematicLogger>();
            _processedItems = new List<int>();
            _buffer = new EventBuffer<int>(async items =>
            {
                lock (_processedItems)
                {
                    _processedItems.AddRange(items);
                }
                await Task.CompletedTask; // Simulate async operation
            }, _mockLogger.Object);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _buffer.Stop();
        }

        [Test]
        public void Start_BufferStartsSuccessfully()
        {
            _buffer.Start();
            Assert.DoesNotThrow(() => _buffer.Push(1));
        }

        [Test]
        public async Task Stop_BufferStopsSuccessfully()
        {
            _buffer.Start();

            await _buffer.Stop();

            Assert.Throws<InvalidOperationException>(() => _buffer.Push(1));
            
            var cts = GetPrivateFieldValue<CancellationTokenSource>(_buffer, "_cts");
            
            Assert.That(IsCancellationTokenSourceDisposed(cts), Is.True, "CancellationTokenSource was not disposed.");
        }

        [Test]
        public async Task Push_ItemsAreAddedToBuffer()
        {
            _buffer.Start();
            _buffer.Push(1);
            await _buffer.Flush();

            Assert.That(_processedItems.Count, Is.EqualTo(1));
            Assert.That(_processedItems[0], Is.EqualTo(1));
        }

        [Test]
        public async Task Flush_ManualFlushWorks()
        {
            _buffer.Start();
            _buffer.Push(1);
            await _buffer.Flush();

            Assert.That(_processedItems.Count, Is.EqualTo(1));
            Assert.That(_processedItems[0], Is.EqualTo(1));
        }

        [Test]
        public void PeriodicFlush_FlushesAtInterval()
        {
            var autoResetEvent = new AutoResetEvent(false);
            _buffer = new EventBuffer<int>(async items =>
            {
                _processedItems.AddRange(items);
                autoResetEvent.Set();
                await Task.CompletedTask; // Simulate async operation
            }, _mockLogger.Object, 100, TimeSpan.FromMilliseconds(500));

            _buffer.Start();
            _buffer.Push(1);

            Assert.That(autoResetEvent.WaitOne(2000), Is.True);
            Assert.That(_processedItems.Count, Is.EqualTo(1));
            Assert.That(_processedItems[0], Is.EqualTo(1));
        }

        [Test]
        public async Task Push_ParallelOperations()
        {
            _buffer.Start();
            var tasks = Enumerable.Range(0, 1000).Select(i => Task.Run(() => _buffer.Push(i))).ToArray();

            await Task.WhenAll(tasks);
            await _buffer.Flush();

            Assert.That(_processedItems.Count, Is.EqualTo(1000));
        }

        [Test]
        public async Task Flush_DoesNotInterfereWithPeriodicFlush()
        {
            var flushes = 0;
            var manualFlushes = 0;

            _buffer = new EventBuffer<int>(async items =>
            {
                flushes++;
                if (items.Contains(-1))
                {
                    manualFlushes++;
                }
                await Task.CompletedTask;
            }, _mockLogger.Object, 100, TimeSpan.FromMilliseconds(500));

            _buffer.Start();

            var pushTasks = Enumerable.Range(0, 1000).Select(i => Task.Run(() => _buffer.Push(i))).ToArray();
            await Task.WhenAll(pushTasks);

            // Manual flush
            _buffer.Push(-1);
            await _buffer.Flush();

            // Wait for at least one periodic flush
            Thread.Sleep(2000);

            _buffer.Stop();

            Assert.That(flushes, Is.GreaterThan(1));
            Assert.That(manualFlushes, Is.EqualTo(1));
        }

        [Test]
        public async Task StartAndStop_ConcurrencyTest()
        {
            // Use separate buffer instances to avoid race conditions on shared state
            var startStopTasks = Enumerable.Range(0, 100).Select(async i =>
            {
                var processedItems = new List<int>();
                var buffer = new EventBuffer<int>(async items =>
                {
                    lock (processedItems)
                    {
                        processedItems.AddRange(items);
                    }
                    await Task.CompletedTask;
                }, _mockLogger.Object);

                buffer.Start();
                await Task.Delay(10);
                await buffer.Stop();
                
                // Ensure this specific buffer is stopped
                Assert.Throws<InvalidOperationException>(() => buffer.Push(1));
            }).ToArray();

            await Task.WhenAll(startStopTasks);
        }

        [Test]
        public async Task PushAndFlush_ConcurrencyTest()
        {
            _buffer.Start();

            var pushTasks = Enumerable.Range(0, 1000).Select(i => Task.Run(() => _buffer.Push(i))).ToArray();
            var flushTasks = Enumerable.Range(0, 100).Select(i => Task.Run(async () =>
            {
                await Task.Delay(10);
                await _buffer.Flush();
            })).ToArray();

            await Task.WhenAll(pushTasks.Concat(flushTasks));

            // Ensure all items were processed
            await _buffer.Flush();
            Assert.That(_processedItems.Count, Is.EqualTo(1000));
        }

        [Test]
        public async Task SimultaneousFlushes_DoNotInterfere()
        {
            _buffer.Start();

            var pushTasks = Enumerable.Range(0, 1000).Select(i => Task.Run(() => _buffer.Push(i))).ToArray();

            await Task.WhenAll(pushTasks);

            var flushTasks = Enumerable.Range(0, 10).Select(i => Task.Run(async () =>
            {
                await _buffer.Flush();
            })).ToArray();

            await Task.WhenAll(flushTasks);

            Assert.That(_processedItems.Count, Is.EqualTo(1000));
        }

        [Test]
        public void SizeBasedFlush_TriggersWhenMaxSizeReached()
        {
            var autoResetEvent = new AutoResetEvent(false);
            var flushedItems = new List<int>();
            var maxSize = 5;
            _buffer = new EventBuffer<int>(async items =>
            {
                lock (flushedItems)
                {
                    flushedItems.AddRange(items);
                }
                autoResetEvent.Set();
                await Task.CompletedTask;
            }, _mockLogger.Object, maxSize, TimeSpan.FromSeconds(60));

            _buffer.Start();

            for (int i = 0; i < maxSize; i++)
            {
                _buffer.Push(i);
            }

            Assert.That(autoResetEvent.WaitOne(5000), Is.True);
            lock (flushedItems)
            {
                Assert.That(flushedItems.Count, Is.EqualTo(maxSize));
            }
        }

        [Test]
        public async Task Flush_RetriesOnTransientFailure()
        {
            var callCount = 0;
            var flushedItems = new List<int>();
            _buffer = new EventBuffer<int>(async items =>
            {
                var attempt = Interlocked.Increment(ref callCount);
                if (attempt == 1)
                {
                    throw new Exception("Transient failure");
                }
                lock (flushedItems)
                {
                    flushedItems.AddRange(items);
                }
                await Task.CompletedTask;
            }, _mockLogger.Object);

            _buffer.Start();
            _buffer.Push(1);
            _buffer.Push(2);
            await _buffer.Flush();

            Assert.That(callCount, Is.EqualTo(2));
            lock (flushedItems)
            {
                Assert.That(flushedItems.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public async Task Flush_DiscardsAfterMaxRetries()
        {
            var callCount = 0;
            _buffer = new EventBuffer<int>(async items =>
            {
                Interlocked.Increment(ref callCount);
                await Task.CompletedTask;
                throw new Exception("Persistent failure");
            }, _mockLogger.Object);

            _buffer.Start();
            _buffer.Push(1);
            _buffer.Push(2);
            await _buffer.Flush();

            // 1 initial attempt + 3 retries = 4 total calls
            Assert.That(callCount, Is.EqualTo(4));
            // Events should be discarded, not re-queued
            Assert.That(_buffer.GetEventCount(), Is.EqualTo(0));
        }

        [Test]
        public async Task Stop_FlushesRemainingEvents()
        {
            var flushedItems = new List<int>();
            _buffer = new EventBuffer<int>(async items =>
            {
                lock (flushedItems)
                {
                    flushedItems.AddRange(items);
                }
                await Task.CompletedTask;
            }, _mockLogger.Object, 100, TimeSpan.FromSeconds(60));

            _buffer.Start();
            _buffer.Push(1);
            _buffer.Push(2);
            _buffer.Push(3);

            await _buffer.Stop();

            lock (flushedItems)
            {
                Assert.That(flushedItems.Count, Is.EqualTo(3));
                Assert.That(flushedItems, Is.EquivalentTo(new[] { 1, 2, 3 }));
            }
        }

        private bool IsSemaphoreSlimDisposed(SemaphoreSlim semaphore)
        {
            try
            {
                semaphore.Wait(0); // Attempt a wait operation
                return false; // If no exception, it's not disposed
            }
            catch (ObjectDisposedException)
            {
                return true; // If ObjectDisposedException, it's disposed
            }
        }

        private bool IsCancellationTokenSourceDisposed(CancellationTokenSource cts)
        {
            try
            {
                cts.Cancel(); // Attempt to cancel
                return false; // If no exception, it's not disposed
            }
            catch (ObjectDisposedException)
            {
                return true; // If ObjectDisposedException, it's disposed
            }
        }

        private T GetPrivateFieldValue<T>(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (T)field?.GetValue(obj);
        }
    }
}
