
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;
using TCore.Pipeline;

namespace PipeRef
{
    public partial class PipeRef : Form
    {
        // When you create a pipeline, you have to tell it the type of the items
        // you want queued and marshalled to the listener.

        // The actual object will not be dispatched to the listener, rather a clone
        // of the object will be sent. This type needs to implement
        // "InitFrom" which will initialize a new object from the give object
        // (if you want to have any move semantics, then do that here. In this case,
        // we move the stopwatch from the given object)
        // You also have to provide a clean constructor.
        class WorkItem : IPipelineBase<WorkItem>
        {
            public int WorkId { get; set; }
            public int Cost { get; set; }
            public bool AbortQueueSupported { get; set; }

            private Stopwatch m_sw;

            public WorkItem()
            {
            }

            public WorkItem(int workId, int cost, bool abortQueueSupported)
            {
                WorkId = workId;
                Cost = cost;
                AbortQueueSupported = abortQueueSupported;
                m_sw = new Stopwatch();
                m_sw.Start();
            }

            public void InitFrom(WorkItem item)
            {
                m_sw = item.m_sw;
                item.m_sw = null;
                WorkId = item.WorkId;
                Cost = item.Cost;
                AbortQueueSupported = item.AbortQueueSupported;
            }

            public static string FormatTimeParts(long secs, long ms, long micro)
            {
                if (secs > 0)
                {
                    if (ms == 0 && micro == 0)
                        return $"{secs}s";

                    if (micro == 0)
                        return $"{secs}.{ms:0##}s";

                    return $"{secs}.{ms:0##}{micro:0##}s";
                }

                if (ms == 0)
                    return $"{micro}µs";

                if (micro == 0)
                    return $"{ms}ms";

                return $"{ms}.{micro:0##}ms";
            }

            public static string FormatElapsed(long ticks, long ticksPerSec)
            {
                long secs = (long) (ticks / ticksPerSec);
                long ticksRemain = (ticks - secs * ticksPerSec);
                long ms = (long) (ticksRemain * 1000) / ticksPerSec;
                ticksRemain -= (ms * (ticksPerSec / 1000));
                long micro = (long) (ticksRemain * 1000 * 1000) / ticksPerSec;

                return FormatTimeParts(secs, ms, micro);
            }

            public string Elapsed()
            {
                return FormatElapsed(m_sw.ElapsedTicks, Stopwatch.Frequency);
            }

            [TestCase(0, 0, 10, "10µs")]
            [TestCase(0, 0, 0, "0µs")]
            [TestCase(0, 0, 100, "100µs")]
            [TestCase(0, 0, 999, "999µs")]
            [TestCase(0, 10, 0, "10ms")]
            [TestCase(0, 10, 10, "10.010ms")]
            [TestCase(10, 0, 0, "10s")]
            [TestCase(10, 10, 0, "10.010s")]
            [TestCase(10, 10, 10, "10.010010s")]
            [Test]
            public static void TestFormatTimeParts(long secs, long ms, long micro, string sExpected)
            {
                Assert.AreEqual(sExpected, FormatTimeParts(secs, ms, micro));
            }

            // note 1 = 1s granularity, 1000 = ms, 1000000 = µs, 10000000 = 1/10µs
            [TestCase(1, 1, "1s")]
            [TestCase(1000, 1000, "1s")]
            [TestCase(1500, 1000, "1.500s")]
            [TestCase(500, 1000, "500ms")]
            [TestCase(1, 1000, "1ms")]
            [TestCase(1000000, 1000000, "1s")]
            [TestCase(1500000, 1000000, "1.500s")]
            [TestCase(1000500, 1000000, "1.000500s")]
            [TestCase(500000, 1000000, "500ms")]
            [TestCase(500500, 1000000, "500.500ms")]
            [TestCase(500001, 1000000, "500.001ms")]
            [TestCase(10000000, 10000000, "1s")]
            [TestCase(15000000, 10000000, "1.500s")]
            [TestCase(5000000, 10000000, "500ms")]
            [TestCase(50000, 10000000, "5ms")]
            [TestCase(51000, 10000000, "5.100ms")]
            [TestCase(50010, 10000000, "5.001ms")]
            [TestCase(51, 10000000, "5µs")]
            [Test]
            public static void TestElapsed(long ticks, long ticksPerSec, string sExpected)
            {
                Assert.AreEqual(sExpected, FormatElapsed(ticks, ticksPerSec));
            }
        }

        // This is the actual dispatch method that will be called whenever there is work waiting
        // on the background thread. This will get an enumerable set of items.
        // This is happening on the background thread as we pull item(s)
        // off the queue
        void ProcessWorkItems(IEnumerable<WorkItem> workItems, Consumer<WorkItem>.ShouldAbortDelegate shouldAbort)
        {
            Log($"{DateTime.Now}: Processing workItems");

            foreach (WorkItem workItem in workItems)
            {
                if (workItem.AbortQueueSupported && shouldAbort())
                {
                    Log($"{DateTime.Now}: Abort requested, wait time {workItem.Elapsed()}");
                    return;
                }

                Log($"{DateTime.Now}: Starting processing for {workItem.WorkId}, wait time {workItem.Elapsed()}");
                if (workItem.Cost != 0)
                    Thread.Sleep(workItem.Cost);
                Log($"{DateTime.Now}: Completed processing for {workItem.WorkId}, wait time {workItem.Elapsed()}");
            }
        }

        void LogCore(string s)
        {
            if (m_tbLog.TextLength > 0)
                m_tbLog.AppendText(Environment.NewLine);

            m_tbLog.AppendText(s);
        }

        void Log(string s)
        {
            s = $"[{Thread.CurrentThread.ManagedThreadId}]: {s}";

            if (m_tbLog.InvokeRequired)
            {
                m_tbLog.Invoke((Action) (() => { LogCore(s); }));
            }
            else
            {
                LogCore(s);
            }
        }
    }
}